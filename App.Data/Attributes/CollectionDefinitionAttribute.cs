namespace App.Data.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class CollectionDefinitionAttribute : Attribute
{
    public CollectionDefinitionAttribute(string name)
    {
        this.Name = name;
    }

    public string Name { get; set; }
}