
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinScript
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    class Interpreter
    {

    }
    class Storage
    {
        public class Program
        {
            // memory
            static private Dictionary<string, Source> Variables = new Dictionary<string, Source>();
            static private Dictionary<string, Source> Functions = new Dictionary<string, Source>();
            static private Dictionary<string, Source> Classes = new Dictionary<string, Source>();
            static private Dictionary<string, Source> IfStatement = new Dictionary<string, Source>();
            static private Dictionary<string, Source> ForStatement = new Dictionary<string, Source>();
            static private Dictionary<string, Source> WhileStatement = new Dictionary<string, Source>();
            static private Dictionary<string, Source> LoopStatement = new Dictionary<string, Source>();
            // get
            public static string GetVariable(string name)
            {
                if (Variables.ContainsKey(name))
                    return Variables[name].ToString();
                else
                    return null;
            }
            public static string GetFunction(string name)
            {
                if (Functions.ContainsKey(name))
                    return Functions[name].ToString();
                else
                    return null;
            }
            public static string GetClass(string name)
            {
                if (Classes.ContainsKey(name))
                    return Classes[name].ToString();
                else
                    return null;
            }
            public static string GetIfStatement(string name)
            {
                if (IfStatement.ContainsKey(name))
                    return IfStatement[name].ToString();
                else
                    return null;
            }
            public static string GetForStatement(string name)
            {
                if (ForStatement.ContainsKey(name))
                    return ForStatement[name].ToString();
                else
                    return null;
            }
            public static string GetWhileStatement(string name)
            {
                if (WhileStatement.ContainsKey(name))
                    return WhileStatement[name].ToString();
                else
                    return null;
            }
            public static string GetLoopStatement(string name)
            {
                if (LoopStatement.ContainsKey(name))
                    return LoopStatement[name].ToString();
                else
                    return null;
            }
            // set or init
            public static void SetVariable(string key, Source value)
            {
                if (!Functions.ContainsKey(key))
                    Functions.Add(key, value);
                else
                    Functions[key] = value;
            }
            public static void InitFunction(string key, Source value)
            {
                if (!Functions.ContainsKey(key))
                    Functions.Add(key, value);
                else
                    throw new Exception("'" + key + "' already exists!");
            }
            public static void InitClass(string name)
            {
                if (Classes.ContainsKey(name))
                    return Classes[name].ToString();
                else
                    return null;
            }
            public static void InitIfStatement(string name)
            {
                if (IfStatement.ContainsKey(name))
                    return IfStatement[name].ToString();
                else
                    return null;
            }
            public static void InitForStatement(string name)
            {
                if (ForStatement.ContainsKey(name))
                    return ForStatement[name].ToString();
                else
                    return null;
            }
            public static void InitWhileStatement(string name)
            {
                if (WhileStatement.ContainsKey(name))
                    return WhileStatement[name].ToString();
                else
                    return null;
            }
            public static void InitLoopStatement(string name)
            {
                if (LoopStatement.ContainsKey(name))
                    return LoopStatement[name].ToString();
                else
                    return null;
            }
        }
        public enum Tokens
        {
            Function,
            Class,
            For,
            While,
            Loop,
            If,
            Elseif,
            Else,
            Variable,
            CallFunction,
            CallVariable,
            CallClass,
            Break,
            Continue,
            Return,
            Load,
            Use,
        }
        public enum StdFunction
        {
            ReadFile,
            WriteFile,
            AppendFile,
            CreateFile,
            MoveFile,
            CopyFile,
            CutFile,
            DeleteFile,
            CompressFile,
            DecompressFile,
            EncryptFile,
            DecryptFile,
            RenameFile,
            Print, // print
            Println, // print + \n
            Prints, // spam string: param -> ("string", 10) where int is the time to spam "string"
            Printf, // print a format string
            Input, // console input: param -> (var_into_store_input_value)
            Init, // initialize a class in a variable
            Cast, // cast a variable: param -> (var_to_cast, int) where int is the type in to cast 'var_to_cast'
        }
    }
    class Source
    {
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
