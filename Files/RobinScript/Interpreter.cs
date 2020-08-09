using System;

namespace RobinScript
{
    class Interpreter
    {
        public static Bytecode Info = new Bytecode();
        public static void Compute(Bytecode BTTable)
        {
            for (int i = 0; i < BTTable.Current.Length; i++) {
                Info = BTTable;
            }
        }
    }
}
