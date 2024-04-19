using FluentAssertions;
using SPA.QueryEvaluator.Models;

namespace SPA.QueryEvaluator.Tests;

public class HelperTests
{
    [Fact]
    public void DeclarationTest1()
    {
        var declaration = "stmt s1, s2;";
        var expected = new List<Variable>
        {
            new Variable { Type = VariableType.Stmt, Name = "s1" },
            new Variable { Type = VariableType.Stmt, Name = "s2" }
        };

        var actual = PQLHelper.ParseDeclarations(declaration);

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void DeclarationTest_FromIteration1Description()
    {
        var declaration = "stmt s1, s2; assign a; while w;";
        var expected = new List<Variable>
        {
            new Variable { Type = VariableType.Stmt, Name = "s1" },
            new Variable { Type = VariableType.Stmt, Name = "s2" },
            new Variable { Type = VariableType.Assign, Name = "a" },
            new Variable { Type = VariableType.While, Name = "w" }
        };

        var actual = PQLHelper.ParseDeclarations(declaration);

        actual.Should().BeEquivalentTo(expected);
    }
}