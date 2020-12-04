using System.Collections.Generic;

namespace RobinVM.Models
{
    public struct Obj
    {
        public Obj Copy()
        {
            return new Obj { CacheTable = new Dictionary<string, object>(CacheTable), Ctor = new Function(Ctor.Value.Instructions) };
        }
        public Dictionary<string, object> CacheTable;
        public Function? Ctor;
    }
}