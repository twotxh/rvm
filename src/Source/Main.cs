using RobinVM;
using RobinVM.Models;
using System;
using System.Diagnostics;

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
         *    load
         *    :img
         *    load:var "myglobal"
         *    rvm:output
         *    ret
         *  .end
         */
        var main = new Function
        {
            Instructions = new Instruction[]
            {
                Instruction.New(Runtime.LoadRuntimeImage),
                Instruction.New(Runtime.LoadGlobal, "myglobal1"),
                Instruction.New(Runtime.LoadRuntimeImage),
                Instruction.New(Runtime.LoadGlobal, "myglobal2"),
                Instruction.New(Runtime.Call, "print(.)"),
                Instruction.New(Runtime.Return)
            }
        };
        var print = new Function
        {
            Instructions = new Instruction[]
            {
                Instruction.New(Runtime.LoadFromArgs, 1),
                Instruction.New(Runtime.RvmOutput),
                Instruction.New(Runtime.Return)
            }
        };
        var image = Image.New("source", ref main);
        image.AddFunction("print(.)", print);
        image.AddGlobal("myglobal1", "global1");
        image.AddGlobal("myglobal2", "global2");
        Robin.Execute(image);
    }
}