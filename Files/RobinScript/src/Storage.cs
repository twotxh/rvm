using System;
using System.Collections.Generic;

namespace RobinScript
{
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
        public LocalMemory Memory = new LocalMemory();
        public Registers Registers = new Registers(); 
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
    public class Registers
    {
        public object ReturnValues { get; set; }
        public object GeneralReg { get; set; }
    }
}