using System;
using System.Collections.Generic;

namespace RobinScript
{
    public enum AdressMode {
        Heap,
        Stack
    }
    public enum DataType {
        Variable,
        Function,
        Class,
        String,
        Integear,
        Char,
        List,
        Decimal,
        Dictionary,
    }
    public class Storage
    {
        public LocalMemory Heap { get; set; } = new LocalMemory();
        public GlobalMemory Stack { get; set; } = new GlobalMemory();
        public object RegRetV { get; set; }
        public Dictionary<Variable, AdressMode> Adress = new Dictionary<Variable, AdressMode>();
        public void StoreVar(AdressMode adress, Variable varToStore)
        {
            try {
                if (adress == AdressMode.Heap) Heap.Variables.Add(varToStore.Name, varToStore);
                else Stack.Variables.Add(varToStore.Name, varToStore);
            } catch (Exception) { throw new Error("Function described jet!", "Rename or remove the function"); }
        }
        public void StoreFunc(AdressMode adress, Function funcToStore)
        {
            try {
                if (adress == AdressMode.Heap) Heap.Functions.Add(funcToStore.Name, funcToStore);
                else Stack.Functions.Add(funcToStore.Name, funcToStore);
            } catch (Exception) { throw new Error("Function described jet!", "Rename or remove the function"); }
        }
        public void StoreClass(AdressMode adress, Class classToStore)
        {
            try {
                if (adress == AdressMode.Heap) Heap.Classes.Add(classToStore.Name, classToStore);
                else Stack.Classes.Add(classToStore.Name, classToStore);
            } catch (Exception) { throw new Error("Function described jet!", "Rename or remove the function"); }
        }
    }
    public struct Class
    {
        public string Name { get; set; }
        public AdressMode Adress { get; set; }
        public Parameter[] Parameters { get; set; }
        public Bytecode Code { get; set; }
    }
    public struct Function
    {
        public string Name { get; set; }
        public AdressMode Adress { get; set; }
        public Parameter[] Parameters { get; set; }
        public Bytecode Code { get; set; }
        public DataType Type { get; set; }
    }
    public struct Variable
    {
        public string Name { get; set; }
        public object Value { get; set; } // make a setter for fix values (take the fix values from oldscript.cs)
        public AdressMode Adress { get; set; }
        public Bytecode Code { get; set; }
        public DataType Type { get; set; }
    }
    public struct Parameter
    {
        public string ID { get; set; }
        public object Value { get; set; }
        public AdressMode Adress { get; set; }
        public DataType Type { get; set; }
    }
    public class LocalMemory
    {
        public Dictionary<string, Function> Functions = new Dictionary<string, Function>();
        public Dictionary<string, Variable> Variables = new Dictionary<string, Variable>();
        public Dictionary<string, Class> Classes = new Dictionary<string, Class>();
    }
    public class GlobalMemory
    {
        public Dictionary<string, Function> Functions = new Dictionary<string, Function>();
        public Dictionary<string, Variable> Variables = new Dictionary<string, Variable>();
        public Dictionary<string, Class> Classes = new Dictionary<string, Class>();
    }
}