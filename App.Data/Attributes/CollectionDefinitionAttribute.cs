namespace App.Data.Attributes;

/// <summary>
///     Used to define a database collection
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class CollectionDefinitionAttribute : Attribute
{
    /// <summary>
    ///     Used to define a database collection
    /// </summary>
    /// <param name="name">Name of the collection in the database</param>
    public CollectionDefinitionAttribute(string name)
    {
        this.Name = name;
    }

    /// <summary>
    ///     Name of the collection in the database
    /// </summary>
    public string Name { get; set; }
}