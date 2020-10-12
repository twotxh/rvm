using System;
using System.Diagnostics;
using System.Collections.Generic;
public static class Runtime {
    public delegate void runtime(dynamic args);
    public static Storage storage = new Storage();
    public static int InstructionIndex = 0;
    public static Group[] Labels;
    public static readonly Stack<dynamic> Stack = new Stack<dynamic>();
    public static readonly List<string> Functions = new List<string>();

    /// <summary>
    /// Casts last element onto the stack to int32 and push result
    /// </summary>
    /// <param name="args"></param>
    public static void CastToInt(dynamic args) => Stack.Push(Convert.ToInt32(Stack.Pop()));

    /// <summary>
    /// Casts last element onto the stack to int32 and push result
    /// </summary>
    /// <param name="args"></param>
    public static void Cast(dynamic args) => Stack.Push(Convert.ChangeType(Stack.Pop(), args));

    /// <summary>
    /// Casts last element onto the stack to float and push result
    /// </summary>
    /// <param name="args"></param>
    public static void CastToFloat(dynamic args) => Stack.Push(Convert.ToSingle(Stack.Pop()));

    /// <summary>
    /// Casts last element onto the stack to bool and push result
    /// </summary>
    /// <param name="args"></param>
    public static void CastToBool(dynamic args) => Stack.Push(Convert.ToBoolean(Stack.Pop()));

    /// <summary>
    /// Stores the value onto the stack in the local heap
    /// </summary>
    /// <param name="args">Index of the local heap into store last stack element</param>
    public static void Store(dynamic args) => storage[args] = Stack.Pop();

    /// <summary>
    /// Calls a function
    /// </summary>
    /// <param name="args">Index of function to call</param>
    public static void Call(dynamic args) => Rvm.ExecuteLabel(Labels[Functions.IndexOf(args)]);

    /// <summary>
    /// Loads onto the stack a constant
    /// </summary>
    /// <param name="args">Constant to load onto the stack</param>
    public static void Load(dynamic args) => Stack.Push(args);

    /// <summary>
    /// Takes last element on the stack as array, the second last as index of the element to change and tird last as value to replace with,
    /// changes the value and push it onto the stack
    /// </summary>
    /// <param name="args"></param>
    public static void StoreElementIntoArray(dynamic args) {
        dynamic arr = Stack.Pop();
        arr[Stack.Pop()] = Stack.Pop();
        Stack.Push(arr);
    }

    /// <summary>
    /// Takes last element on the stack as array, the second last as index of the element to get and
    /// pushes it onto the stack
    /// </summary>
    /// <param name="args"></param>
    public static void LoadElementFromArray(dynamic args) => Stack.Push(Stack.Pop()[Stack.Pop()]);

    /// <summary>
    /// Clears stack
    /// </summary>
    /// <param name="args"></param>
    public static void Clear(dynamic args) => Stack.Clear();

    /// <summary>
    /// Loads from local heap onto the stack
    /// </summary>
    /// <param name="args">Index of local heap to load</param>
    public static void LoadFromStorage(dynamic args) => Stack.Push(storage[args]);

    /// <summary>
    /// Adds last element with second last and pushes it onto the stack
    /// </summary>
    /// <param name="args"></param>
    public static void Add(dynamic args) { dynamic p = Stack.Pop(); Stack.Push(Stack.Pop() + p); }

    /// <summary>
    /// Subs last element with second last and pushes it onto the stack
    /// </summary>
    /// <param name="args"></param>
    public static void Sub(dynamic args) { dynamic p = Stack.Pop(); Stack.Push(Stack.Pop() - p); }

    /// <summary>
    /// Divides last element with second last and pushes it onto the stack
    /// </summary>
    /// <param name="args"></param>
    public static void Div(dynamic args) { dynamic p = Stack.Pop(); Stack.Push(Stack.Pop() / p); }

    /// <summary>
    /// Multiplies last element with second last and pushes it onto the stack
    /// </summary>
    /// <param name="args"></param>
    public static void Mul(dynamic args) => Stack.Push(Stack.Pop() * Stack.Pop());

    /// <summary>
    /// Does the power between last element and second last and pushes it onto the stack
    /// </summary>
    /// <param name="args"></param>
    public static void Pow(dynamic args) { dynamic p = Stack.Pop(); Stack.Push(Math.Pow(Stack.Pop(), p)); }

