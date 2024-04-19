namespace SPA.Domain.Models;

public class Ref : IExpression
{
    public Ref(string variableName)
    {
        VariableName = variableName;
    }

    public string VariableName { get; set; }

    public void AddLeft(string Value)
    {
        throw new NotImplementedException();
    }

    public void AddRight(String Value)
    {
        throw new NotImplementedException();
    }

    public void AddRightExpression(IExpression Expression)
    {
        throw new NotImplementedException();
    }
}