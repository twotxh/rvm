using RobinVM;
using RobinVM.Models;
using System;
using System.Diagnostics;

class Test
{
    static void CompareLOE_Int(object args)
    {
        int p = (int)Runtime.Stack.Pop();
        Runtime.Stack.Push((int)Runtime.Stack.Pop()<=p);
    }
    static void Sub_Int(object args)
    {
        int p = (int)Runtime.Stack.Pop();
        Runtime.Stack.Push((int)Runtime.Stack.Pop() - p);
    }
    static void Add_Int(object args)
    {
        Runtime.Stack.Push((int)Runtime.Stack.Pop() + (int)Runtime.Stack.Pop());
    }
    static void Main()
    {
        var timer = new Stopwatch();
        var fib = new Instruction[]
        {
            Instruction.New(Runtime.Store, 0),
            Instruction.New(Runtime.LoadFromStorage, 0),
            Instruction.New(Runtime.Load, 1),
            Instruction.New(CompareLOE_Int),
            Instruction.New(Runtime.SkipFalse, 2),
            Instruction.New(Runtime.LoadFromStorage, 0),
            Instruction.New(Runtime.Return),
            Instruction.New(Runtime.LoadFromStorage, 0),
            Instruction.New(Runtime.Load, 1),
            Instruction.New(Sub_Int),
            Instruction.New(Runtime.Call, "fib"),
            Instruction.New(Runtime.LoadFromStorage, 0),
            Instruction.New(Runtime.Load, 2),
            Instruction.New(Sub_Int),
            Instruction.New(Runtime.Call, "fib"),
            Instruction.New(Add_Int),
            Instruction.New(Runtime.Return),
        };
        var main = new Instruction[]
        {
            Instruction.New(Console.WriteLine, "The program calculates the N° num of fibonacci series!"),
            Instruction.New(Console.Write, "N°: "),
            Instruction.New(Runtime.RvmInput),
            Instruction.New(Runtime.CastToInt),
            Instruction.New(Runtime.Call, "fib"),
            Instruction.New(Console.Write, "The N° num: "),
            Instruction.New(Runtime.RvmOutput),
            Instruction.New(Console.Write, '\n'),
            Instruction.New(Runtime.Jump, 1),
            Instruction.New(Runtime.Return)
        };
        var program = new Function[]
        {
            Function.New(main, "main"),
            Function.New(fib, "fib"),
        };
        Robin.Execute(program);
    }
}