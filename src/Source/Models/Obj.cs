using System;
using System.Collections.Generic;

namespace RobinVM.Models
{
    public struct Obj
    {
        readonly Dictionary<string, object> CacheTable;
        public Function Ctor;
    }
}