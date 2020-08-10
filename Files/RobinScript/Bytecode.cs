using System;
using System.Collections.Generic;

namespace RobinScript
{
    public enum Istructions {
        GIVEP,
        CALL,
        STORE,
        JUMP,
        EVAL,
        TAKE,

    }
    public class Bytecode
    {
        public List<Block> Current = new List<Block>();
        public void Append(string lineNumber, Istructions istruction, string[] arguments)
        {
            //Current.Add(new Block() { IstructionNumber = int.Parse(lineNumber), Arguments = arguments, Istruction = istruction });
        }
        public static Bytecode Parse(string bytecode)
        {
            string[] code = bytecode.Split(new char[] { '\n', '\r' });
            Bytecode BTTable = new Bytecode();
            for (int i = 0; i < code.Length; i++) {
                if (string.IsNullOrWhiteSpace(code[i])) continue;
                string text = code[i].Substring(code[i].IndexOf("\t")+1);
                string[] arguments = new string[10];
                bool isInterpolate = false;
                int count = 0;
                for (int j = 0; j < text.Length; j++) {
                    if (text[j] == '\'') {
                        isInterpolate = (isInterpolate) ? false : true;
                        arguments[count] += '\'';
                    }
                    else if (isInterpolate) {
                        arguments[count] += text[j];
                    } else if (text[j] == ' ' && !isInterpolate) {
                        if (!string.IsNullOrWhiteSpace(arguments[count]))
                            count++;
                    } else {
                        arguments[count] += text[j];
                    }
                }
                BTTable.Append(code[i].Split(' ')[0], (Istructions) Enum.Parse(typeof(Istructions), code[i].Split('\t')[0].Split(' ')[1].ToUpper()), arguments);
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
            for (int i = 0; i < Current.Count; i++) {
                string tmp = "";
                for (int j = 0; j < Current[i].Arguments.Length; j++) {
                    tmp += Current[i].Arguments[j]+" ";
                }
                toReturn.AppendLine($"{Current[i].IstructionNumber} {Current[i].Istruction}\t{tmp}");
            }
            return toReturn.ToString();
        }
    }
    public class Block
    {
        public int IstructionNumber { get; set; }
        public Istructions Istruction { get; set; }
        public object[] Arguments { get; set; }
    }
}