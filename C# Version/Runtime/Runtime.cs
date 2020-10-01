using System;
using System.Diagnostics;
using System.Collections.Generic;
public static class Runtime {
    public delegate void runtime(dynamic args);
    public delegate void builtinCall();
    public static Storage storage = new Storage();
    public static int InstructionIndex = 0;
    public static Group[] Labels;
    public static Stack<dynamic> Stack = new Stack<dynamic>();
    public static void Store(dynamic args) => storage[args] = Stack.Pop();
    public static void Call(dynamic args) => Rvm.ExecuteLabel(Labels[args]);
    public static void Load(dynamic args) => Stack.Push(args);
    public static void StoreElementIntoArray(dynamic args) {
        dynamic arr = Stack.Pop();
        arr[Stack.Pop()] = Stack.Pop();
        Stack.Push(arr);
    }
    public static void LoadElementFromArray(dynamic args) => Stack.Push(Stack.Pop()[Stack.Pop()]);
    public static void Clear(dynamic args) => Stack.Clear();
    public static void LoadFromStorage(dynamic args) => Stack.Push(storage[args]);
    public static void Add(dynamic args) => Stack.Push(Stack.Pop() + Stack.Pop());
    public static void Sub(dynamic args) => Stack.Push(Stack.Pop() - Stack.Pop());
    public static void Div(dynamic args) => Stack.Push(Stack.Pop() / Stack.Pop());
    public static void Mul(dynamic args) => Stack.Push(Stack.Pop() * Stack.Pop());
    public static void Pow(dynamic args) => Stack.Push(Math.Pow(Stack.Pop(), Stack.Pop()));
    public static void RvmOutput(dynamic args) => Console.Write(Stack.Pop());
    public static void RvmInput(dynamic args) => Stack.Push(Console.ReadLine());
    public static void RvmShell(dynamic args) => Process.Start(new ProcessStartInfo() { FileName = "cmd", Arguments = "/C " + Stack.Pop(), UseShellExecute = false });
    public static void RvmCall(dynamic args) => Stack.Pop()();
    public static void Pop(dynamic args) => Stack.Pop();
    public static void Return(dynamic args) => InstructionIndex = 10000000;
    public static void CompareEQ(dynamic args) => Stack.Push(Stack.Pop() == Stack.Pop());
    public static void CompareGreater(dynamic args) => Stack.Push(Stack.Pop() > Stack.Pop());
    public static void CompareLess(dynamic args) => Stack.Push(Stack.Pop() < Stack.Pop());
    public static void CompareNEQ(dynamic args) => Stack.Push(Stack.Pop() != Stack.Pop());
    public static void End(dynamic args) => Environment.Exit(0);
    public static void JumpTrue(dynamic args) {
        if (Stack.Pop())
            InstructionIndex = args - 1;
    }
    public static void JumpFalse(dynamic args) {
        if (!Stack.Pop())
            InstructionIndex = args - 1;
    }
    public static void Jump(dynamic args) => InstructionIndex = args-1;
}