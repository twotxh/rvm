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
                        Instruction.New(Runtime.Return)
                    }
                }
                },
            }
        };

        var main = new Function(null)
        {
            Instructions = new Instruction[]
            {
                Instruction.New(Runtime.Call, "f1()"),
                Instruction.New(Runtime.Return)
            }
        };

        var f1 = new Function(null)
        {
            Instructions = new Instruction[]
            {
                Instruction.New(Runtime.Return)
            }
        };

        var image = Image.New("main", ref main);
        image.AddFunction("f1()", f1);
        //image.AddObj("str", str);

        //var stopwatch = Stopwatch.StartNew();
        image.Execute();
        //stopwatch.Stop();
        //Console.WriteLine("ms: {0}", stopwatch.ElapsedMilliseconds);
    }
}