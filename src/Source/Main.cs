using RobinVM;
using RobinVM.Models;
using System;

class BenchmarkMethods
{
    public void Test()
    {
    }
    public void Test1()
    {
    }
    public void Test3()
    {
    }
}
class Test
{
    public static Function[] program = null;
    static void Main()
    {
        var main = new Instruction[]
        {
            Instruction.New(Runtime.Load, "Hello World"),
            Instruction.New(Runtime.RvmOutput),
            Instruction.New(Runtime.Return)
        };
        program = new Function[]
        {
            Function.New(main, "main")
        };
        var msStart = DateTime.Now.Ticks;
        Robin.Execute(program);
        var msEnd = DateTime.Now.Ticks-42000; // overhead
        Console.WriteLine($"\n\nMs: {(msEnd-msStart)/10000}, Ticks: {msEnd-msStart}");
    }
}