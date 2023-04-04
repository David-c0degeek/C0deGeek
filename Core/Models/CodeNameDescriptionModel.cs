using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Core.Models;

public class CodeNameDescriptionModel : CodeNameModel
{
    internal CodeNameDescriptionModel()
    {
        Description = string.Empty;
    }

    public CodeNameDescriptionModel(string code, string name, string description) : base(code, name)
    {
        Description = description;
    }

    [JsonProperty("Description", Order = -990)]
    [JsonPropertyOrder(-990)]
    public string Description { get; set; }
}