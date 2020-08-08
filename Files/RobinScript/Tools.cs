using System;

namespace RobinScript
{
    class Tools
    {
        public static Bytecode Compile(string Path)
        {
            Bytecode BTTable = new Bytecode();
            return BTTable;
        }
        public static void CompileRun(string Path)
        {
            Compile(Path);
        }
    }
}
