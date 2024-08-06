using System.Text.Json.Serialization;

public class Cabecalho
{
    [JsonPropertyName("typ")]
    public string Typ { get; set; }

    [JsonPropertyName("alg")]
    public string Alg { get; set; }
}