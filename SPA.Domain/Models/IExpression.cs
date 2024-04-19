namespace SPA.Domain.Models;

public interface IExpression
{
    public void AddLeft(String Value);

    public void AddRightExpression(IExpression Expression);

    public void AddRight(String Value);
}