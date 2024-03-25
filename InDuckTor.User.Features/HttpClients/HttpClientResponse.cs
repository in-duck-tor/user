namespace InDuckTor.User.Features.HttpClients
{
    public class HttpClientResponse<T>
    {
        public bool Succeed { get; set; }

        public T Content { get; set; }

        public HttpClientResponse(bool succeed)
        {
            this.Succeed = succeed;
        }

        public HttpClientResponse(bool succeed, T content)
        {
            this.Succeed = succeed;
            this.Content = content;
        }
    }
}
