using System;
using System.Diagnostics;
class Rvm {
    public static void Execute(Group[] labels) {
        Runtime.Labels = labels;
        ExecuteLabel(labels[0]);
    }
    public static void ExecuteLabel(Group label) {
        for (Runtime.InstructionIndex = 0; Runtime.InstructionIndex < label.Instructions.Length; Runtime.InstructionIndex++) {

            label.Instructions[Runtime.InstructionIndex](label.Arguments[Runtime.InstructionIndex]);
        }
    }
}