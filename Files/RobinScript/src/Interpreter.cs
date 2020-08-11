using System;

namespace RobinScript
{
    class Interpreter
    {
        public static int i;
        public static Storage RandomAccessMemory = new Storage();
        public static Block Block = null;
        public static void Compute(Bytecode BTTable)
        {
            for (i = 0; i < BTTable.Current.Count; i++) {
                Block = BTTable.Current[i];
                switch (Block.Istruction) {
                    case Istructions.CALL:
                        Runtime.CALL();
                        break;
                    case Istructions.STORE:
                        Runtime.STORE();
                        break;
                    case Istructions.JUMP:
                        Runtime.JUMP();
                        break;
                    case Istructions.EVAL:
                        Runtime.EVAL();
                        break;
                }
            }
        }
    }
    static class Runtime
    {
        public static void _goto(string istructionNumber)
        {
            Interpreter.i = int.Parse(istructionNumber.Substring(1, istructionNumber.Length - 2));
        }
        public static void CALL()
        {

        }
        public static void STORE()
        {
        }
        public static void JUMP()
        {
        }
        public static void EVAL()
        {
        }
    }
}