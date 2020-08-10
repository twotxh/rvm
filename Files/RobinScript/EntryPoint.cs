using System;
using System.Linq;
using System.IO;
using System.IO.Compression;

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
                        Console.WriteLine(Bytecode.Parse(Console.ReadLine()));
                        /*try { Tools.CompileRuntime(Console.ReadLine()); } catch (Error) { }*/
                    }
                    break;
                default:
                    for (int i = 0; i < args.Count(); i++) {
                        if (args[i] == "-c") {
                            try { File.WriteAllText(args[i + 1].Remove(args[i + 1].LastIndexOf('.'))+".rc", Tools.Compile(args[i + 1]).ToString()); } catch (Error) { Console.ReadKey(); continue; }
                        } else {
                            if (args[i].Substring(args[i].LastIndexOf('.')) == ".rc") {
                                Tools.Run(args[i]);
                            } else {
                                Console.Title = args[i];
                                try { Tools.CompileRun(args[i]); } catch (Error) { Console.ReadKey(); continue; }
                            }
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