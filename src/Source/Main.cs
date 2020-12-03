using RobinVM;
using RobinVM.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

class Test
{
    static void Main()
    {
        var str = new Obj
        {
            Ctor = new Function(null)
            {
                Instructions = new Instruction[]
                {
                    Instruction.New(Runtime.Return)
                }
            },
            CacheTable = new Dictionary<string, object>()
            {
                { "insfun(.)", new Function(null)
                {
                    Instructions = new Instruction[]
                    {
                        Instruction.New(Runtime.Load, "Unerror"),
                        Instruction.New(Runtime.RvmThrow),
                        Instruction.New(Runtime.Return)
                    }
                }
                },
                { "$", "str" }
            }
        };

        var main = new Function(null)
        {
            Instructions = new Instruction[]
            {
                Instruction.New(Runtime.Return)
            }
        };

        var image = Image.New("main", ref main);

        image.Execute();
    }
}