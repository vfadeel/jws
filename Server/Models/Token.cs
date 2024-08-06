using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;

namespace Server.Models
{
    public class Token
    {
        public Carro Carro { get; set; }
        public Cabecalho Cabecalho { get; set; }
        public string Assinatura { get; set; }

        public static Token Preencher(string tokenAssinado)
        {
            Token token = new Token();

            string[] tokenArray = tokenAssinado.Split('.');

            token.Cabecalho = Preencher<Cabecalho>(tokenArray[0]);
            token.Carro = Preencher<Carro>(tokenArray[1]);
            token.Assinatura = tokenArray[2];

            return token;
        }

        private static T Preencher<T>(string base64)
        {
            string base64Url = Base64UrlEncoder.Decode(base64);

            return JsonSerializer.Deserialize<T>(base64Url);
        }
    }
}