namespace SPA.Domain.Models;

public class Factor : IExpression
{
    public Factor(int value)
    {
        Value = value;
    }

    public int Value { get; set; }

    public void AddLeft(String Value)
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