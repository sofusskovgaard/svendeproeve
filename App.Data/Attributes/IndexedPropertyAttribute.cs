namespace App.Data.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class IndexedPropertyAttribute : Attribute
{
    public enum Direction
    {
        Ascending = 1,
        Descending = -1
    }

    public IndexedPropertyAttribute(string indexName, Direction order = Direction.Ascending)
    {
        this.IndexName = indexName;
        this.Order = order;
    }

    public string IndexName { get; set; }

    public Direction Order { get; set; }
}