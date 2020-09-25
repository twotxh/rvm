using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program {
    static void Main(string[] args) {
        var main = new Instruction[] {
            // for i in range(10):
            //    print(i)
            Instruction.New(Runtime.Store, new dynamic[] { 0, 10 }), // const 0
            Instruction.New(Runtime.Store, new dynamic[] { 1, 0 }), //1
            Instruction.New(Runtime.LoadFromStorage, new dynamic[] { 0, 1 }), //3
            Instruction.New(Runtime.Add, new dynamic[] { 1, 1 }), //2
            Instruction.New(Runtime.CompareJG, 6), //4
            Instruction.New(Runtime.Jump, 9), //5
            Instruction.New(Runtime.PassFromStorage, 1), //6
            Instruction.New(Runtime.RvmOutput), //7
            Instruction.New(Runtime.Jump, 2), //8
            Instruction.New(Runtime.Pass, "\nFinish!\n"), //9
            Instruction.New(Runtime.RvmOutput), //10
        };
        var program = new Group[] {
            new Group() {Instructions = main},
        };
        Console.WriteLine(DateTime.Now);
        Rvm.Execute(program);
        Console.WriteLine(DateTime.Now);
    }
}
