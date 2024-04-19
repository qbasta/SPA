using SPA.Domain.Models;
using SPA.QueryEvaluator.Models;

namespace SPA.QueryEvaluator.RelationshipProcessors;

public abstract class RelationshipProcessorBase
{
    protected readonly MainProcedure _mainProcedure;

    protected RelationshipProcessorBase(MainProcedure mainProcedure)
    {
        _mainProcedure = mainProcedure;
    }
    
    public abstract List<Relationship> GetRelationships(PQLArgument a, PQLArgument b);
}