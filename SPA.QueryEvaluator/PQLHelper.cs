using SPA.QueryEvaluator.Models;

namespace SPA.QueryEvaluator;

public static class PQLHelper
{
    public static List<Variable> ParseDeclarations(string declarationLine)
    {
        var splitDeclarations = declarationLine.Split(';');
        var result = new List<Variable>();

        foreach (var part in splitDeclarations)
        {
            if (string.IsNullOrWhiteSpace(part))
                continue;

            var splitPart = part.Split(' ', ',').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            var type = GetVariableType(splitPart[0]);
            var variables = splitPart.Skip(1).Select(x => new Variable { Type = type, Name = x });
            result.AddRange(variables);
        }

        return result;
    }

    private static VariableType GetVariableType(string name)
    {
        switch (name)
        {
            case "stmt":
                return VariableType.Stmt;
            case "assign":
                return VariableType.Assign;
            case "while":
                return VariableType.While;
            default:
                throw new Exception("Not supported variable type");
        }
    }
}