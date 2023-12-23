using Api.Common;

namespace Api.Entities;

public class Budget : Entity
{
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public decimal Usage { get; set; }
    public int Recurrence { get; set; }
}