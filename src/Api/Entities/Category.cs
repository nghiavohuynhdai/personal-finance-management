using Api.Common;

namespace Api.Entities;

public class Category : Entity
{
    public CategoryType Type { get; set; }
    public string Name { get; set; }
    public Guid BudgetId { get; set; }
}