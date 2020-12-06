using RobinVM;
using RobinVM.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

class Test
{
    static void ExtensionMethod()
    {
        Console.WriteLine("Inside ExtensionMethod...");
        Console.WriteLine(@"From here you can access to all runtime vm services
For example i can debug the stack by drawing it:");
        Runtime.Stack.DrawStack();
    }

    static void Main()
    {

        var main = Function.New
        (
            Instruction.New(Runtime.Load, (Runtime.CallPointer)ExtensionMethod),
            Instruction.New(Runtime.RvmCall),
            Instruction.New(Runtime.Return)
        );

        var image = Image.New(manifestName: "main", entryPoint: ref main);

        image.Execute();

    }
}