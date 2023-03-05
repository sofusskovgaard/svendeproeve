namespace App.Data.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class SearchIndexDefinitionAttribute : Attribute
{
    public SearchIndexDefinitionAttribute(string name)
    {
        this.Name = name;
    }

    public string Name { get; set; }
}