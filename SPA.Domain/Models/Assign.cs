namespace SPA.Domain.Models;

public class Assign : IStatement
{
    public Assign()
    {
    }

    public Assign(int lineNumber, string variableName, IExpression expression, StatementsList parent)
    {
        LineNumber = lineNumber;
        VariableName = variableName;
        Expression = expression;
        Parent = parent;
    }

    public Assign(int lineNumber, string variableName, IExpression expression) : this()
    {
        LineNumber = lineNumber;
        VariableName = variableName;
        Expression = expression;
    }

    public string VariableName { get; set; }

    public IExpression Expression { get; set; }

    public int LineNumber { get; set; }

    public StatementsList Parent { get; set; }
}