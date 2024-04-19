using FluentAssertions;

namespace SPA.QueryEvaluator.Tests;

public class ParentTests
{
    [Theory]
    [InlineData("stmt s1, s2;", "Select s1 such that Parent (s1, s2)", new int[] { 6 })]
    [InlineData("stmt s1, s2;", "Select s2 such that Parent (s1, s2)", new int[] { 7, 8 })]
    [InlineData("stmt s; while w;", "Select s such that Parent (w, s)", new int[] { 7, 8 })]
    [InlineData("stmt s; while w;", "Select w such that Parent (w, s)", new int[] { 6 })]
    [InlineData("stmt s;", "Select s such that Parent (6, s)", new int[] { 7, 8 })]
    public void Parent_ShouldFind(string declaration, string query, int[] expected)
    {
        var pkb = PKBGenerator.GetExamplePKB2();
        var processor = new PQLProcessor(pkb);
        var result = processor.RunQuery(declaration, query);

        result.Should().Equal(expected);
    }
    
    [Theory]
    [InlineData("stmt s1, s2;", "Select s1 such that Parent* (s1, s2)", new int[] { 6, 11, 13 })]
    [InlineData("stmt s1, s2;", "Select s2 such that Parent* (s1, s2)", new int[] { 7, 8, 12, 13, 14, 15 })]
    [InlineData("stmt s; while w;", "Select s such that Parent* (w, s)", new int[] { 7, 8, 12, 13, 14, 15 })]
    [InlineData("stmt s; while w;", "Select w such that Parent* (w, s)", new int[] { 6, 11, 13 })]
    public void ParentT_ShouldFind(string declaration, string query, int[] expected)
    {
        var pkb = PKBGenerator.GetExamplePKB3();
        var processor = new PQLProcessor(pkb);
        var result = processor.RunQuery(declaration, query);

        result.Should().Equal(expected);
    }
}