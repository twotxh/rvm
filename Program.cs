using System;
using System.Diagnostics;

class Program {
    static void Main(string[] args) {
        BytecodeBuilder bytecode = new BytecodeBuilder();
        bytecode.AppendInstruction("store", new dynamic[] { 0, "1\n" });
        bytecode.AppendInstruction("loadf", new dynamic[] { 0 });
        bytecode.AppendInstruction("rvm_out", null);
        bytecode.AppendInstruction("call", new dynamic[] { 1 });
        bytecode.AppendInstruction("loadf", new dynamic[] { 0 });
        bytecode.AppendInstruction("rvm_out", null);
        bytecode.AppendLabel();
        bytecode.AppendInstruction("push", null);
        bytecode.AppendInstruction("store", new dynamic[] { 0, "2\n" });
        bytecode.AppendInstruction("loadf", new dynamic[] { 0 });
        bytecode.AppendInstruction("rvm_out", null);
        bytecode.AppendInstruction("pop", null);
        bytecode.AppendLabel();
        Rvm.Execute(bytecode.Build().GenerateExecutions());
        //Stopwatch s = new Stopwatch();
        //s.Start();
        //s.Stop();
        //Console.WriteLine(s.ElapsedTicks);
    }
}
