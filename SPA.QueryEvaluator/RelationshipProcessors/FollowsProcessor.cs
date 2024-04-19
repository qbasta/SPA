using SPA.Domain.Models;
using SPA.QueryEvaluator.Models;

namespace SPA.QueryEvaluator.RelationshipProcessors;

public class FollowsProcessor : RelationshipProcessorBase
{
    public FollowsProcessor(MainProcedure mainProcedure) : base(mainProcedure)
    {
    }

    public override List<Relationship> GetRelationships(PQLArgument a, PQLArgument b)
    {
        var result = new List<Relationship>();

        foreach (var procedure in _mainProcedure.Procedures)
        {
            var statements = procedure.StatementsList.Statements;
            for (int i = 1; i < statements.Count; i++)
            {
                result.Add(new Relationship { A = statements[i - 1], B = statements[i] });
            }
        }

        return result;
    }
}