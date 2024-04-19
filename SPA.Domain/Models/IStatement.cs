namespace SPA.Domain.Models;

public interface IStatement
{
    public int LineNumber { get; set; }
    
    public StatementsList Parent { get; set; }
}