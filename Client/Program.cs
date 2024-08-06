// See https://aka.ms/new-console-template for more information

using System.Net.Http.Json;


try
{

    Carro carro = new Carro();

    carro.Cor = "Azul";
    carro.Modelo = "Corsa";
    carro.Ano = 2002;

    Cabecalho cabecalho = new Cabecalho();
    cabecalho.Typ = "json";
    cabecalho.Alg = "RS256";

    Mensagem mensagem = new Mensagem();

    mensagem.Cabecalho = cabecalho;
    mensagem.Dados = carro;

    string tokenAssinado = mensagem.Assinar();
    
    HttpServer httpServer = new HttpServer();

    httpServer.Send(tokenAssinado);

}
catch (Exception e)
{
    string mesage = e.Message;
}




