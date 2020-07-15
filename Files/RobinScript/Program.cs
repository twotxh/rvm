
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
            switch (args.Count())
            {
                case 0:
                    Welcome();
                    while (true) {
                        Console.Write("[> ");
                        Tools.ExecLine(Console.ReadLine());
                    }
                default:
                    for (int i = 0; i < args.Count(); i++) {
                        Console.WriteLine(i);
                        Tools.ExecFile(args[i]);
                    }break;
            }
        }

        private static void Welcome()
        {
            Console.WriteLine("RobinScript 0.5 (32 bit/ 64 bit) - State: OpenSource, Licese: Apache Licese 2.0 \nAuthor: Carpal, Repository: https://github.com/Carpall/RobinScript");
        }
    }
    class Tools
    {
        public static int LineCounter = 0;
        public static void ExecLine(string Line)
        {
            // fix shell
            if (!string.IsNullOrWhiteSpace(Line))
            {
                Lexer.GetProcessTable(Line);
                Interpreter Robin = new Interpreter();
                Robin.Run();
            }
        }
        public static void ExecFile(string Path)
        {
            string[] Code = System.IO.File.ReadAllLines(Path);
            for (int i = 0; i < Code.Count(); i++) {
                LineCounter++;
                if (!string.IsNullOrWhiteSpace(Code[i]))
                    Lexer.GetProcessTable(Code[i]);
            }
            Interpreter Robin = new Interpreter();
            Robin.Run();
        }
    }
    class Lexer
    {
        public static void GetProcessTable(string Line)
        {
        }
    }
    class Interpreter
    {
        public void Run()
        {
            Storage.Program Ram = new Storage.Program();
            BuiltIn.InitializeComponent(Ram);
            for (int i = 0; i <  Storage.ProcessCounter; i++) {
                Process.RunProcess(Storage.GetProcessType()[i], Storage.GetProcessName()[i], Storage.GetProcessNameArg()[i], Storage.GetProcessArg()[i], Ram);
            }
        }
    }
    class Process
    {
        class Runtime
        {
            public static void Print(string arg) { Console.Write(arg); }
            public static void Println(string arg) { Console.WriteLine(arg); }
            public static void Printf(string arg) { Console.Write(arg); }
            public static void Prints(int spam, string arg) {
                for (int i = 0; i < spam; i++) { Console.Write(arg); }
            }
            public static void Printlns(int spam, string arg)
            {
                for (int i = 0; i < spam; i++) { Console.WriteLine(arg); }
            }
        }
        public static void RunProcess(Type ProcessType, string ProcessName, string ProcessNameArg, object ProcessArg, Storage.Program Ram)
        {
            switch (ProcessType) {
                case Type.Variable: Ram.SetVariable(ProcessName, ProcessArg); break;
                case Type.Print: Runtime.Print(ProcessArg.ToString()); break;
            }
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
            Printlns, // spam string print +\n 
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
        private static List<Process.Type> ProcessType = new List<Process.Type>();
        private static List<string> ProcessName = new List<string>();
        private static List<string> ProcessNameArg = new List<string>();
        private static List<object> ProcessArg = new List<object>();
        public static int ProcessCounter = 0;
        public static void AddToProcessTable(Process.Type _processType, string _processName, string _processNameArg, object _processArg)
        {
            ProcessType.Add(_processType);
            ProcessName.Add(_processName);
            ProcessNameArg.Add(_processNameArg);
            ProcessArg.Add(_processArg);
            ProcessCounter++;
        }
        public static List<Process.Type> GetProcessType() { return ProcessType; }
        public static List<string> GetProcessName() { return ProcessName; }
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
    class Debuger
    {
        public static void print(string toPrint, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null) { Console.WriteLine(toPrint + ' ' + arg0 + ' ' + arg1 + ' ' + arg2 + ' ' + arg3); }
        public void Debug(string line)
        {
        }
        public static void Except(int line, string istruction, string error, string tip)
        {
            istruction += '\n';
            for (int i=0;i<istruction.Length;i++) {
                istruction += '^';
            }
            istruction+= " -> ";
            throw new Exception(line+" | "+istruction+error+"\nTip: "+tip);
        }
    }
    class BuiltIn
    {
        public static void InitializeComponent(Storage.Program Ram)
        {
            // install stdfunction // istall stdclass // install std var
        }
    }
}
