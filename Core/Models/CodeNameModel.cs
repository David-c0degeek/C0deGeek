using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Core.Models;

public class CodeNameModel
{
    public CodeNameModel()
    {
        Code = string.Empty;
        Name = string.Empty;
    }

    public CodeNameModel(string code, string name)
    {
        Code = code;
        Name = name;
    }

    [JsonProperty(nameof(Code), Order = -1000)]
    [JsonPropertyOrder(-1000)]
    public string Code { get; set; }

    [JsonProperty(nameof(Name), Order = -1000)]
    [JsonPropertyOrder(-1000)]
    public string Name { get; set; }
}