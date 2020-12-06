using RobinVM;
using RobinVM.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

class Test
{
    static void Main()
    {
        var main = Function.New
        (
            Instruction.New(Runtime.RvmInput),  // ask for input
            Instruction.New(Runtime.CastToInt), // cast input into int
            Instruction.New(Runtime.RvmInput),
            Instruction.New(Runtime.CastToInt),

            Instruction.New(Runtime.CompareEQ), // pop two element from stack and push bool: true if eq, else false

            Instruction.New(Runtime.SkipFalse, 3),  // skip the execution of 2 instructions if stack pop is false
                Instruction.New(Runtime.Load, "=="), // print == if eq
                Instruction.New(Runtime.RvmOutput),
                Instruction.New(Runtime.Skip, 2),    // go out of else scope
            Instruction.New(Runtime.Load, "!="),
            Instruction.New(Runtime.RvmOutput),

            Instruction.New(Runtime.Return)
        );

        var image = Image.New(manifestName: "main", entryPoint: ref main);

        image.Execute();

    }
}