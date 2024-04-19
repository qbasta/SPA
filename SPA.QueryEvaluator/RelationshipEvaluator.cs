using SPA.Domain.Models;
using SPA.QueryEvaluator.Enums;
using SPA.QueryEvaluator.Models;
using SPA.QueryEvaluator.RelationshipProcessors;

namespace SPA.QueryEvaluator;

public class RelationshipEvaluator
{
    private readonly MainProcedure _mainProcedure;

    public RelationshipEvaluator(MainProcedure mainProcedure)
    {
        _mainProcedure = mainProcedure;
    }

    public List<Relationship>? GetRelationships(string operation, PQLArgument a, PQLArgument b)
    {
        RelationshipProcessorBase relationshipProcessor = operation switch
        {
            "Follows" => new FollowsProcessor(_mainProcedure),
            "Follows*" => new FollowsTProcessor(_mainProcedure),
            "Parent" => new ParentProcessor(_mainProcedure),
            "Parent*" => new ParentTProcessor(_mainProcedure),
            "Modifies" => new ModifiesProcessor(_mainProcedure),
            "Uses" => new UsesProcessor(_mainProcedure),
            _ => throw new Exception("Unhandled PQL operation")
        };

        var relationships = relationshipProcessor.GetRelationships(a, b);

        if (relationships.Count == 0)
        {
            return relationships;
        }

        switch (a.Type, b.Type)
        {
            case (ArgumentType.LineNumber, ArgumentType.DeclaredVariable):
                return relationships.Where(r => r.A.LineNumber == a.LineNumber
                                                && CheckType(b.Variable.Type, r.B)).ToList();
            case (ArgumentType.DeclaredVariable, ArgumentType.LineNumber):
                return relationships.Where(r => CheckType(a.Variable.Type, r.A)
                                                && r.B.LineNumber == b.LineNumber).ToList();
            case (ArgumentType.DeclaredVariable, ArgumentType.DeclaredVariable):
                return relationships.Where(r => CheckType(a.Variable.Type, r.A)
                                                && CheckType(b.Variable.Type, r.B)).ToList();
            case (ArgumentType.DeclaredVariable, ArgumentType.VariableName):
                return relationships.Where(r => CheckType(a.Variable.Type, r.A)).ToList();
            default:
                throw new Exception("Not supported argument configuration");
        }
    }

    private bool CheckType(VariableType expected, IStatement? statement)
    {
        return expected switch
        {
            VariableType.Stmt => statement != null,
            VariableType.Assign => statement is Assign,
            VariableType.While => statement is While,
            _ => throw new NotImplementedException()
        };
    }
}