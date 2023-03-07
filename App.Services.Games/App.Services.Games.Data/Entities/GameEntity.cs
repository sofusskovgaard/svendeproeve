using App.Data;
using App.Data.Attributes;

namespace App.Services.Games.Data.Entities;

[SearchIndexDefinition("search")]
[CollectionDefinition(nameof(GameEntity))]
public class GameEntity : BaseEntity
{
    [IndexedProperty("search")]
    public string Name { get; set; }

    [IndexedProperty("search")]
    public string Description { get; set; }

    public string ProfilePicture { get; set; }

    public string CoverPicture { get; set; }

    [IndexedProperty("search")]
    public string[] Genre { get; set; }
}