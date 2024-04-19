using SPA.Domain.Models;
using SPA.QueryEvaluator.Enums;
using SPA.QueryEvaluator.Models;

namespace SPA.QueryEvaluator;

public class PQLProcessor
{
    private readonly MainProcedure _mainProcedure;

    public PQLProcessor(MainProcedure mainProcedure)
    {
        _mainProcedure = mainProcedure;
    }

    public List<int> RunQuery(string declaration, string query)
    {
        var declaredVariables = PQLHelper.ParseDeclarations(declaration);

        var splitQuery = query.Split(' ', ',', '(', ')').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

        var selected = splitQuery[1];
        var operationPos = 4;
        var operation = splitQuery[4];
        var operationParamA = splitQuery[++operationPos];
        var operationParamB = splitQuery[++operationPos];

        var argumentA = GetOperationArgument(operationParamA, declaredVariables);
        var argumentB = GetOperationArgument(operationParamB, declaredVariables);

        var relationshipProcessor = new RelationshipEvaluator(_mainProcedure);
        var relationships = relationshipProcessor.GetRelationships(operation, argumentA, argumentB);
        var result = new List<int>();

        if (relationships == null)
            return result;

        result = selected == operationParamA
            ? relationships.Select(x => x.A.LineNumber).ToList()
            : relationships.Select(x => x.B.LineNumber).ToList();

        result = result.Distinct().ToList();

        return result;
    }

    private static PQLArgument GetOperationArgument(string argumentString, List<Variable> declaredVariables)
    {
        var result = new PQLArgument();

        if (int.TryParse(argumentString, out int parsedA))
        {
            result.LineNumber = parsedA;
            result.Type = ArgumentType.LineNumber;
            return result;
        }

        result.Variable = declaredVariables.FirstOrDefault(x => x.Name == argumentString);

        if (result.Variable == null)
        {
            result.Name = argumentString.Trim('"');
            result.Type = ArgumentType.VariableName;
        }
        else
        {
            result.Type = ArgumentType.DeclaredVariable;
        }

        return result;
    }
}