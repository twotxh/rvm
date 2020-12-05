using RobinVM;
using RobinVM.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

class Test
{
    static void Main()
    {
        #region ERROR HANDLING TEST
        /*
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
                Instruction.New(Runtime.Try, 5),
                    Instruction.New(Console.WriteLine, "Inside Try..."),
                    Instruction.New(Runtime.Finally),
                    Instruction.New(Runtime.Jump, 8),
                Instruction.New(Runtime.OnPanic),
                    Instruction.New(Console.WriteLine, "Inside OnPanic..."),
                    Instruction.New(Runtime.Finally),
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
                Instruction.New(Runtime.Try, 10),
                    Instruction.New(Runtime.Load, "This is a managed error"),
                    Instruction.New(Runtime.Load, 5),
                    Instruction.New(Runtime.Load, "ManagedError"),
                    Instruction.New(Runtime.NewObj, "basepanic"),
                    Instruction.New(Runtime.RvmThrow),
                    Instruction.New(Console.WriteLine, "Inside Try..."),
                    Instruction.New(Runtime.Finally),
                    Instruction.New(Runtime.Jump, 10),
                Instruction.New(Runtime.OnPanic),
                    Instruction.New(Console.Write, "Inside OnPanic, managed error: "),
                    Instruction.New(Runtime.LoadGlobal, "msg"),
                    Instruction.New(Runtime.RvmOutput),
                    Instruction.New(Console.Write, "\n"),
                    Instruction.New(Runtime.Finally),
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
                Instruction.New(Runtime.Load, "This is an unmanaged error"),
                Instruction.New(Runtime.Load, 2),
                Instruction.New(Runtime.Load, "UnmanagedPanic"),
                Instruction.New(Runtime.NewObj, "basepanic"),
                Instruction.New(Runtime.RvmThrow),
                Instruction.New(Runtime.Return)
            }
        };
        */
        #endregion
        var main = Function.New(
            new Instruction[]
            {
                Instruction.New(Runtime.Load, 23),
                Instruction.New(Runtime.Load, 2),
                Instruction.New(Runtime.Load, "fdf"),
                Instruction.New(Runtime.Load, 1),
                Instruction.New(Runtime.NewObj, "list"),
                Instruction.New(Runtime.Load, 0),
                Instruction.New(Runtime.CallInstance, "find(..)"),
                Instruction.New(Runtime.RvmOutput),
                Instruction.New(Runtime.Return)
            });
        var image = Image.New("main", ref main);

        image.Execute();
    }
}