using System;
using System.Collections.Generic;

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
            if (CacheTable.TryGetValue(id, out object value))
                return (Function)value;
            BasePanic.Throw($"Undifined function `{id}`", "Runtime");
            return (Function)new object();
        }
        public Obj FindObj(string id)
        {
            if (CacheTable.TryGetValue(id, out object value))
                return (Obj)value;
            BasePanic.Throw($"Undifined obj `{id}`", "Runtime");
            return (Obj)new object();
        }
        public object FindGlobal(string id)
        {
            if (CacheTable.TryGetValue(id, out object value))
                return (Function)value;
            BasePanic.Throw($"Undifined global variable `{id}`", "Runtime");
            return null;
        }
        public void AddFunction(string id, Function function)
        {
            if (function.UninstantiatedLabels())
                BasePanic.Throw($"Uninstantiated labels table in function `{id}`, instantiate it in this way: `new Function(instructions: null) {{/*body*/}}", "PreRuntime");
            if (!this.CacheTable.TryAdd(id, function))
                BasePanic.Throw($"Already defined function `{id}`", "Runtime");
        }
        public void AddObj(string id, Obj @object)
        {
            if (@object.CacheTable == null)
                BasePanic.Throw($"Obj `{id}` does not contain members", "PreRuntime");
            @object.CacheTable.TryAdd("$", id);
            if (@object.Ctor == null)
                BasePanic.Throw($"Obj `{id}` does not contain a definition for ctor function", "PreRuntime");
            if (!this.CacheTable.TryAdd(id, @object))
                BasePanic.Throw($"Already defined obj `{id}`", "Runtime");
        }
        public void AddGlobal(string id, object global)
        {
            if (!this.CacheTable.TryAdd(id, global))
                BasePanic.Throw($"Already defined global variable `{id}`", "Runtime");
        }

        public Dictionary<string, object> GetCacheTable()
        {
            return CacheTable;
        }
    }
}