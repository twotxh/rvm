using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program {
    static void Main(string[] args) {
        Instruction[] main = new Instruction[] {
            // x = 90
            // y = 90
            // if x == y:
            //   x+=10
            //   print(x)
            // else:
            //   y-=10
            //   print(x)
            new Instruction(Runtime.RvmShell, new dynamic[] { "echo", "%time%"} ),
            new Instruction(Runtime.Store, new dynamic[] {0, 90} ),
            new Instruction(Runtime.Store, new dynamic[] {1, 90} ),
            new Instruction(Runtime.LoadFromStorage, new dynamic[] {0, 1} ),
            new Instruction(Runtime.CompareJNE, new dynamic[] {9} ),
            new Instruction(Runtime.Add, new dynamic[] {0, 10} ),
            new Instruction(Runtime.PassFromStorage, new dynamic[] {0} ),
            new Instruction(Runtime.RvmOutput, null),
            new Instruction(Runtime.Jump, new dynamic[] {12} ),
            new Instruction(Runtime.Sub, new dynamic[] {1, 10} ),
            new Instruction(Runtime.PassFromStorage, new dynamic[] {1} ),
            new Instruction(Runtime.RvmOutput, null),
            new Instruction(Runtime.RvmShell, new dynamic[] { "echo", "%time%"} ),
        };
        var program = new Group[] {
            new Group() {Instructions = main},
        };
        //Console.WriteLine(DateTime.Now);
        Rvm.Execute(program);
        //Console.WriteLine(DateTime.Now);
    }
}
