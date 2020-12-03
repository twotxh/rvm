using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RobinVM.Models
{
    public struct Function
    {
        public static Function New(Instruction[] instructions) => new Function(instructions);
        public Function(Instruction[] instructions)
        {
            Instructions = instructions;
            Arguments = null;
            Labels = new Dictionary<string, int>();
        }
        public void AddLabel(string label, int instructionIndex)
        {
            if (!Labels.TryAdd(label, instructionIndex))
                BasePanic.Throw($"Already define label `{label}` as {Labels[label]}: `{Instructions[Labels[label]].FunctionPointer.Method.Name}`", "PreRuntime");
        }
        public int FindLabel(string label)
        {
            if (Labels.TryGetValue(label, out int ret))
                return ret;
            BasePanic.Throw($"Undefine label `{label}`", "Runtime");
            return 0;
        }
        public bool UninstantiatedLabels()
        {
            return Labels == null;
        }
        Dictionary<string, int> Labels;
        public object[] Arguments;
        public Instruction[] Instructions;
    }
}