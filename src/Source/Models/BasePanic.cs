using System;
using System.Collections.Generic;
using System.Text;

namespace RobinVM.Models
{
    public static class BasePanic
    {
        static List<string> Trace = new List<string>();
        public static void Throw(string error, string state)
        {
            Console.WriteLine("BasePanic[{2}]: {0}\nTrace:\n   at: {1}", error, Trace.Count == 0 ? "$(No Trace)" : string.Join("\n   in: ", Trace), state);
            Environment.Exit(-1);
        }
        public static void LoadTrail(string trail)
        {
            BasePanic.Trace.Insert(0, trail);
        }
        public static void UnloadTrail()
        {
            BasePanic.Trace.RemoveAt(0);
        }
    }
}
