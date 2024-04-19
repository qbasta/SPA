using System.Text.RegularExpressions;
using FluentAssertions;
using SPA.Domain.Models;

namespace SPA.Parser.Tests;

public class RegexRulesTest
{
    [Theory]
    [InlineData("procedure Circle {", "Circle")]
    [InlineData("procedure  Rectangle       {", "Rectangle")]
    [InlineData("procedure   Triangle {", "Triangle")]
    [InlineData("procedure Hexagon   {", "Hexagon")]
    public void Parse_Procedure_Name(string input, string expectedOutput)
    {
        var regex = new Regex(RegexRules.Procedure.Name);
        var match = regex.Match(input);

        match.Groups[1].Value.Should().Be(expectedOutput);
    }
    
    [Theory]
    [InlineData("4. call Triangle;", 4, "Triangle")]
    [InlineData("26.  call    Hexagon;)", 26, "Hexagon")]
    public void Parse_Statement_Call(string input, int expectedLineNumber, string expectedProcedureName)
    {
        var regex = new Regex(RegexRules.Statement.Call);
        var match = regex.Match(input);

        match.Groups[1].Value.Should().Be(expectedLineNumber.ToString());
        match.Groups[2].Value.Should().Be(expectedProcedureName);
    }
    
    [Theory]
    [InlineData("8. if t then {", 8, "t")]
    [InlineData("24. if xd then {", 24, "xd")]
    public void Parse_Statement_If(string input, int expectedLineNumber, string expectedVariableName)
    {
        var regex = new Regex(RegexRules.Statement.If);
        var match = regex.Match(input);

        match.Groups[1].Value.Should().Be(expectedLineNumber.ToString());
        match.Groups[2].Value.Should().Be(expectedVariableName);
    }
    
    [Theory]
    [InlineData("18. while c {", 18, "c")]
    [InlineData("1. while xddd {", 1, "xddd")]
    public void Parse_Statement_While(string input, int expectedLineNumber, string expectedVariableName)
    {
        var regex = new Regex(RegexRules.Statement.While);
        var match = regex.Match(input);

        match.Groups[1].Value.Should().Be(expectedLineNumber.ToString());
        match.Groups[2].Value.Should().Be(expectedVariableName);
    }
    
    [Theory]
    [InlineData("19. t = d + 3 * a + c;", 19, "t", "d + 3 * a + c;")]
    [InlineData("26. a = t *a + d +  k*b; }}", 26, "a", "t *a + d +  k*b; }}")]
    public void Parse_Statement_Assign(string input, int expectedLineNumber, string expectedVariableName, string expectedExpression)
    {
        var regex = new Regex(RegexRules.Statement.Assign);
        var match = regex.Match(input);

        match.Groups[1].Value.Should().Be(expectedLineNumber.ToString());
        match.Groups[2].Value.Should().Be(expectedVariableName);
        match.Groups[3].Value.Should().Be(expectedExpression);
    }
}