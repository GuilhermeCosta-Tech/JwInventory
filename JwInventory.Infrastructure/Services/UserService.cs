using AutoMapper;
using JwInventory.Application.DTOs.User;
using JwInventory.Application.Interfaces.Repositories;
using JwInventory.Application.Interfaces.Services;
using JwInventory.Domain.Entities;
using JwInventory.Infrastructure.Security; // Adicionar para HashHelper
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JwInventory.Infrastructure.Services
{
    public class UserService : IUserService // Implementar a interface
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        // Construtor único e correto injetando as dependências
        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Cria um novo usuário no sistema.
        /// </summary>
        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            if (createUserDto == null)
                throw new ArgumentNullException(nameof(createUserDto));

            // 1. Verificar se o usuário já existe
            var existingUser = await _userRepository.GetByEmailAsync(createUserDto.Email);
            if (existingUser != null)
            {
                throw new Exception("Um usuário com este e-mail já existe.");
            }

            // 2. Mapear DTO para a entidade User
            var user = _mapper.Map<User>(createUserDto);

            // 3. Hashear a senha antes de salvar
            user.PasswordHash = HashHelper.HashPassword(createUserDto.Password);

            // 4. Chamar o repositório para criar o usuário
            var createdUser = await _userRepository.CreateUserAsync(createUserDto);

            // 5. Mapear a entidade criada de volta para DTO e retornar
            return _mapper.Map<UserDto>(createdUser);
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return null; // Retornar nulo se não encontrado
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }


        public async Task<UserDto> UpdateAsync(Guid id, UserDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return null; // Retornar nulo se não encontrado
            }

            _mapper.Map(userDto, user);

            // Se a senha for atualizada, hasheá-la
            if (!string.IsNullOrEmpty(userDto.PasswordHash))
            {
                user.PasswordHash = HashHelper.HashPassword(userDto.PasswordHash);
            }

            await _userRepository.UpdateAsync(id, user);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return false; // Retorna falso se não encontrado
            }
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return null; // Retorna nulo se não encontrado
            }
            return _mapper.Map<UserDto>(user);
        }
    }
}
