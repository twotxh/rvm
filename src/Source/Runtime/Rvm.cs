using System;
using System.Diagnostics;
public static class Rvm {
    /// <summary>
    /// Performs main function of <paramref name="program"/>
    /// <code>
    /// <see cref="Instruction"/>[] main = {<br/>
    /// ••• <see cref="Instruction"/>.New(<see cref="Runtime"/>.Load, "Hello World!"),<br/>
    /// ••• <see cref="Instruction"/>.New(<see cref="Runtime"/>.RvmOutput),<br/>
    /// ••• <see cref="Instruction"/>.New(<see cref="Runtime"/>.Return),<br/>
    /// };<br/>
    /// <see cref="Function"/>[] program = {<br/>
    /// ••• <see cref="Function"/>.New(main, "main")<br/>
    /// };<br/>
    /// <see cref="Rvm"/>.Execute(program);
    /// </code>
    /// </summary>
    /// <param name="program">Functions</param>
    public static void Execute(Function[] program) {
        Runtime.RuntimeFunctions = program;
        ExecuteLabel(program[Runtime.SwitchFunctions.IndexOf("main")]);
    }
    public static void ExecuteLabel(Function label) {
        Storage x0 = Runtime.storage;
        int x1 = Runtime.InstructionIndex;
        Runtime.storage = new Storage();
        for (Runtime.InstructionIndex = 0; Runtime.InstructionIndex < label.Instructions.Length; Runtime.InstructionIndex++)
            label.Instructions[Runtime.InstructionIndex].instruction(label.Instructions[Runtime.InstructionIndex].arguments);
        Runtime.storage = x0;
        Runtime.InstructionIndex = x1;
    }
    /// <summary>
    /// Returns a program performable by Execute(Function[])<br/>
    /// <code>
    /// var program = <see cref="Rvm"/>.CompileJit(<see cref="System.IO.File"/>.ReadAllText("bytecode.ext"));<br/>
    /// <see cref="Rvm"/>.Execute(program);
    /// </code>
    /// </summary>
    /// <param name="code"></param>
    public static void CompileJit(string code)
    {
        for (int i = 0; i < code.Length; i++)
        {
            // tokenizer and loader
        }
    }
}