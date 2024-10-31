using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Core.Models;

/// <summary>
/// Represents a base model that contains a code and name pair.
/// </summary>
public class CodeNameModel
{
    /// <summary>
    /// Initializes a new instance of the CodeNameModel class.
    /// </summary>
    public CodeNameModel()
    {
        Code = string.Empty;
        Name = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the CodeNameModel class with specified code and name.
    /// </summary>
    /// <param name="code">The unique code identifier.</param>
    /// <param name="name">The human-readable name.</param>
    public CodeNameModel(string code, string name)
    {
        Code = code;
        Name = name;
    }

    /// <summary>
    /// Gets or sets the unique code identifier.
    /// </summary>
    [JsonProperty(nameof(Code), Order = -1000)]
    [JsonPropertyOrder(-1000)]
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the human-readable name.
    /// </summary>
    [JsonProperty(nameof(Name), Order = -1000)]
    [JsonPropertyOrder(-1000)]
    public string Name { get; set; }
}