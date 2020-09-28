using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program {
   static void Main(string[] args) {
      var main = new Instruction[] {
         Instruction.New(Runtime.Pass, new dynamic[] { new Runtime.builtinCall(BuiltInRuntime.WriteInFile), "C:/Users/Mondelli/Desktop/t.exe", "we" }),
         Instruction.New(Runtime.RvmCall),
      };
      var program = new Group[] {
         Group.New(main),
      };
      Console.WriteLine(DateTime.Now);
      Rvm.Execute(program);
      Console.WriteLine(DateTime.Now);
   }
}