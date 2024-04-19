namespace SPA.Parser;

public static class RegexRules
{
    public static class Procedure
    {
        public static string Name = @"procedure[ ]{1,}(\w+)[ ]{1,}{";
    }

    public static class Statement
    {
        public static string Call = @"(\d+).[ ]{1,}call[ ]{1,}(\w+);";

        public static string If = @"(\d+).[ ]{1,}if[ ]{1,}(\w+)[ ]{1,}then[ ]{1,}{";

        public static string While = @"(\d+).[ ]{1,}while[ ]{1,}(\w+)[ ]{1,}{";

        public static string Assign = @"(\d+).[ ]{1,}(\w+)[ ]{1,}=[ ]{1,}(.+)";
    }

    public static class Expression
    {
        public static string Symbol = @"([\+\*\-])";

        public static string VariableName = @"([a-zA-Z]{1}\w*)";

        public static string Value = @"(\d+)";
    }
}