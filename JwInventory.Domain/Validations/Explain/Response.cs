using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwInventory.Domain.Validations.Explain
{
    public class Response
    {
        public Response()
        {
            Report = new List<Report>();
        }

        public Response(List<Report> reports)
        {
            Report = reports;
        }

        public Response(Report report) : this(new List<Report> { report })
        {

        }

        public List<Report> Report { get; }


        public static Response Ok<T>(T data) => new Response<T>(data);
        public static Response Ok() => new Response();
        public static Response Unprocessable(List<Report> reports) => new Response(reports);
        public static Response Unprocessable(Report report) => new Response(report);
    }

    public class Response<T> : Response
    {
        public Response(T data) : base()
        {
            Data = data;
        }
        public Response(List<Report> reports, T data) : base(reports)
        {
            Data = data;
        }
        public T Data { get; set; }
    }
}