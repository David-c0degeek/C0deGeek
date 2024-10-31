using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Core.Models;

/// <summary>
/// Represents a model that contains a code, name, and description.
/// </summary>
public class CodeNameDescriptionModel : CodeNameModel
{
    /// <summary>
    /// Initializes a new instance of the CodeNameDescriptionModel class.
    /// </summary>
    internal CodeNameDescriptionModel()
    {
        Description = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the CodeNameDescriptionModel class with specified code, name, and description.
    /// </summary>
    /// <param name="code">The unique code identifier.</param>
    /// <param name="name">The human-readable name.</param>
    /// <param name="description">The detailed description.</param>
    public CodeNameDescriptionModel(string code, string name, string description) : base(code, name)
    {
        Description = description;
    }

    /// <summary>
    /// Gets or sets the detailed description.
    /// </summary>
    [JsonProperty(nameof(Description), Order = -990)]
    [JsonPropertyOrder(-990)]
    public string Description { get; set; }
}