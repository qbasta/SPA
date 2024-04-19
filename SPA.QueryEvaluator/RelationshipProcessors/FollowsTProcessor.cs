using SPA.Domain.Models;
using SPA.QueryEvaluator.Models;

namespace SPA.QueryEvaluator.RelationshipProcessors;

public class FollowsTProcessor : RelationshipProcessorBase
{
    public FollowsTProcessor(MainProcedure mainProcedure) : base(mainProcedure)
    {
    }

    public override List<Relationship> GetRelationships(PQLArgument a, PQLArgument b)
    {
        var result = new List<Relationship>();

        foreach (var procedure in _mainProcedure.Procedures)
        {
            var statements = procedure.StatementsList.Statements;
            for (int i = 0; i < statements.Count; i++)
            {
                for (int j = i + 1; j < statements.Count; j++)
                {
                    result.Add(new Relationship { A = statements[i], B = statements[j] });
                }
            }
        }

        return result;
    }
}