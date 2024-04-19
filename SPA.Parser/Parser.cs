using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using SPA.Domain.Models;
using static System.Net.Mime.MediaTypeNames;

namespace SPA.Parser;

public class Parser
{
    public static MainProcedure Parse(string filePath)
    {
        var lines = File.ReadLines(filePath).ToList();

        if (!lines.Any())
        {
            return null;
        }

        MainProcedure mainProcedure = null;
        for (int i = 0; i < lines.Count; i++)
        {
            if (lines[i].StartsWith("procedure"))
            {
                var regex = new Regex(RegexRules.Procedure.Name);
                var match = regex.Match(lines[i]);
                i++;

                var statementsList = ParseStatementsList(lines, ref i);
                
                if (mainProcedure == null)
                {
                    mainProcedure = new MainProcedure { Name = match.Groups[1].Value, StatementsList = statementsList };
                }
                else
                {
                    mainProcedure.Procedures.Add(new Procedure { Name = match.Groups[1].Value, StatementsList = statementsList, Parent = mainProcedure});
                }
            }
        }

        return mainProcedure;
    }

    public static StatementsList ParseStatementsList(List<string> lines, ref int index)
    {
        StatementsList statementsList = new StatementsList();
        for (int i = index; i < lines.Count; i++)
        {
            var result = ParseStatement(lines, ref index);
            statementsList.Statements.Add(result);
            
            if (lines[i].Contains('}') || index == lines.Count - 1)
            {
                break;
            }
            
            index++;
        }

        return statementsList;
    }

    public static IStatement ParseStatement(List<string> lines, ref int index)
    {
        var isAssign = Regex.Match(lines[index], RegexRules.Statement.Assign);
        if (isAssign.Success)
        {
            return new Assign
            {
                LineNumber = int.Parse(isAssign.Groups[1].Value),
                VariableName = isAssign.Groups[2].Value,
                Expression = ParseExpression(isAssign.Groups[3].Value)
            };
        }
        
        var isWhile = Regex.Match(lines[index], RegexRules.Statement.While);
        if (isWhile.Success)
        {
            var newWhile = new While
            {
                LineNumber = int.Parse(isWhile.Groups[1].Value),
                VariableName = isWhile.Groups[2].Value
            };

            index++;
            var statementsList = ParseStatementsList(lines, ref index);

            foreach (var statement in statementsList.Statements)
            {
                statement.Parent = statementsList;
            }

            newWhile.StatementsList = statementsList;

            return newWhile;
        }
        
        // Not required for iteration 1
        // var isIf = Regex.Match(lines[index], RegexRules.Statement.If);
        // if (isIf.Success)
        // {
        //     return new If
        //     {
        //         LineNumber = int.Parse(isIf.Groups[1].Value),
        //         VariableName = isIf.Groups[2].Value
        //     };
        // }

        return default;
    }

    //Expression like this
    //3. d = t + a - 2 * r;

    //will be parsed similar to this

    //                    assign
    //                    /       \
    //                   d         +
    //                          /     \
    //                        t        -
    //                               /    \
    //                              a      *
    //                                   /    \
    //                                  2      r
    // where assign, +, -, * are IExpressions

    public static IExpression ParseExpression(string line)
    {

        string ClearedExpression = ClearLine(line); // clear line from signs
        int SymbolIndex = 0;

        IExpression MainExpression = null;
        List<string> Symbols = new List<string>();
        List<string> Values = new List<string>();
        List<IExpression> ExpressionTree = new List<IExpression>();

        Values = ClearedExpression.Split(" ").Where(s => !string.IsNullOrWhiteSpace(s)).ToList(); // split cleared into values list ( letters & numbers)

        foreach (char Sign in line) // add every +, -, * to symbols list
        {
            if (Sign == '+' || Sign == '-' || Sign == '*')
            {
                Symbols.Add(Sign.ToString());
            }
        }

        if (Symbols.Count > 0 && Values.Count > 1) //parse more complex expressions fe. x = 1+3 - 2 *g
        {
            for (int i = 0; i < Values.Count + 1 / 2; i += 2)
            {

                CreateExpression(ref ExpressionTree, ref i, Symbols[SymbolIndex], Values[i], Values[i+1], ref SymbolIndex, Symbols.Count);

            }

        }
        else // if line is only assignment fe. x = -9 or y= 69
        {
            IExpression expression = CreateSign(Symbols[0]);

            expression.AddLeft(Values[0]);
            ExpressionTree.Add(expression);
        }


        for (int i = ExpressionTree.Count - 1; i >= 0; i--) // create tree from expressions list
        {
            if (ExpressionTree.Count > 1)
            {
                ExpressionTree[i - 1].AddRightExpression(ExpressionTree[i]);
                ExpressionTree.RemoveAt(i);
            }
            else
            {
                MainExpression = ExpressionTree[0];
            }
        }

        return MainExpression;
    }

    private static IExpression CreateSign(string symbol)
    {

        switch (symbol)
        {
            case "+":
                return new Plus();
            case "-":
                return new Minus();
            case "*":
                return new Times();
            default:
                return new Plus();
        }
    }

    private static string ClearLine(string line)
    {
        return line
            .Replace(";", "")
            .Replace("+", " ")
            .Replace("-", " ")
            .Replace("*", " ")
            .Replace("}", " ");
    }

    private static void CreateExpression(ref List<IExpression> ExpressionsTree, ref int Index, string Sign, string ValueLeft, string ValueRight, ref int SymbolIndex, int SymbolsCount)
    {
        IExpression expression = CreateSign(Sign); //decide expression type

        if (SymbolIndex + 1 == SymbolsCount) // if its last expression in tree, right and left have values
        {
            expression.AddLeft(ValueLeft);
            expression.AddRight(ValueRight);
            ExpressionsTree.Add(expression);
        }
        else //basic expression, value in left, right is null, value will be added later
        {
            expression.AddLeft(ValueLeft);
            ExpressionsTree.Add(expression);
            Index -= 1;
        }

        if (SymbolIndex + 1 < SymbolsCount)
        {
            SymbolIndex++;
        }
    }

}