using SPA.Domain.Models;

namespace SPA.QueryEvaluator.Tests;

internal static class PKBGenerator
{
    public static MainProcedure GetExamplePKB1() => new MainProcedure
    {
        Procedures = new List<Procedure>
        {
            new Procedure
            {
                Name = "Circle",
                StatementsList = new StatementsList
                {
                    Statements = new List<IStatement>
                    {
                        new Assign(1, "t", new Factor(1)),
                        new Assign(2, "a", new Plus { Left = "t", RightValue = "1" }),
                        new Assign(3, "d", new Plus { Left = "t", Right = new Plus { Left = "a", RightValue = "2" } }),
                        new Assign(4, "b", new Plus { Left = "t", RightValue = "a" }),
                    }
                }
            }
        }
    };

    /*
    procedure Circle {
    1. t = 1;
    2. a = t + 10;
    3. d = t + a + 2;
    4. b = t + a;
    5. b = t + a;
    6. while c {
    7. d = d + t;
    8. c = d + 1; }
    9. a = d + t;
    10. c = c + 1; }
    */
    public static MainProcedure GetExamplePKB2() => new MainProcedure
    {
        Procedures = new List<Procedure>
        {
            new Procedure
            {
                Name = "Circle",
                StatementsList = new StatementsList
                {
                    Statements = new List<IStatement>
                    {
                        new Assign(1, "t", new Factor(1)),
                        new Assign(2, "a", new Plus { Left = "t", RightValue = "1" }),
                        new Assign(3, "d", new Plus { Left = "t", Right = new Plus { Left = "a", RightValue = "2" } }),
                        new Assign(4, "b", new Plus { Left = "t", RightValue = "a" }),
                        new Assign(5, "b", new Plus { Left = "t", RightValue = "a" }),
                        new While
                        {
                            LineNumber = 6,
                            VariableName = "c",
                            StatementsList = new StatementsList
                            {
                                Statements = new List<IStatement>
                                {
                                    new Assign(7, "d", new Plus { Left = "d", RightValue = "t" }),
                                    new Assign(8, "c", new Plus { Left = "d", RightValue = "1" })
                                }
                            }
                        },
                        new Assign(9, "a", new Plus { Left = "d", RightValue = "t" }),
                        new Assign(10, "c", new Plus { Left = "c", RightValue = "1" })
                    }
                }
            }
        }
    };


    /*
    procedure Circle {
    1. t = 1;
    2. a = t + 10;
    3. d = t + a + 2;
    4. b = t + a;
    5. b = t + a;
    6. while c {
    7. d = d + t;
    8. c = d + 1; }
    9. a = d + t;
    10. c = c + 1;
    11. while a {
    12. d = d + t;
    13. while c {
    14. d = d + t;}
    15. c = d + 1; }}
    */
    public static MainProcedure GetExamplePKB3() => new MainProcedure
    {
        Procedures = new List<Procedure>
        {
            new Procedure
            {
                Name = "Circle",
                StatementsList = new StatementsList
                {
                    Statements = new List<IStatement>
                    {
                        new Assign(1, "t", new Factor(1)),
                        new Assign(2, "a", new Plus { Left = "t", RightValue = "1" }),
                        new Assign(3, "d", new Plus { Left = "t", Right = new Plus { Left = "a", RightValue = "2" } }),
                        new Assign(4, "b", new Plus { Left = "t", RightValue = "a" }),
                        new Assign(5, "b", new Plus { Left = "t", RightValue = "a" }),
                        new While
                        {
                            LineNumber = 6,
                            VariableName = "c",
                            StatementsList = new StatementsList
                            {
                                Statements = new List<IStatement>
                                {
                                    new Assign(7, "d", new Plus { Left = "d", RightValue = "t" }),
                                    new Assign(8, "c", new Plus { Left = "d", RightValue = "1" })
                                }
                            }
                        },
                        new Assign(9, "a", new Plus { Left = "d", RightValue = "t" }),
                        new Assign(10, "c", new Plus { Left = "c", RightValue = "1" }),
                        new While
                        {
                            LineNumber = 11,
                            VariableName = "a",
                            StatementsList = new StatementsList
                            {
                                Statements = new List<IStatement>
                                {
                                    new Assign(12, "d", new Plus { Left = "d", RightValue = "t" }),
                                    new While
                                    {
                                        LineNumber = 13,
                                        VariableName = "c",
                                        StatementsList = new StatementsList
                                        {
                                            Statements = new List<IStatement>
                                            {
                                                new Assign(14, "d", new Plus { Left = "d", RightValue = "t" }),
                                            }
                                        }
                                    },
                                    new Assign(15, "c", new Plus { Left = "d", RightValue = "1" })
                                }
                            }
                        }
                    }
                }
            }
        }
    };
}