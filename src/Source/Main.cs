using RobinVM;
using RobinVM.Models;
using System;
class Test
{
    static void Main()
    {
        var main = new Instruction[]
        {
            Instruction.New(Runtime.Load, "Hello World"),
            Instruction.New(Runtime.RvmOutput),
            Instruction.New(Runtime.Return)
        };
        var program = new Function[]
        {
            Function.New(main, "main")
        };
        var msStart = DateTime.Now.Ticks;
        Robin.Execute(program);
        var msEnd = DateTime.Now.Ticks-42000; // overhead
        Console.WriteLine($"\n\nMs: {(msEnd-msStart)/10000}, Ticks: {msEnd-msStart}");
    }
}