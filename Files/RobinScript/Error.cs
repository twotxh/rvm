using System;

namespace RobinScript
{
    [Serializable]
    class Error : Exception
    {
        public Error(string error, string tip)
        {
            string istruction = Interpreter.ExternBTTable.LineIstruction + '\n';
            for (int i = 0; i < Interpreter.ExternBTTable.LineNumber.ToString().Length + 3; i++)
                istruction += ' ';
            for (int j = 0; j < istruction.Length; j++) {
                istruction += '^';
            }
            istruction += " -> ";
            Console.WriteLine(Interpreter.ExternBTTable.LineNumber + " | " + istruction + error + "\nTip: " + tip);
        }
    }
}