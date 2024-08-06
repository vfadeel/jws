using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;

namespace Server.Models
{
    public class Certificado
    {
        private Dictionary<string, string> repositorio { get; set; }
        private string nomeCertificado { get; set; }

        public Certificado(string nomeCertificado)
        {
            repositorio = new Dictionary<string, string>();

            repositorio.Add("Corsa", @"-----BEGIN CERTIFICATE-----
<<ADICIONAR AQUI CERTIFICADO DIGITAL EXPORTADO COM CHAVE PÚBLICA>>
-----END CERTIFICATE-----");

            this.nomeCertificado = nomeCertificado;
        }

        public RSA ObterChavePublica()
        {
            byte[] bytes = Encoding.UTF8.GetBytes(repositorio[nomeCertificado]);

            var certificado = new X509Certificate2(bytes);

            return certificado.GetRSAPublicKey();
        }
    }
}