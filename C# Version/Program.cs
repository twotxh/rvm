using System;
class Program {
    static void Main(string[] args) {
        var main = new Instruction[] {
            Instruction.New(Runtime.Load, 0),
            Instruction.New(Runtime.Load, new string[1] { "ciao" }),
            Instruction.New(Runtime.LoadElementFromArray),
            Instruction.New(Runtime.RvmOutput)
        };
        var program = new Group[] {
            Group.New(main),
        };
        //Console.WriteLine(DateTime.Now);
        Rvm.Execute(program);
        //Console.WriteLine(DateTime.Now);
    }
}