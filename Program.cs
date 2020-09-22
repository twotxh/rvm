using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program {
    static void Main(string[] args) {
        Instruction[] main = new Instruction[] {
            new Instruction(Runtime.Pass, new dynamic[] { 10, 2 }),
            new Instruction(Runtime.Call, new dynamic[] { 1 }),
            new Instruction(Runtime.RvmOutput, null),
        };
        Instruction[] add = new Instruction[] {
            new Instruction(Runtime.LoadFP, new dynamic[] { 1 }),
            new Instruction(Runtime.StoreFP, new dynamic[] { 0, 0 }),
            new Instruction(Runtime.AddF, new dynamic[] { 0 }),
            new Instruction(Runtime.ReturnF, new dynamic[] { 0 }),
        };
        Rvm.Execute(new Group[] {
            new Group() {Instructions = main},
            new Group() {Instructions = add},
        });
        //Stopwatch s = new Stopwatch();
        //s.Start();
        //s.Stop();
        //Console.WriteLine(s.ElapsedTicks);
    }
}
