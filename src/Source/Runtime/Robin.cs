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
        public static void Execute(Models.Image programImage)
        {
            Runtime.Stack.Clear();
            Runtime.Storage = new object[byte.MaxValue];
            Runtime.RuntimeImage = programImage;
            ExecuteLabel(programImage.EntryPointPointer);
        }
        public static void ExecuteLabel(Models.Function label)
        {
            var x0 = Runtime.Storage;
            var x1 = Runtime.ProgramCounter;
            Runtime.Storage = new object[byte.MaxValue];
            for (Runtime.ProgramCounter = 0; Runtime.ProgramCounter < label.Instructions.Length; Runtime.ProgramCounter++)
                label.Instructions[Runtime.ProgramCounter].FunctionPointer(label.Instructions[Runtime.ProgramCounter].Argument);
            Runtime.Storage = x0;
            Runtime.ProgramCounter = x1;
        }
    }
}