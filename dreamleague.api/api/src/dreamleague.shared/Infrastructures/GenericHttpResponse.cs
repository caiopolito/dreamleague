using System.Net;

namespace dreamleague.shared.Infrastructures
{
    public class GenericHttpResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string[] Errors { get; set; }
    }

    public class GenericHttpResponse<T> : GenericHttpResponse
    {
        public T Content { get; set; }
    }
}
