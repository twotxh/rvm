﻿using RobinVM;
using RobinVM.Models;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;
class Test
{
    static void Main()
    {
        /*
         * TEXT:
         *  .img "source" .end
         *  
         *  .var "myglobal"
         *      10
         *  .end
         *  
         *  .ctor
         *    load:img
         *    load:var "myglobal"
         *    rvm:output
         *    ret
         *  .end
         */
        var person = new Obj
        {
            Ctor = new Function
            {
                Instructions = new Instruction[]
                {
                    Instruction.New(Runtime.LoadFromArgs, 0),
                    Instruction.New(Runtime.LoadFromArgs, 1),
                    Instruction.New(Runtime.StoreGlobal, "name"),
                    Instruction.New(Runtime.Return)
                }
            },
            CacheTable = new Dictionary<string, object>
            {
                { "about(.)", new Function
                {
                    Instructions = new Instruction[]
                    {
                        Instruction.New(Runtime.Load, "My name is: "),
                        Instruction.New(Runtime.LoadFromArgs, 0),
                        Instruction.New(Runtime.LoadGlobal, "name"),
                        Instruction.New(Runtime.Add),
                        Instruction.New(Runtime.RvmOutput),
                        Instruction.New(Runtime.Return)
                    }
                }
                },
                { "name", null }
            }

        };
        var main = new Function
        {
            Instructions = new Instruction[]
            {
                Instruction.New(Runtime.Load, "Carpal\n"),
                Instruction.New(Runtime.NewObj, "person"),
                Instruction.New(Runtime.CallInstance, "about(.)"),
                Instruction.New(Runtime.Return)
            }
        };
        var image = Image.New("source", ref main);

        image.AddObj("person", person);
        Robin.Execute(image);
    }
}