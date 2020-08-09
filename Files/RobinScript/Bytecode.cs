using System;
using System.Collections.Generic;

namespace RobinScript
{
    public enum Istructions {
        Return,
    }
    public class Bytecode
    {
        private static Dictionary<string, Istructions> ToIstruction = new Dictionary<string, Istructions>()
        {
            { "Return", Istructions.Return },
            // finish all
        };
        public Block[] Current { get; set; } = { };
        private int BlockNumber = 0;
        public void Append(string lineNumber, Istructions istruction, string referedIstructionNumber, string[] arguments)
        {
            Current[BlockNumber].IstructionNumber = int.Parse(lineNumber);
            Current[BlockNumber].Istruction = istruction;
            Current[BlockNumber].Reference = int.Parse(referedIstructionNumber);
            Current[BlockNumber].Arguments = arguments;
            BlockNumber++;
        }
        public static Bytecode ToBytecode(string bytecode)
        {
            string[] code = bytecode.Split(new char[] { '\n', '\r' });
            Bytecode BTTable = new Bytecode();
            for (int i = 0; i < code.Length; i++) {
                string[] arguments = { };
                for (int j = 2; j < code[i].Split(' ').Length; j++) {
                    arguments[j - 2] = code[i].Split(' ')[j];
                }
                BTTable.Append(code[i].Split(' ')[0], ToIstruction[code[i].Split(' ')[1]], code[i].Split(' ')[2], arguments);
            }
            return BTTable;
        }
        public Block ElementAt(int index)
        {
            return Current[index];
        }
        public override string ToString()
        {
            System.Text.StringBuilder toReturn = new System.Text.StringBuilder();
            for (int i = 0; i < Current.Length; i++) {
                string tmp = "";
                for (int j = 0; j < Current[i].Arguments.Length; j++)
                    tmp += Current[i].Arguments[j] + " :: ";
                toReturn.AppendLine($"{Current[i].IstructionNumber}\t{Current[i].Istruction}\t{Current[i].Reference}\t{tmp}");
            }
            return toReturn.ToString();
        }
    }
    public class Block
    {
        public int IstructionNumber { get; set; }
        public Istructions Istruction { get; set; }
        public int Reference { get; set; }
        public object[] Arguments { get; set; }
    }
}