namespace SPA.Domain.Models;

public class Call : IStatement
{
    public string ProcedureName { get; set; }
    
    public int LineNumber { get; set; }
    
    public StatementsList Parent { get; set; }
}