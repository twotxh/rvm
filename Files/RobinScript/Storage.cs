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
        BinaryAdd,
        BinarySub,
        BinaryMul,
        BinaryDiv,
    }
    public class Storage
    {
        public FastMemory[] Heap { get; set; }
        public GlobalMemory[] Stack { get; set; }
        public Dictionary<Variable, AdressMode> Adress = new Dictionary<Variable, AdressMode>();
        public void Store()
        {
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
        public AdressMode Adress { get; set; }
        public object Value { get; set; } // make a setter for fix values (take the fix values from oldscript.cs)
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
    public struct FastMemory
    {
        public string NameArea { get; set; }
        public object ValueArea { get; set; }
        public object[] ArgumentArea { get; set; }
    }
    public struct GlobalMemory
    {
        public string NameArea { get; set; }
        public object ValueArea { get; set; }
        public object[] ArgumentArea { get; set; }
    }
}
