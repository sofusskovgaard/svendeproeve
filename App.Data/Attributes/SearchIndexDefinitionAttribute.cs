namespace App.Data.Attributes;

/// <summary>
///     Used to define a search index in a collection
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class SearchIndexDefinitionAttribute : Attribute
{
    /// <summary>
    ///     Used to define an index in a collection
    /// </summary>
    /// <param name="name">Name of the search index in a collection</param>
    public SearchIndexDefinitionAttribute(string name = "search")
    {
        this.Name = name;
    }

    /// <summary>
    ///     Name of the search index in a collection
    /// </summary>
    public string Name { get; set; }
}