using FluentAssertions;

namespace SPA.QueryEvaluator.Tests;

public class FollowsTests
{
    [Theory]
    [InlineData("stmt s1, s2;", "Select s1 such that Follows (s1, s2)", new int[] { 1, 2, 3 })]
    [InlineData("stmt s1, s2;", "Select s2 such that Follows (s1, s2)", new int[] { 2, 3, 4 })]
    [InlineData("stmt s1;", "Select s1 such that Follows (2, s1)", new int[] { 3 })]
    [InlineData("stmt s1;", "Select s1 such that Follows (s1, 4)", new int[] { 3 })]
    public void Follows1_ShouldFind(string declaration, string query, int[] expected)
    {
        var pkb = PKBGenerator.GetExamplePKB1();
        var processor = new PQLProcessor(pkb);
        var result = processor.RunQuery(declaration, query);

        result.Should().Equal(expected);
    }

    [Theory]
    [InlineData("stmt s1; while w", "Select s1 such that Follows (s1, w)", new int[] { 5 })]
    [InlineData("stmt s1; while w", "Select w such that Follows (s1, w)", new int[] { 6 })]
    public void Follows2_ShouldFind(string declaration, string query, int[] expected)
    {
        var pkb = PKBGenerator.GetExamplePKB2();
        var processor = new PQLProcessor(pkb);
        var result = processor.RunQuery(declaration, query);

        result.Should().Equal(expected);
    }

    [Theory]
    [InlineData("stmt s1;", "Select s1 such that Follows* (2, s1)", new int[] { 3, 4 })]
    [InlineData("stmt s1;", "Select s1 such that Follows* (1, s1)", new int[] { 2, 3, 4 })]
    [InlineData("stmt s1;", "Select s1 such that Follows* (1, s1)", new int[] { 2, 3, 4 })]
    public void FollowsT1_ShouldFind(string declaration, string query, int[] expected)
    {
        var pkb = PKBGenerator.GetExamplePKB1();
        var processor = new PQLProcessor(pkb);
        var result = processor.RunQuery(declaration, query);

        result.Should().Equal(expected);
    }

    [Theory]
    [InlineData("stmt s1; while w", "Select s1 such that Follows* (4, s1)", new int[] { 5, 6, 9, 10 })]
    [InlineData("stmt s1; while w", "Select w such that Follows* (4, w)", new int[] { 6 })]
    [InlineData("stmt s1; while w", "Select w such that Follows* (w, 9)", new int[] { 6 })]
    public void FollowsT2_ShouldFind(string declaration, string query, int[] expected)
    {
        var pkb = PKBGenerator.GetExamplePKB2();
        var processor = new PQLProcessor(pkb);
        var result = processor.RunQuery(declaration, query);

        result.Should().Equal(expected);
    }
}