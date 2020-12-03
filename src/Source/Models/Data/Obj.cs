using System.Collections.Generic;

namespace RobinVM.Models
{
    public struct Obj
    {
        public Dictionary<string, object> CacheTable;
        public Function? Ctor;
    }
}