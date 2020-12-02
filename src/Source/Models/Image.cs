using System;
using System.Collections.Generic;
using System.Text;

namespace RobinVM.Models
{
    public struct Image
    {
        public static Image New(string manifestName, ref Function entryPoint)
        {
            return new Image(manifestName, ref entryPoint);
        }

        readonly public string ManifestName;
        readonly public Function EntryPointPointer;

        readonly Dictionary<string, object> CacheTable;
        public Image(string manifestName, ref Function entryPoint)
        {
            this.ManifestName = manifestName;
            this.EntryPointPointer = entryPoint;
            this.CacheTable = new Dictionary<string, object>();

            AddFunction(manifestName, entryPoint);
        }
        public Function FindFunction(string id)
        {
            return (Function)CacheTable[id];
        }
        public Obj FindObj(string id)
        {
            return (Obj)CacheTable[id];
        }
        public object FindGlobal(string id)
        {
            return CacheTable[id];
        }
        public void AddFunction(string id, Function function)
        {
            this.CacheTable.TryAdd(id, function);
        }
        public void AddObj(string id, Obj @object)
        {
            this.CacheTable.Add(id, @object);
        }
        public void AddGlobal(string id, object global)
        {
            this.CacheTable.Add(id, global);
        }

        public Dictionary<string, object> GetCacheTable()
        {
            return CacheTable;
        }
    }
}