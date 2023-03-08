namespace App.Data.Attributes;

/// <summary>
///     Used to define an index in a collection
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class IndexDefinitionAttribute : Attribute
{
    /// <summary>
    ///     Used to define an index in a collection
    /// </summary>
    /// <param name="name">Name of the index in a collection</param>
    /// <param name="isUnique">If the index should be unique</param>
    public IndexDefinitionAttribute(string name, bool isUnique = false)
    {
        this.Name = name;
        this.IsUnique = isUnique;
    }

    /// <summary>
    ///     Name of the index in a collection
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     If the index should be unique
    /// </summary>
    public bool IsUnique { get; set; }
}