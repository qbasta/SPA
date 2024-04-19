namespace SPA.Domain.Models;

public class If : IStatement
{
    public string VariableName { get; set; }
    
    public StatementsList IfStatementsList { get; set; }
    
    public StatementsList ElseStatementsList { get; set; }
    
    public int LineNumber { get; set; }
    
    public StatementsList Parent { get; set; }
}