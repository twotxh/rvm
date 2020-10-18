using System;
using System.Diagnostics;

class Program {
    static void Main(string[] args) {
        RuntimeInt x = 1.1f;
        x.Add(10);
        RuntimeString y = "ciao";
        RuntimeObjectArray arr = new RuntimeObject[] {x, y};
        RuntimeObjectArray ar1 = new RuntimeObject[] {x, y, arr};
        Console.WriteLine("x:\n  Type: {0}\n  Value: {1}", x.Type, x);
        Console.WriteLine("y:\n  Type: {0}\n  Value: {1}", y.Type, y);
        Console.WriteLine("arr:\n  Type: {0}\n  Value: {1}", arr.Type, arr);
        Console.WriteLine("ar1:\n  Type: {0}\n  Value: {1}", ar1.Type, ar1);
        // var main = new Instruction[] {
        // 	Instruction.New(Runtime.Call, "printhello"),
        //     Instruction.New(Runtime.Return),
        // };
        // var printhello = new Instruction[] {
        // 	Instruction.New(Console.Write, "Hello World!"),
        //     Instruction.New(Runtime.Return),
        // };
        // var program = new Group[] {
        //     Group.New(main, "main"),
        //     Group.New(printhello, "printhello"),
        // };
        // Rvm.Execute(program);
    }
}