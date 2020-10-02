using System;
class Program {
    static void Main(string[] args) {
        var main = new Instruction[] {
            Instruction.New(Runtime.Load, 12),
            Instruction.New(Runtime.Load, 11),
            Instruction.New(Runtime.CompareLess),
            Instruction.New(Runtime.RvmOutput)
        };
        var program = new Group[] {
            Group.New(main),
        };
        Rvm.Execute(program);
    }
}