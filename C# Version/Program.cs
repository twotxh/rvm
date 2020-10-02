using System;
class Program {
    static void makewhatiwant() {
        Console.WriteLine("");
    }
    static void Main(string[] args) {
        var main = new Instruction[] {
            Instruction.New(Runtime.Load, new Runtime.builtinCall(makewhatiwant)),
            Instruction.New(Runtime.RvmCall)
        };
        var program = new Group[] {
            Group.New(main),
        };
        Console.WriteLine("{0}\n", DateTime.Now);
        Rvm.Execute(program);
        Console.WriteLine("\n{0}",DateTime.Now);
    }
}