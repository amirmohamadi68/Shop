using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
    public record Response<T>
    {
        public T Data { get; private set; }
        public string Message { get; private set; }
        public static Response<T> Create(T data , string message)
        { 
            return new Response<T>(data , message);
           
        }
        private Response(T data, string message) {
            this.Data = Data;
            this.Message = Message;
        }
    }
    public record Response
    {
        public string Message { get; private set; }
        public static Response Create( string message)
        {
            return new Response( message);

        }
        private Response( string message)
        {
            this.Message = Message;
        }
    }
}
