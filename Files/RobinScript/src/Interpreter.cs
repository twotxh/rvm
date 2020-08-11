using System;

namespace RobinScript
{
    class Interpreter
    {
        static int i;
        public static Storage RandomAccessMemory = new Storage();
        public static void Compute(Bytecode BTTable)
        {
            for (i = 0; i < BTTable.Current.Count; i++) {
                Block Block = BTTable.Current[i];
                switch (Block.Istruction) {
                    case Istructions.GIVEP:
                        break;
                    case Istructions.CALL:
                        break;
                    case Istructions.STORE:
                        break;
                }
            }
        }
        public static void Goto(int istructionNumber)
        {
            i = istructionNumber;
        }
    }
}