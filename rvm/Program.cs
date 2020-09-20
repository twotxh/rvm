using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program {
    static void Main(string[] args) {
        Rvm.Execute(Bytecode.ParseFromString("{\nLOAD 0, \"print(\\\"\\nciao\\\")\"\n}").GenerateExecutions());
        Console.WriteLine(Runtime.storage[0]);
        //Stopwatch s = new Stopwatch();
        //s.Start();
        //s.Stop();
        //Console.WriteLine(s.ElapsedTicks);
    }
}
