namespace App.Data.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class IndexDefinitionAttribute : Attribute
{
    public IndexDefinitionAttribute(string name, bool isUnique = false)
    {
        this.Name = name;
        this.IsUnique = isUnique;
    }

    public string Name { get; set; }

    public bool IsUnique { get; set; }
}