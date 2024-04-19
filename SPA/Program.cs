using SPA.Domain.Models;

namespace SPA;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        string path = "input_iteration_1.txt";

        //string path = "/Users/lukaszczapkowski/ATS/SPA/SPA/bin/input_iteration_1.txt"; nie usuwajcie tego bo mi sie nie chce zmieniac za kazdym razem

        Procedure mainProcedure = Parser.Parser.Parse(path);
        
        Console.WriteLine("Ready");
    }
}