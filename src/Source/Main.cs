using RobinVM;
using RobinVM.Models;
using System;
using System.Diagnostics;

namespace Lang
{
    class Runtime
    {
        public static void AddString(object nill)
        {
            RobinVM.Runtime.Stack.Push((string)RobinVM.Runtime.Stack.Pop()+RobinVM.Runtime.Stack.Pop());
        }
    }
}

class Program {
    static void Main() {
        Instruction[] main =
        {
            Instruction.New(Runtime.Load, "ciao"),
            Instruction.New(Runtime.Load, " mondo\n"),
            Instruction.New(Lang.Runtime.AddString),
            Instruction.New(Runtime.Return)
        };
        Function[] program =
        {
            Function.New(main, "main")
        };
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Robin.Execute(program);
        sw.Stop();
        Console.WriteLine("Elapsed: {0}ms", sw.ElapsedMilliseconds);
    }
}