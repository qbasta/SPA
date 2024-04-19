using FluentAssertions;

namespace SPA.QueryEvaluator.Tests;

public class ModifiesTests
{
    [Theory]
    [InlineData("stmt s1;", "Select s1 such that Modifies (s1, \"a\")", new int[] { 2, 9 })]
    [InlineData("while w;", "Select w such that Modifies (w, \"d\")", new int[] { 6 })]
    public void Modifies_ShouldFind(string declaration, string query, int[] expected)
    {
        var pkb = PKBGenerator.GetExamplePKB2();
        var processor = new PQLProcessor(pkb);
        var result = processor.RunQuery(declaration, query);

        result.Should().Equal(expected);
    }
}