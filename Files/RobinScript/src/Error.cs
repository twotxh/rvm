using System;

namespace RobinScript
{
    [Serializable]
    class Error : Exception
    {
        public Error(string error, string tip)
        {
            string istruction = Lexer.CurrentLine+ '\n';
            for (int i = 0; i < Lexer.CurrentLineNum.ToString().Length + 3; i++)
                istruction += ' ';
            for (int j = 0; j < Lexer.CurrentLine.Length; j++) {
                istruction += '^';
            }
            istruction += " -> ";
            Console.WriteLine(Lexer.CurrentLineNum + " | " + istruction + error + "\nTip: " + tip);
        }
    }
}