
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
            while (true) {
                Console.Write("[> ");
                Tools.ExecLine(Console.ReadLine());
            }
        }
    }
    class Tools
    {
        public static void ExecLine(string Line)
        {
            Console.WriteLine("1.EXECLINE");
            if (!string.IsNullOrWhiteSpace(Line))
            {
                Lexer.GetProcessTable(Line);
                Interpreter Robin = new Interpreter();
                Robin.Run();
            }
        }
    }
    class Lexer
    {
        public static void GetProcessTable(string Line)
        {
            Console.WriteLine("2.LEXER");
        }
    }
    class Interpreter
    {
        public void Run()
        {
            Console.WriteLine("3.INTERPRETER");
            Storage.Program Ram = new Storage.Program();
            for (int i = 0; i <  Storage.ProcessCounter; i++) {
                Process.Runtime(Storage.GetProcessName()[i], Storage.GetProcessNameArg()[i], Storage.GetProcessArg()[i], Ram);
            }
        }
    }
    class Process
    {
        public static void Runtime(Type ProcessType, string ProcessNameArg, object ProcessArg, Storage.Program Ram)
        {
            Console.WriteLine("4.RUNTIME");
            if (ProcessType == Type.Print)
                Console.Write(ProcessArg);
            else if (ProcessType == Type.Variable)
                Ram.SetVariable(ProcessNameArg, ProcessArg);
            else if (ProcessType == Type.Println)
                Console.WriteLine(ProcessArg);
        }
        public enum Type {
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
    class Storage
    {
        public class Program
        {
            // memory
            private Dictionary<string, object> Variables = new Dictionary<string, object>();
            private Dictionary<string, object> Functions = new Dictionary<string, object>();
            private Dictionary<string, object> Classes = new Dictionary<string, object>();
            private Dictionary<string, object> IfStatement = new Dictionary<string, object>();
            private Dictionary<string, object> ForStatement = new Dictionary<string, object>();
            private Dictionary<string, object> WhileStatement = new Dictionary<string, object>();
            private Dictionary<string, object> LoopStatement = new Dictionary<string, object>();
            // get
            public string GetVariable(string name)
            {
                if (Variables.ContainsKey(name))
                    return Variables[name].ToString();
                else
                    return null;
            }
            public string GetFunction(string name)
            {
                if (Functions.ContainsKey(name))
                    return Functions[name].ToString();
                else
                    return null;
            }
            public string GetClass(string name)
            {
                if (Classes.ContainsKey(name))
                    return Classes[name].ToString();
                else
                    return null;
            }
            public string GetIfStatement(string name)
            {
                if (IfStatement.ContainsKey(name))
                    return IfStatement[name].ToString();
                else
                    return null;
            }
            public string GetForStatement(string name)
            {
                if (ForStatement.ContainsKey(name))
                    return ForStatement[name].ToString();
                else
                    return null;
            }
            public string GetWhileStatement(string name)
            {
                if (WhileStatement.ContainsKey(name))
                    return WhileStatement[name].ToString();
                else
                    return null;
            }
            public string GetLoopStatement(string name)
            {
                if (LoopStatement.ContainsKey(name))
                    return LoopStatement[name].ToString();
                else
                    return null;
            }
            // set or init
            public void SetVariable(string key, object value)
            {
                if (!Functions.ContainsKey(key))
                    Functions.Add(key, value);
                else
                    Functions[key] = value;
            }
            public void InitFunction(string key, object value)
            {
                if (!Functions.ContainsKey(key))
                    Functions.Add(key, value);
                else
                    throw new Exception("'" + key + "' already exists!");
            }
            public void InitClass(string key, object value)
            {
                if (!Classes.ContainsKey(key))
                    Classes.Add(key, value);
                else
                    throw new Exception("'" + key + "' already exists!");
            }
            public void InitIfStatement(string key, object value)
            {
                if (!IfStatement.ContainsKey(key))
                    IfStatement.Add(key, value);
                else
                    throw new Exception("'" + key + "' already exists!");
            }
            public void InitForStatement(string key, object value)
            {
                if (!ForStatement.ContainsKey(key))
                    ForStatement.Add(key, value);
                else
                    throw new Exception("'" + key + "' already exists!");
            }
            public void InitWhileStatement(string key, object value)
            {
                if (!WhileStatement.ContainsKey(key))
                    WhileStatement.Add(key, value);
                else
                    throw new Exception("'" + key + "' already exists!");
            }
            public void InitLoopStatement(string key, object value)
            {
                if (!LoopStatement.ContainsKey(key))
                    LoopStatement.Add(key, value);
                else
                    throw new Exception("'" + key + "' already exists!");
            }
        }
        private static List<Process.Type> ProcessName = new List<Process.Type>();
        private static List<string> ProcessNameArg = new List<string>();
        private static List<object> ProcessArg = new List<object>();
        public static int ProcessCounter = 0;
        public static void AddToProcessTable(Process.Type _processName, string _processNameArg, object _processArg)
        {
            ProcessName.Add(_processName);
            ProcessNameArg.Add(_processNameArg);
            ProcessArg.Add(_processArg);
            ProcessCounter++;
        }
        public static List<Process.Type> GetProcessName() { return ProcessName; }
        public static List<string> GetProcessNameArg() { return ProcessNameArg; }
        public static List<object> GetProcessArg() { return ProcessArg; }
    }
    class Source
    {
        public string Value = "";
        public string GetEmptyString()
        {
            string toReturn = "";
            bool isInterpolate = false;
            foreach (char chr in Value) {
                if (chr == '"') {
                    toReturn += '"';
                    isInterpolate = (isInterpolate) ? false:true;
                }
                else if (!isInterpolate)
                    toReturn += chr;
                else
                    toReturn += ' ';

            }
            return toReturn;
        }
        public string ToString()
        {
            return Value;
        }
    }
}
