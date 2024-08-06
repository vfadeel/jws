using System.Net.Http.Json;

public class HttpServer : HttpClient
{
    public HttpServer()
    {
        this.BaseAddress = new Uri("https://localhost:7066");
    }

    public void Send(string tokenAssinado)
    {
        JsonContent content = JsonContent.Create(tokenAssinado);

        HttpResponseMessage message = this.PostAsync("token", content).Result;
    }
}