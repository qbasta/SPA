using SPA.Domain.Models;
using SPA.QueryEvaluator.Models;
using System.Collections.Generic;

namespace SPA.QueryEvaluator.RelationshipProcessors;

public class UsesProcessor : RelationshipProcessorBase
{
    public UsesProcessor(MainProcedure mainProcedure) : base(mainProcedure)
    {
    }

    public override List<Relationship> GetRelationships(PQLArgument a, PQLArgument b)
    {
        var result = new List<Relationship>();

        foreach (var procedure in _mainProcedure.Procedures)
        {
            foreach (var statement in procedure.StatementsList.Statements)
            {
                FindUses(result, statement, b.Name);
            }
        }

        return result;
    }

    private void FindUses(List<Relationship> relationships, IStatement statement, string variableName)
    {
        switch (statement)
        {
            case Assign assign:
                if (ExpressionContainsVariable(assign.Expression, variableName))
                {
                    relationships.Add(new Relationship { A = assign, B = null });
                }
                break;
            case While w:
                foreach (var innerStatement in w.StatementsList.Statements)
                {
                    FindUses(relationships, innerStatement, variableName);
                }
                break;
            default:
                break;
        }
    }

    private bool ExpressionContainsVariable(IExpression expression, string variableName)
    {
        switch (expression)
        {
            case VariableReference vr:
                return vr.Name == variableName;
            case BinaryOperation bo:
                return ExpressionContainsVariable(bo.Left, variableName) || ExpressionContainsVariable(bo.Right, variableName);
            case UnaryOperation uo:
                return ExpressionContainsVariable(uo.Operand, variableName);
            default:
                return false;
        }
    }
}
