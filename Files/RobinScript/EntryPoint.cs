using System;
using System.Collections.Generic;
using System.Linq;

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
                        try { Tools.Compile(Console.ReadLine()); } catch (Error) { }
                    }
                default:
                    for (int i = 0; i < args.Count(); i++) {
                        Console.Title = args[i];
                        try { Tools.Compile(args[i]); } catch (Error) { Console.ReadKey(); continue; }
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
