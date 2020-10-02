using System;
class Program {
    static void Main(string[] args) {
        var main = new Instruction[] {
            Instruction.New(Runtime.Load, "true"),
            Instruction.New(Runtime.CastToBool),
            Instruction.New(Runtime.RvmOutput)
        };
        var program = new Group[] {
            Group.New(main),
        };
        Console.WriteLine("{0}\n", DateTime.Now);
        Rvm.Execute(program);
        Console.WriteLine("\n{0}",DateTime.Now);
    }
}