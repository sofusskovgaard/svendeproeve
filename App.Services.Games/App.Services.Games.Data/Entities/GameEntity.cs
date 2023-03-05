using App.Data;
using App.Data.Attributes;

namespace App.Services.Games.Data.Entities;

[CollectionDefinition(nameof(GameEntity))]
public class GameEntity : BaseEntity
{
    public string Name { get; set; }

    public string Discription { get; set; }

    public string ProfilePicture { get; set; }

    public string CoverPicture { get; set; }

    public string[] Genre { get; set; }
}