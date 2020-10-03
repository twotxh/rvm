//using System;
//using System.Diagnostics;

//class Program {
//    static void Main(string[] args) {
//        var main = new Instruction[] {
//            Instruction.New(Runtime.Load, new Runtime.builtinCall(Console.Clear)),
//            Instruction.New(Runtime.RvmCall),
//            Instruction.New(Console.Write, "Insert a number: "),
//            Instruction.New(Runtime.RvmInput),
//            Instruction.New(Runtime.CastToInt),
//            Instruction.New(Console.Write, "Insert another number: "),
//            Instruction.New(Runtime.RvmInput),
//            Instruction.New(Runtime.CastToInt),

//            Instruction.New(Console.Write, "Choose an operation: [a:add, s:sub, m:mul, d:div, p:pow] > "),
//            Instruction.New(Runtime.RvmInput),
//            Instruction.New(Runtime.Store, 0),

//            Instruction.New(Runtime.LoadFromStorage, 0),
//            Instruction.New(Runtime.Load, "a"),
//            Instruction.New(Runtime.CompareEQ),
//            Instruction.New(Runtime.SkipFalse, 2),
//            Instruction.New(Runtime.Call, 1),
//            Instruction.New(Runtime.Jump, 0),

//            Instruction.New(Runtime.LoadFromStorage, 0),
//            Instruction.New(Runtime.Load, "s"),
//            Instruction.New(Runtime.CompareEQ),
//            Instruction.New(Runtime.SkipFalse, 2),
//            Instruction.New(Runtime.Call, 2),
//            Instruction.New(Runtime.Jump, 0),

//            Instruction.New(Runtime.LoadFromStorage, 0),
//            Instruction.New(Runtime.Load, "m"),
//            Instruction.New(Runtime.CompareEQ),
//            Instruction.New(Runtime.SkipFalse, 2),
//            Instruction.New(Runtime.Call, 3),
//            Instruction.New(Runtime.Jump, 0),

//            Instruction.New(Runtime.LoadFromStorage, 0),
//            Instruction.New(Runtime.Load, "d"),
//            Instruction.New(Runtime.CompareEQ),
//            Instruction.New(Runtime.SkipFalse, 2),
//            Instruction.New(Runtime.Call, 4),
//            Instruction.New(Runtime.Jump, 0),

//            Instruction.New(Runtime.LoadFromStorage, 0),
//            Instruction.New(Runtime.Load, "p"),
//            Instruction.New(Runtime.CompareEQ),
//            Instruction.New(Runtime.SkipFalse, 2),
//            Instruction.New(Runtime.Call, 5),
//            Instruction.New(Runtime.Jump, 0),

//            Instruction.New(Console.WriteLine, "Failed to read operation"),
//            Instruction.New(Runtime.RvmInput),
//            Instruction.New(Runtime.Pop),
//            Instruction.New(Runtime.Jump, 0),
//        };
//        var add = new Instruction[] {
//            Instruction.New(Runtime.Add),
//            Instruction.New(Console.Write, "Result: "),
//            Instruction.New(Runtime.RvmOutput),
//            Instruction.New(Runtime.RvmInput),
//            Instruction.New(Runtime.Pop),
//            Instruction.New(Runtime.Return),
//        };
//        var sub = new Instruction[] {
//            Instruction.New(Runtime.Sub),
//            Instruction.New(Console.Write, "Result: "),
//            Instruction.New(Runtime.RvmOutput),
//            Instruction.New(Runtime.RvmInput),
//            Instruction.New(Runtime.Pop),
//            Instruction.New(Runtime.Return),
//        };
//        var mul = new Instruction[] {
//            Instruction.New(Runtime.Mul),
//            Instruction.New(Console.Write, "Result: "),
//            Instruction.New(Runtime.RvmOutput),
//            Instruction.New(Runtime.RvmInput),
//            Instruction.New(Runtime.Pop),
//            Instruction.New(Runtime.Return),
//        };
//        var div = new Instruction[] {
//            Instruction.New(Runtime.Div),
//            Instruction.New(Console.Write, "Result: "),
//            Instruction.New(Runtime.RvmOutput),
//            Instruction.New(Runtime.RvmInput),
//            Instruction.New(Runtime.Pop),
//            Instruction.New(Runtime.Return),
//        };
//        var pow = new Instruction[] {
//            Instruction.New(Runtime.Pow),
//            Instruction.New(Console.Write, "Result: "),
//            Instruction.New(Runtime.RvmOutput),
//            Instruction.New(Runtime.RvmInput),
//            Instruction.New(Runtime.Pop),
//            Instruction.New(Runtime.Return),
//        };
//        var program = new Group[] {
//            Group.New(main), // 0
//            Group.New(add), // 1
//            Group.New(sub), // 2
//            Group.New(mul), // 3
//            Group.New(div), // 4
//            Group.New(pow), // 5
//        };
//        //Console.WriteLine("----{0}----", DateTime.Now);
//        Rvm.Execute(program);
//        //Console.WriteLine("\n----{0}----", DateTime.Now);
//    }
//}