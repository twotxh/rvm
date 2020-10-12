using System;
using System.Diagnostics;

class Program {
    static void Main(string[] args) {
        var main = new Instruction[] {
            Instruction.New(Runtime.Load, new Action(Console.Clear)),
            Instruction.New(Runtime.RvmCall),
            Instruction.New(Console.Write, "Insert a number: "),
            Instruction.New(Runtime.RvmInput),
            Instruction.New(Runtime.CastToInt),
            Instruction.New(Console.Write, "Insert another number: "),
            Instruction.New(Runtime.RvmInput),
            Instruction.New(Runtime.Cast, typeof(int)),

            Instruction.New(Console.Write, "Choose an operation: [a:add, s:sub, m:mul, d:div, p:pow] > "),
            Instruction.New(Runtime.RvmInput),
            Instruction.New(Runtime.Store, 0),

            Instruction.New(Runtime.LoadFromStorage, 0),
            Instruction.New(Runtime.Load, "a"),
            Instruction.New(Runtime.CompareEQ),
            Instruction.New(Runtime.SkipFalse, 2),
            Instruction.New(Runtime.Call, "add"),
            Instruction.New(Runtime.Jump, 0),

            Instruction.New(Runtime.LoadFromStorage, 0),
            Instruction.New(Runtime.Load, "s"),
            Instruction.New(Runtime.CompareEQ),
            Instruction.New(Runtime.SkipFalse, 2),
            Instruction.New(Runtime.Call, "sub"),
            Instruction.New(Runtime.Jump, 0),

            Instruction.New(Runtime.LoadFromStorage, 0),
            Instruction.New(Runtime.Load, "m"),
            Instruction.New(Runtime.CompareEQ),
            Instruction.New(Runtime.SkipFalse, 2),
            Instruction.New(Runtime.Call, "mul"),
            Instruction.New(Runtime.Jump, 0),

            Instruction.New(Runtime.LoadFromStorage, 0),
            Instruction.New(Runtime.Load, "d"),
            Instruction.New(Runtime.CompareEQ),
            Instruction.New(Runtime.SkipFalse, 2),
            Instruction.New(Runtime.Call, "div"),
            Instruction.New(Runtime.Jump, 0),

            Instruction.New(Runtime.LoadFromStorage, 0),
            Instruction.New(Runtime.Load, "p"),
            Instruction.New(Runtime.CompareEQ),
            Instruction.New(Runtime.SkipFalse, 2),
            Instruction.New(Runtime.Call, "pow"),
            Instruction.New(Runtime.Jump, 0),

            Instruction.New(Console.WriteLine, "Failed to read operation"),
            Instruction.New(Runtime.Load, new Func<ConsoleKeyInfo>(Console.ReadKey)),
            Instruction.New(Runtime.RvmCall),
            Instruction.New(Runtime.Jump, 0),
        };
        var add = new Instruction[] {
            Instruction.New(Runtime.Add),
            Instruction.New(Console.Write, "Result: "),
            Instruction.New(Runtime.RvmOutput),
            Instruction.New(Runtime.Load, new Func<ConsoleKeyInfo>(Console.ReadKey)),
            Instruction.New(Runtime.RvmCall),
            Instruction.New(Runtime.Return),
        };
        var sub = new Instruction[] {
            Instruction.New(Runtime.Sub),
            Instruction.New(Console.Write, "Result: "),
            Instruction.New(Runtime.RvmOutput),
            Instruction.New(Runtime.Load, new Func<ConsoleKeyInfo>(Console.ReadKey)),
            Instruction.New(Runtime.RvmCall),
            Instruction.New(Runtime.Return),
        };
        var mul = new Instruction[] {
            Instruction.New(Runtime.Mul),
            Instruction.New(Console.Write, "Result: "),
            Instruction.New(Runtime.RvmOutput),
            Instruction.New(Runtime.Load, new Func<ConsoleKeyInfo>(Console.ReadKey)),
            Instruction.New(Runtime.RvmCall),
            Instruction.New(Runtime.Return),
        };
        var div = new Instruction[] {
            Instruction.New(Runtime.Div),
            Instruction.New(Console.Write, "Result: "),
            Instruction.New(Runtime.RvmOutput),
            Instruction.New(Runtime.Load, new Func<ConsoleKeyInfo>(Console.ReadKey)),
            Instruction.New(Runtime.RvmCall),
            Instruction.New(Runtime.Return),
        };
        var pow = new Instruction[] {
            Instruction.New(Runtime.Pow),
            Instruction.New(Console.Write, "Result: "),
            Instruction.New(Runtime.RvmOutput),
            Instruction.New(Runtime.Load, new Func<ConsoleKeyInfo>(Console.ReadKey)),
            Instruction.New(Runtime.RvmCall),
            Instruction.New(Runtime.Return),
        };
        var program = new Group[] {
            Group.New(main, "main"),
            Group.New(add, "add"),
            Group.New(sub, "sub"),
            Group.New(mul, "mul"),
            Group.New(div, "div"),
            Group.New(pow, "pow"),
        };
        Console.WriteLine("----{0}----", DateTime.Now);
        Rvm.Execute(program);
        Console.WriteLine("\n----{0}----", DateTime.Now);
    }
}