using RobinVM;
using RobinVM.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;


class FunctionBuilder
{
    
}

class Test
{
    static void Main()
    {
        var main = Function.New
        (
            Instruction.New(Runtime.Return)
        );

        var image = Image.New(manifestName: "main", entryPoint: ref main);

        image.Execute();

    }
}