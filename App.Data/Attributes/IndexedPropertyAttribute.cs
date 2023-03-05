namespace App.Data.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class IndexedPropertyAttribute : Attribute
{
    public enum Direction
    {
        Ascending = 1,
        Descending = -1
    }

    /// <summary>
    /// Specify this property as indexed
    /// </summary>
    /// <param name="indexName">the index to include the property in</param>
    public IndexedPropertyAttribute(string indexName)
    {
        this.IndexName = indexName;
    }

    /// <summary>
    /// Specify a sort order on this property.
    /// </summary>
    /// <param name="indexName">the index to include the property in</param>
    /// <param name="order">the sort order of this property in the index</param>
    public IndexedPropertyAttribute(string indexName, Direction order)
    {
        this.IndexName = indexName;
        this.Order = order;
    }

    /// <summary>
    /// Specify the weight of this property in a search index. This is for text indexes.
    /// </summary>
    /// <param name="indexName">the index to include the property in</param>
    /// <param name="weight">the weight of this property in the search index</param>
    public IndexedPropertyAttribute(string indexName, int weight)
    {
        this.IndexName = indexName;
        this.Weight = weight;
    }

    /// <summary>
    /// Specify this property as hashed. This is for sharding indexes.
    /// </summary>
    /// <param name="indexName">the index to include the property in</param>
    /// <param name="hashed">if this property is hashed</param>
    public IndexedPropertyAttribute(string indexName, bool hashed)
    {
        this.IndexName = indexName;
        this.Hashed = hashed;
    }

    /// <summary>
    /// The index to include the the target property in
    /// </summary>
    public string IndexName { get; set; }

    /// <summary>
    /// The weight to assign the target property in a search index
    /// </summary>
    public int Weight { get; set; } = 1;

    /// <summary>
    /// If the property should be hashed
    /// </summary>
    public bool Hashed { get; set; }

    /// <summary>
    /// Specifies the sort order direction
    /// </summary>
    public Direction Order { get; set; } = Direction.Ascending;
}