using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RobinVM
{
    public static class Robin
    {
        /// <summary>
        /// Performs main function of <paramref name="program"/>
        /// <code>
        /// <see cref="Instruction"/>[] main = {<br/>
        /// ••• <see cref="Instruction"/>.New(<see cref="Runtime"/>.Load, "Hello World!"),<br/>
        /// ••• <see cref="Instruction"/>.New(<see cref="Runtime"/>.RvmOutput),<br/>
        /// ••• <see cref="Instruction"/>.New(<see cref="Runtime"/>.Return),<br/>
        /// };<br/>
        /// <see cref="Models.Function"/>[] program = {<br/>
        /// ••• <see cref="Models.Function"/>.New(main, "main")<br/>
        /// };<br/>
        /// <see cref="Robin"/>.Execute(program);
        /// </code>
        /// </summary>
        /// <param name="program">Functions</param>
        public static void Execute(Models.Function[] program)
        {
            Runtime.RuntimeFunctions = program;
            ExecuteLabel(program[Runtime.SwitchFunctions.IndexOf("main")]);
        }
        public static void ExecuteLabel(Models.Function label)
        {
            object[] x0 = Runtime.storage;
            int x1 = Runtime.InstructionIndex;
            Runtime.storage = new object[256];
            for (Runtime.InstructionIndex = 0; Runtime.InstructionIndex < label.Instructions.Length; Runtime.InstructionIndex++)
                label.Instructions[Runtime.InstructionIndex].FunctionPointer(label.Instructions[Runtime.InstructionIndex].Argument);
            Runtime.storage = x0;
            Runtime.InstructionIndex = x1;
        }
    }
}