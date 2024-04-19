namespace SPA.Domain.Models;

public class While : IStatement
{
    public string VariableName { get; set; }
    
    public StatementsList StatementsList { get; set; }
    
    public int LineNumber { get; set; }
    
    public StatementsList Parent { get; set; }
}