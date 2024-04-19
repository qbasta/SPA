using SPA.QueryEvaluator.Enums;

namespace SPA.QueryEvaluator.Models;

public class PQLArgument
{
    public ArgumentType Type { get; set; }

    public int LineNumber { get; set; }

    public string? Name { get; set; }

    public Variable? Variable { get; set; }
}