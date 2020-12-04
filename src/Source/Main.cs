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
                    Instruction.New(Runtime.LoadFromArgs, 2),
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
                Instruction.New(Runtime.Load, "Carpal"),
                Instruction.New(Runtime.Load, "!"),
                Instruction.New(Runtime.NewObj, "person"),
                Instruction.New(Runtime.CallInstance, "printname(.)"),
                Instruction.New(Runtime.Return)
            }
        };

        var image = Image.New("main", ref main);
        image.AddObj("person", person);

        //var stopwatch = Stopwatch.StartNew();
        image.Execute();
        //stopwatch.Stop();
        //Console.WriteLine("ms: {0}", stopwatch.ElapsedMilliseconds);
    }
}