using RobinVM;
using RobinVM.Models;
using System;
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
    static int Fib(int n)
    {
        if (n <= 1)
            return n;
        return Fib(n-1)+Fib(n-2);
    }
    static void Main()
    {
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
            Instruction.New(Runtime.Load, 28),
            Instruction.New(Runtime.Call, "fib"),
            Instruction.New(Runtime.RvmOutput),
            Instruction.New(Runtime.Return)
        };
        var program = new Function[]
        {
            Function.New(main, "main"),
            Function.New(fib, "fib"),
        };
        var msStart = DateTime.Now.Ticks;
        //Console.WriteLine(Fib(20));
        Robin.Execute(program);
        var msEnd = DateTime.Now.Ticks-90000; // datetime, stloc, ld overhead
        Console.WriteLine($"\nS: {(msEnd - msStart) / 10000000}, Ms: {(msEnd-msStart)/10000}, Ticks: {msEnd-msStart}");
    }
}