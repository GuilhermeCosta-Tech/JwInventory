namespace JwInventory.API.Endpoints
{
    public static class ProductExtension
    {
        public static void AddEndpointsProduct(this WebApplication app)
        {
            var groupBuilder = app
                .MapGroup("products")
                .RequireAuthorization()
                .WithTags("Products");
        }

    }
}
