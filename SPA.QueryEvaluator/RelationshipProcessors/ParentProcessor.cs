using SPA.Domain.Models;
using SPA.QueryEvaluator.Enums;
using SPA.QueryEvaluator.Models;

namespace SPA.QueryEvaluator.RelationshipProcessors;

public class ParentProcessor : RelationshipProcessorBase
{
    public ParentProcessor(MainProcedure mainProcedure) : base(mainProcedure)
    {
    }

    public RelationshipType Type => RelationshipType.Parent;

    public override List<Relationship> GetRelationships(PQLArgument a, PQLArgument b)
    {
        var result = new List<Relationship>();

        foreach (var procedure in _mainProcedure.Procedures)
        {
            var statements = procedure.StatementsList.Statements.Where(x => x is While).Cast<While>();

            foreach (var statement in statements)
                ProcessWhileAsParent(result, statement);
        }

        return result;
    }

    private void ProcessWhileAsParent(List<Relationship> relationships, While statement)
    {
        foreach (var child in statement.StatementsList.Statements)
        {
            relationships.Add(new Relationship { A = statement, B = child });
            if (child is While whileChild)
                ProcessWhileAsParent(relationships, whileChild);
        }
    }
}