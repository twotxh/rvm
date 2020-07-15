
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
                        Tools.ExecLine(Console.ReadLine(), new Storage());
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
        public static void ExecLine(string _line, Storage _processTable)
        {
            // fix shell
            if (!string.IsNullOrWhiteSpace(_line))
            {
                Source Line = new Source();
                Line.Value = _line;
                Source LineEmptyString = new Source();
                LineEmptyString.Value = _line; LineEmptyString.Value = LineEmptyString.GetEmptyString();
                Storage ProcessTable = Lexer.GetProcessTable(Line, LineEmptyString, _processTable);
                Interpreter Robin = new Interpreter();
                Robin.Run(ProcessTable);
            }
        }
        public static void ExecFile(string Path)
        {
            string[] Code = System.IO.File.ReadAllLines(Path);
            Source Line = new Source();
            Source LineEmptyString = new Source();
            Storage ProcessTable = new Storage();
            for (int i = 0; i < Code.Count(); i++) {
                LineCounter++;
                if (!string.IsNullOrWhiteSpace(Code[i])) {
                    Line.Value = Code[i];
                    LineEmptyString.Value = Line.GetEmptyString();
                    ProcessTable = Lexer.GetProcessTable(Line, LineEmptyString, ProcessTable);
                }
            }
            Interpreter Robin = new Interpreter();
            Robin.Run(ProcessTable);
        }
    }
    class Lexer
    {
        private static bool isIndentArea = false; private static Process.Type _LastProcessType = Process.Type.Null; private static string _LastProcessName = string.Empty; private static string _LastProcessNameArg = string.Empty; private static StringBuilder _LastProcessArg = new StringBuilder();
        public static Storage GetProcessTable(Source Line, Source LineEmptyString, Storage ProcessTable)
        {
            if (isIndentArea) {
                if (Line.Pop(" ") == "}") {
                    isIndentArea = false;
                    ProcessTable.AddToProcessTable(_LastProcessType, _LastProcessName, _LastProcessNameArg, _LastProcessArg);
                }
                else {
                    _LastProcessArg.AppendLine(Line.ToString());
                }
            }


            else if (LineEmptyString.Value.Contains('=') && LineEmptyString.Pop(" ").Replace("=", " = ").Split(' ')[1] == "=")
                ProcessTable.AddToProcessTable(Process.Type.Variable, LineEmptyString.Select(0, LineEmptyString.Value.IndexOf('=')).Replace(" ", ""), "TEST", Line.Value.Substring(Line.Value.IndexOf('=') + 1));
            return ProcessTable;
        }
    }
    class Interpreter
    {
        public void Run(Storage ProcessTable)
        {
            Storage.Program Ram = new Storage.Program();
            BuiltIn.InitializeComponent(Ram);
            for (int i = 0; i <  ProcessTable.ProcessCounter; i++) {
                Process.RunProcess(ProcessTable.GetProcessType()[i], ProcessTable.GetProcessName()[i], ProcessTable.GetProcessNameArg()[i], ProcessTable.GetProcessArg()[i], Ram);
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
            Null,
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
            private Dictionary<string, Source> Functions = new Dictionary<string, Source>();
            private Dictionary<string, Source> Classes = new Dictionary<string, Source>();
            private Dictionary<string, Source> IfStatement = new Dictionary<string, Source>();
            private Dictionary<string, Source> ForStatement = new Dictionary<string, Source>();
            private Dictionary<string, Source> WhileStatement = new Dictionary<string, Source>();
            private Dictionary<string, Source> LoopStatement = new Dictionary<string, Source>();
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
                if (!Variables.ContainsKey(key))
                    Variables.Add(key, value);
                else
                    Variables[key] = value;
            }
            public void InitFunction(string key, Source value)
            {
                if (!Functions.ContainsKey(key))
                    Functions.Add(key, value);
                else
                    throw new Exception("'" + key + "' already exists!");
            }
            public void InitClass(string key, Source value)
            {
                if (!Classes.ContainsKey(key))
                    Classes.Add(key, value);
                else
                    throw new Exception("'" + key + "' already exists!");
            }
            public void InitIfStatement(string key, Source value)
            {
                if (!IfStatement.ContainsKey(key))
                    IfStatement.Add(key, value);
            }
            public void InitForStatement(string key, Source value)
            {
                if (!ForStatement.ContainsKey(key))
                    ForStatement.Add(key, value);
            }
            public void InitWhileStatement(string key, Source value)
            {
                if (!WhileStatement.ContainsKey(key))
                    WhileStatement.Add(key, value);
            }
            public void InitLoopStatement(string key, Source value)
            {
                if (!LoopStatement.ContainsKey(key))
                    LoopStatement.Add(key, value);
            }
        }
        private List<Process.Type> ProcessType = new List<Process.Type>();
        private List<string> ProcessName = new List<string>();
        private List<string> ProcessNameArg = new List<string>();
        private List<object> ProcessArg = new List<object>();
        public int ProcessCounter = 0;
        public void AddToProcessTable(Process.Type _processType, string _processName, string _processNameArg, object _processArg)
        {
            ProcessType.Add(_processType);
            ProcessName.Add(_processName);
            ProcessNameArg.Add(_processNameArg);
            ProcessArg.Add(_processArg);
            ProcessCounter++;
        }
        public List<Process.Type> GetProcessType() { return ProcessType; }
        public List<string> GetProcessName() { return ProcessName; }
        public List<string> GetProcessNameArg() { return ProcessNameArg; }
        public List<object> GetProcessArg() { return ProcessArg; }
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
        public string Pop(string str)
        {
            return Value.Replace(str, string.Empty);
        }
        public bool AsKeyword(string keyword)
        {
            try
            {
                int FirstLetterIndex = 0;
                for (int i = 0; i < Value.Length; i++)
                {
                    if (Value[i] == ' ')
                        FirstLetterIndex++;
                    else
                        break;
                }
                if (FirstLetterIndex >= 1)
                    return (Value.Remove(0, FirstLetterIndex).Substring(0, keyword.Length) == keyword) ? true : false;
                else
                    return (Value.Substring(0, keyword.Length) == keyword) ? true : false;
            }
            catch (ArgumentOutOfRangeException) { return false; }
        }
        public string Select(int startIndex, int finishIndex)
        {
            return Value.Substring(startIndex, finishIndex - startIndex);
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
