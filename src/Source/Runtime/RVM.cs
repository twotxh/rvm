using System;
using System.Diagnostics;
public static class Rvm {
    /// <summary>
    /// Performs first label of <paramref name="labels"/>
    /// </summary>
    /// <param name="labels">Functions</param>
    public static void Execute(Group[] labels) {
        Console.Title = "Robin";
        Runtime.Labels = labels;
        ExecuteLabel(labels[0]);
    }
    public static void ExecuteLabel(Group label) {
        Storage x0 = Runtime.storage;
        int x1 = Runtime.InstructionIndex;
        Runtime.storage = new Storage();
        for (Runtime.InstructionIndex = 0; Runtime.InstructionIndex < label.Instructions.Length; Runtime.InstructionIndex++)
            label.Instructions[Runtime.InstructionIndex].instruction(label.Instructions[Runtime.InstructionIndex].arguments);
        Runtime.storage = x0;
        Runtime.InstructionIndex = x1;
    }
}