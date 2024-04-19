using SPA.Domain.Models;
using SPA.QueryEvaluator.Models;

namespace SPA.QueryEvaluator.RelationshipProcessors;

public class ParentTProcessor : ParentProcessor
{
    public ParentTProcessor(MainProcedure mainProcedure) : base(mainProcedure)
    {
    }

    public override List<Relationship> GetRelationships(PQLArgument a, PQLArgument b)
    {
        var parentRelationships = base.GetRelationships(a, b);
        var result = new List<Relationship>();

        for (var i = 0; i < parentRelationships.Count; i++)
        {
            result.Add(parentRelationships[i]);
            var potentialParent = parentRelationships[i].B;
            for (var j = i + 1; j < parentRelationships.Count; j++)
            {
                if (parentRelationships[j].A.LineNumber == potentialParent.LineNumber)
                    result.Add(new Relationship
                    {
                        A = potentialParent, B = parentRelationships[j].B
                    });
            }
        }

        return result;
    }
}