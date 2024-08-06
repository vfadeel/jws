using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

public class Mensagem
{
    public Cabecalho Cabecalho { get; set; }
    public object Dados { get; set; }


    private string CabecalhoToBase64()
    {
        string cabecalhoJson = JsonSerializer.Serialize(this.Cabecalho);

        return ToBase64(cabecalhoJson);
    }

    private string DadosToBase64()
    {
        string dadosJson = JsonSerializer.Serialize(this.Dados);

        return ToBase64(dadosJson);
    }

    private string ToBase64(string json)
    {
        byte[] textoAsBytes = Encoding.ASCII.GetBytes(json);

        string exemplo = System.Convert.ToBase64String(textoAsBytes);

        //Transforma em Base64Url, que é o base64 sem os símbolos não permitidos
        return System.Convert.ToBase64String(textoAsBytes)
                                           .Replace("=", "")
                                           .Replace('+', '-')
                                           .Replace('/', '_');
    }

    public string Assinar()
    {
        string primeiraMetadeToken = CabecalhoToBase64() + "." + DadosToBase64();

        var certificado = new X509Certificate2(@"C:\POC\Jws\Client\Certificado\certificadoComChavePrivada.pfx", "@bc123");

        RSA rsa = certificado.GetRSAPrivateKey();

        byte[] primeiraMetadeTokenBytes = Encoding.UTF8.GetBytes(primeiraMetadeToken);

        byte[] resultadoByte = rsa.SignData(primeiraMetadeTokenBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

        string tokenCriptografado = Convert.ToBase64String(resultadoByte)
                                           .Replace("=", "")
                                           .Replace('+', '-')
                                           .Replace('/', '_');

        return primeiraMetadeToken + "." + tokenCriptografado;
    }
}