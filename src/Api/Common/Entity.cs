namespace Api.Common;

public abstract class Entity
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}