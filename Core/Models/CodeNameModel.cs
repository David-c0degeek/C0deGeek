using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Core.Models;

public class CodeNameModel
{
    public CodeNameModel()
    {
    }

    public CodeNameModel(string code, string name)
    {
        Code = code;
        Name = name;
    }

    [JsonProperty("Code", Order = -1000)]
    [JsonPropertyOrder(-1000)]
    public string Code { get; set; }

    [JsonProperty("Name", Order = -1000)]
    [JsonPropertyOrder(-1000)]
    public string Name { get; set; }
}