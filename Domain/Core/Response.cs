using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
    public record Response<T>
    {
        public T Data { get;  set; }
        public string Message { get;  set; }
        public static Response<T> Create(T data , string message)
        { 
            return new Response<T>(data , message);
           
        }
        private Response(T data, string message) {
            this.Data = data;
            this.Message = message;
        }
    }
    public record Response
    {
        public string Message { get;  set; }
        public static Response Create( string message)
        {
            return new Response( message);

        }
        private Response( string message)
        {
            this.Message = message;
        }
    }
}
