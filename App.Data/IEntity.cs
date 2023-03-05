namespace App.Data;

public interface IEntity
{
    string? Id { get; set; }

    DateTime CreatedTs { get; set; }

    DateTime? UpdatedTs { get; set; }
}