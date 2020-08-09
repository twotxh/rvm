using System;
using System.Linq;
using System.IO;

namespace RobinScript
{
    class EntryPoint
    {
        static void Main(string[] args)
        {
            switch (args.Count()) {
                case 0:
                    Console.Title = "RobinScript";
                    Welcome();
                    while (true) {
                        Console.Write(":: ");
                        try { Tools.CompileRuntime(Console.ReadLine()); } catch (Error) { }
                    }
                default:
                    for (int i = 0; i < args.Count(); i++) {
                        if (args[i] == "-c") {
                            File.WriteAllText(args[i+1].Remove(args[i + 1].LastIndexOf('.'))+".rc", Tools.Compile(args[i+1]).ToString());
                        } else {
                            Console.Title = args[i];
                            try { Tools.CompileRun(args[i]); } catch (Error) { Console.ReadKey(); continue; }
                        }
                    }
                    break;
            }
        }
        static void Welcome()
        {
            Console.WriteLine("RobinScript 0.5 (32 bit/ 64 bit) - State: OpenSource, Licese: Apache Licese 2.0 \nAuthor: Carpal, Repository: https://github.com/Carpall/RobinScript");
        }
    }
}
