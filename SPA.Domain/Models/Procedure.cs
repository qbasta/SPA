namespace SPA.Domain.Models;

public class Procedure
{
    public string Name { get; set; }
    
    public StatementsList StatementsList { get; set; } = new();

    public MainProcedure Parent { get; set; }
}