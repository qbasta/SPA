namespace SPA.Domain.Models;

// L * R
public class Times : IExpression
{
    public String Left { get; set; }

    public IExpression Right { get; set; }

    public String RightValue { get; set; }

    public void AddLeft(String Value)
    {
        this.Left = Value;
    }

    public void AddRight(String Value)
    {
        this.RightValue = Value;
    }

    public void AddRightExpression(IExpression Expression)
    {
        this.Right = Expression;
    }
}