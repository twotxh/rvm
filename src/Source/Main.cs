using RobinVM;
using RobinVM.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

class Test
{
    static void Main()
    {
        var person = new Obj
        {
            Ctor = new Function(null)
            {
                Instructions = new Instruction[]
                {
                    Instruction.New(Runtime.LoadFromArgs, 0),
                    Instruction.New(Runtime.LoadFromArgs, 1),
                    Instruction.New(Runtime.StoreGlobal, "name"),
                    Instruction.New(Runtime.Return)
                }
            },
            CacheTable = new Dictionary<string, object>()
            {
                { "printname(.)", new Function(null)
                {
                    Instructions = new Instruction[]
                    {
                        Instruction.New(Runtime.LoadFromArgs, 0),
                        Instruction.New(Runtime.LoadGlobal, "name"),
                        Instruction.New(Runtime.RvmOutput),
                        Instruction.New(Runtime.Return)
                    }
                }
                },
                { "name", null }
            }
        };

        var main = new Function(null)
        {
            Instructions = new Instruction[]
            {
                Instruction.New(Console.WriteLine, "Test: 1"),
                Instruction.New(Runtime.Try, 5), // 4 = instruction index of OnPanic statement
                    Instruction.New(Console.WriteLine, "Inside Try..."),
                    Instruction.New(Runtime.Finally), // close try scope
                    Instruction.New(Runtime.Jump, 8), // jump out of onpanic scope
                Instruction.New(Runtime.OnPanic),
                    Instruction.New(Console.WriteLine, "Inside OnPanic..."),
                    Instruction.New(Runtime.Finally), // close try scope
                Instruction.New(Console.WriteLine, "Finally..."),
                Instruction.New(Runtime.Call, "test2()"),
                Instruction.New(Runtime.Return)
            }
        };

        var test2 = new Function(null)
        {
            Instructions = new Instruction[]
            {
                Instruction.New(Console.WriteLine, "Test: 2"),
                Instruction.New(Runtime.Try, 7), // 4 = instruction index of OnPanic statement
                    Instruction.New(Runtime.Load, "Managed error"),
                    Instruction.New(Runtime.RvmThrow),
                    Instruction.New(Console.WriteLine, "Inside Try..."),
                    Instruction.New(Runtime.Finally), // close try scope
                    Instruction.New(Runtime.Jump, 10), // jump out of onpanic scope
                Instruction.New(Runtime.OnPanic),
                    Instruction.New(Console.WriteLine, "Inside OnPanic..."),
                    Instruction.New(Runtime.Finally), // close onpanic scope
                Instruction.New(Console.WriteLine, "Finally..."),
                Instruction.New(Runtime.Call, "test3()"),
                Instruction.New(Runtime.Return)
            }
        };

        var test3 = new Function(null)
        {
            Instructions = new Instruction[]
            {
                Instruction.New(Console.WriteLine, "Test: 3"),
                Instruction.New(Runtime.Load, "Unmanaged error"),
                Instruction.New(Runtime.RvmThrow),
                Instruction.New(Runtime.Return)
            }
        };

        var image = Image.New("main", ref main);
        image.AddFunction("test2()", test2);
        image.AddFunction("test3()", test3);

        image.Execute();
    }
}