    /// <summary>
    /// Prints the last element onto the stack into the console
    /// </summary>
    /// <param name="args"></param>
    public static void RvmOutput(dynamic args) => Console.Write(Stack.Pop());

    /// <summary>
    /// Asks for input and returns onto the stack
    /// </summary>
    /// <param name="args"></param>
    public static void RvmInput(dynamic args) => Stack.Push(Console.ReadLine());

    /// <summary>
    /// Calls shell using the last element onto the stack
    /// </summary>
    /// <param name="args"></param>
    public static void RvmShell(dynamic args) => Process.Start(new ProcessStartInfo() { FileName = "cmd", Arguments = "/C " + Stack.Pop(), UseShellExecute = false });

    /// <summary>
    /// Calls a built in rvm function reference stored onto the stack
    /// </summary>
    /// <param name="args"></param>
    public static void RvmCall(dynamic args) => Stack.Pop()();

    /// <summary>
    /// Pops the last element of the stack
    /// </summary>
    /// <param name="args"></param>
    public static void Pop(dynamic args) => Stack.Pop();

    /// <summary>
    /// Breaks function executing returning to previous
    /// </summary>
    /// <param name="args"></param>
    public static void Return(dynamic args) => InstructionIndex = 10000000;

    /// <summary>
    /// Compares last two elements onto the stack and pushes true if are equals or false
    /// </summary>
    /// <param name="args"></param>
    public static void CompareEQ(dynamic args) => Stack.Push(Stack.Pop() == Stack.Pop());

    /// <summary>
    /// Compares last two elements onto the stack and pushes true if last is greater than second last or false
    /// </summary>
    /// <param name="args"></param>
    public static void CompareGreater(dynamic args) => Stack.Push(Stack.Pop() < Stack.Pop());

    /// <summary>
    /// Compares last two elements onto the stack and pushes true if last is less than second last or false
    /// </summary>
    /// <param name="args"></param>
    public static void CompareLess(dynamic args) => Stack.Push(Stack.Pop() > Stack.Pop());

    /// <summary>
    /// Compares last two elements onto the stack and pushes true if are not equals or false
    /// </summary>
    /// <param name="args"></param>
    public static void CompareNEQ(dynamic args) => Stack.Push(Stack.Pop() != Stack.Pop());

    /// <summary>
    /// Exits to the program
    /// </summary>
    /// <param name="args"></param>
    public static void End(dynamic args) => Environment.Exit(0);

    /// <summary>
    /// Pops last element of the stack and jump to <paramref name="args"/> if true
    /// </summary>
    /// <param name="args">Index of instruction to jump on</param>
    public static void JumpTrue(dynamic args) {
        if (Stack.Pop())
            InstructionIndex = args - 1;
    }

    /// <summary>
    /// Pops last element of the stack and jump to <paramref name="args"/> if false
    /// </summary>
    /// <param name="args">Index of instruction to jump on</param>
    public static void JumpFalse(dynamic args) {
        if (!Stack.Pop())
            InstructionIndex = args - 1;
    }

    /// <summary>
    /// Pops last element of the stack and jump to <paramref name="args"/> if false
    /// </summary>
    /// <param name="args">Index of instruction to jump on</param>
    public static void SkipTrue(dynamic args) {
        if (Stack.Pop())
            InstructionIndex += args;
    }

    /// <summary>
    /// Pops last element of the stack and jump to <paramref name="args"/> if false
    /// </summary>
    /// <param name="args">Index of instruction to jump on</param>
    public static void SkipFalse(dynamic args) {
        if (!Stack.Pop())
            InstructionIndex += args;
    }

    /// <summary>
    /// Pops last element of the stack and jump to <paramref name="args"/> if false
    /// </summary>
    /// <param name="args">Index of instruction to jump on</param>
    public static void BackTrue(dynamic args) {
        if (Stack.Pop())
            InstructionIndex -= args;
    }

    /// <summary>
    /// Pops last element of the stack and jump to <paramref name="args"/> if false
    /// </summary>
    /// <param name="args">Index of instruction to jump on</param>
    public static void BackFalse(dynamic args) {
        if (!Stack.Pop())
            InstructionIndex -= args;
    }

    /// <summary>
    /// Jumps to <paramref name="args"/>
    /// </summary>
    /// <param name="args">Number of instruction to jump on</param>
    public static void Jump(dynamic args) => InstructionIndex = args-1;
}