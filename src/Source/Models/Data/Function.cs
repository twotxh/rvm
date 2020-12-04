﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        public object FindArgument(byte index)
        {
            if (Arguments is null)
                BasePanic.Throw($"Insufficient function arguments, have not been passed {index+1} argument/s", "Runtime");
            if (index < 0)
                BasePanic.Throw("Can not index function argument with a negative index", "Runtime");
            if (index < Arguments.Length)
                return Arguments[index];
            BasePanic.Throw($"Insufficient function arguments, have not been passed {index+1} argument/s", "Runtime");
            return null;
        }
        public void PassArguments(object[] arguments)
        {
            Arguments = arguments;
        }
        Dictionary<string, int> Labels;
        object[] Arguments;
        public Instruction[] Instructions;
    }
}