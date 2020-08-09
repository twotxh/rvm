using System;
using System.IO;
namespace RobinScript
{
    class Tools
    {
        public static Bytecode Compile(string Path)
        {
            return Parser.Parse(Lexer.Lex(File.ReadAllLines(Path)));
        }
        public static void CompileRun(string Path)
        {
            Interpreter.Compute(Compile(Path));
        }
        public static void CompileRuntime(string Line)
        {
            Interpreter.Compute(Parser.Parse(Lexer.Lex(new string[] { Line })));
        }
    }
}
