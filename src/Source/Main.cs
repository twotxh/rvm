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
         *    load:img
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
                Instruction.New(Runtime.LoadGlobal, "myglobal"),
                Instruction.New(Runtime.RvmOutput),
                Instruction.New(Runtime.Return)
            }
        };
        var image = Image.New("source", ref main);
        image.AddGlobal("myglobal", 10);
        Robin.Execute(image);
    }
}