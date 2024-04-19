using SPA.Domain.Models;
using SPA.QueryEvaluator.Models;

namespace SPA.QueryEvaluator.RelationshipProcessors;

public class ModifiesProcessor : RelationshipProcessorBase
{
    public ModifiesProcessor(MainProcedure mainProcedure) : base(mainProcedure)
    {
    }

    public override List<Relationship> GetRelationships(PQLArgument a, PQLArgument b)
    {
        var result = new List<Relationship>();

        foreach (var procedure in _mainProcedure.Procedures)
        {
            var statements = procedure.StatementsList.Statements;
            FindModifies(result, statements, b.Name);
        }

        return result;
    }

    private void FindModifies(List<Relationship> relationships, List<IStatement> statements, string variableName)
    {
        foreach (var statement in statements)
        {
            switch (statement)
            {
                case Assign a:
                {
                    if (a.VariableName == variableName)
                        relationships.Add(new Relationship { A = statement });
                    break;
                }
                case While w:
                {
                    var countBefore = relationships.Count;
                    var whileChildren = w.StatementsList.Statements;
                    FindModifies(relationships, whileChildren, variableName);
                    if (relationships.Count > countBefore)
                        relationships.Add(new Relationship { A = statement });
                    break;
                }
                default:
                    throw new NotImplementedException();
            }
        }
    }
}