using Lang;
using RobinVM;
using RobinVM.Models;
using System;
using System.Diagnostics;

namespace Lang
{
    static class RuntimeExt
    {
        public static void Print()
        {
        }
    }
}

class Program {    
    static void Main() {
        Instruction[] main =
        {
            Instruction.New(Runtime.Load, (Runtime.CallPointer)RuntimeExt.Print),
            Instruction.New(Runtime.RvmCall),
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
        Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}ms");
    }
}