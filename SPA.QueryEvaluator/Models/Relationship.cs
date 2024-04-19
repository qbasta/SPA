using SPA.Domain.Models;

namespace SPA.QueryEvaluator.Models;

public class Relationship
{
    public IStatement A { get; set; }

    public IStatement B { get; set; }
}