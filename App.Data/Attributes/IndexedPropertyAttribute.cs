namespace App.Data.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class IndexedPropertyAttribute : Attribute
{
    public enum Direction
    {
        Ascending = 1,
        Descending = -1
    }

    public IndexedPropertyAttribute(string indexName, Direction order = Direction.Ascending)
    {
        IndexName = indexName;
        Order = order;
    }

    public string IndexName { get; set; }

    public Direction Order { get; set; }
}