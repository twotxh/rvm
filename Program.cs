using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program {
    static void Main(string[] args) {
        Instruction[] main = new Instruction[] {
            new Instruction(Runtime.Store, new dynamic[] { 0, 100 }),
            new Instruction(Runtime.Call, new dynamic[] { 1 }),
        };
        Instruction[] store = new Instruction[] {
            new Instruction(Runtime.Store, new dynamic[] { 0, 100 }),
            new Instruction(Runtime.Call, new dynamic[] { 2 }),
        };
        Instruction[] f1 = new Instruction[] {
            new Instruction(Runtime.Store, new dynamic[] { 0, 100 }),
        };
        Rvm.Execute(new Group[] {
            new Group() {Instructions = main},
            new Group() {Instructions = store},
            new Group() {Instructions = f1},
        });
        //Stopwatch s = new Stopwatch();
        //s.Start();
        //s.Stop();
        //Console.WriteLine(s.ElapsedTicks);
    }
}
