namespace Api.Common;

public abstract class Entity
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}