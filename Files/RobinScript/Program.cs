using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobinScript
{
    // RobinScript 
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "RobinScript";
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
                        Tools.ExecFile(args[i]);
                        Tools.GetExecuteTime();
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
        public static Storage ProcessTable = new Storage();
        public static Interpreter Robin = new Interpreter();
        public static void ExecLine(string _line, Storage _processTable)
        {
            // fix shell
            if (!string.IsNullOrWhiteSpace(_line))
            {
                Source Line = new Source();
                Line.Value = _line;
                Line.Value = Line.RemoveComments("//");
                Source LineEmptyString = new Source();
                LineEmptyString.Value = _line; LineEmptyString.Value = LineEmptyString.GetEmptyString();
                ProcessTable = Lexer.GetProcessTable(Line, LineEmptyString, _processTable);
                Robin.Run(ProcessTable);
            }
        }
        private static System.Diagnostics.Stopwatch ExecuteTimer = new System.Diagnostics.Stopwatch();
        public static void ExecFile(string Path)
        {
            string[] Code = System.IO.File.ReadAllLines(Path);
            Source Line = new Source();
            Source LineEmptyString = new Source();
            Storage ProcessTable = new Storage();
            Interpreter Robin = new Interpreter();
            ExecuteTimer.Start();
            for (int i = 0; i < Code.Count(); i++) {
                LineCounter++;
                if (!string.IsNullOrWhiteSpace(Code[i])) {
                    Line.Value = Code[i];
                    Line.Value = Line.RemoveComments("//");
                    LineEmptyString.Value = Line.GetEmptyString();
                    Robin.Run(Lexer.GetProcessTable(Line, LineEmptyString, ProcessTable));
                }
            }
            ExecuteTimer.Stop();
        }
        public static void GetExecuteTime()
        {
            Console.WriteLine("Execute time: {0}ms", ExecuteTimer.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
    class Lexer
    {

        private static bool isIndentArea = false; private static Process.Type _LastProcessType = Process.Type.Null; private static string _LastProcessName = string.Empty; private static string _LastProcessNameArg = string.Empty; private static StringBuilder _LastProcessArg = new StringBuilder();
        public static Storage GetProcessTable(Source Line, Source LineEmptyString, Storage ProcessTable)
        {
            //Tokenizer TokenTable = Tokenizer.GetTokenTable(Line, Line.GetWordWrapList(", "));
            Tokenizer TokenTable = Tokenizer.GetTokenTable(Line);
            for (int i = 0; i < TokenTable.TokenName.Count; i++) {
                Console.WriteLine("Type: {0} Name: {1} Value: {2}", TokenTable.TokenType[i], TokenTable.TokenName[i], TokenTable.TokenValue[i]);


                if (isIndentArea) {
                    if (TokenTable.TokenType[i] == Tokenizer.Types.Dedent) {
                        isIndentArea = false;
                        ProcessTable.AddToProcessTable(_LastProcessType, _LastProcessName, _LastProcessNameArg, _LastProcessArg);
                    } else {
                        _LastProcessArg.AppendLine(Line.Value);
                    }
                }
                if (TokenTable.TokenType[i] == Tokenizer.Types.Indent) {
                    isIndentArea = true;
                }
            }




            return ProcessTable;
        }
    }
    class Tokenizer
    {
        public List<Types> TokenType = new List<Types>();
        public List<string> TokenName = new List<string>();
        public List<object> TokenValue = new List<object>();
        // old tokenizer
        /*public static Tokenizer GetTokenTable(Source Line, List<string> Tokens)
        {
            Tokenizer TokenTable = new Tokenizer();
            for (int i = 0; i < Tokens.Count; i++) {
                if (Tokens[0] == "fn") {
                    TokenTable.Add(Types.FunctionDescribement, (Tokens[1].Contains('(')) ? Tokens[1].Substring(0, Tokens[1].IndexOf('(')) : Tokens[1], (Tokens[1].Contains('(')) ? true : false); // dove false se non contiene paramentri, al contrario true se li contiene
                    Tokens = Line.GetWordWrapList("(,");
                    for (int j = 1; j < Tokens.Count(); j++) {
                        if (Tokens[j].Contains(')')) {
                            TokenTable.Add(Types.FunctionParameter, (Tokens[j].Contains('=')) ? Tokens[j].Split('=')[0].Replace(" ", "") : Tokens[j].Substring(0, Tokens[j].LastIndexOf(')')).Replace(" ", ""), (Tokens[j].Contains('=')) ? Tokens[j].Split('=')[1].Substring(0, Tokens[j].Split('=')[1].LastIndexOf(")")) : "");
                            break;
                        }
                        TokenTable.Add(Types.FunctionParameter, (Tokens[j].Contains('=')) ? Tokens[j].Split('=')[0].Replace(" ", "") : Tokens[j].Replace(" ", ""), (Tokens[j].Contains('=')) ? Tokens[j].Split('=')[1] : "");
                    }
                    break;
                } if (Tokens[0] == "class") {
                    TokenTable.Add(Types.ClassDescribement, Tokens[1]);
                    break;
                } if (Tokens[i][0] == '$') {
                    TokenTable.Add(Types.CallingFunction, (Tokens[i].Contains('(')) ? Tokens[i].Substring(1, Tokens[i].IndexOf('(') - 1) : Tokens[i].Substring(1), (Line.GetWordWrapList("()").Count() < 3) ? false : true);
                    Tokens = Line.GetWordWrapList("(,");
                    for (int j = 1; j < Tokens.Count(); j++) {
                        if (Tokens[j].Contains(')')) {
                            TokenTable.Add(Types.FunctionParameter, (Tokens[j].Contains('=')) ? Tokens[j].Split('=')[0].Replace(" ", "") : Tokens[j].Substring(0, Tokens[j].LastIndexOf(')')).Replace(" ", ""), (Tokens[j].Contains('=')) ? Tokens[j].Split('=')[1].Substring(0, Tokens[j].Split('=')[1].LastIndexOf(")")) : "");
                            break;
                        }
                        TokenTable.Add(Types.FunctionParameter, (Tokens[j].Contains('=')) ? Tokens[j].Split('=')[0].Replace(" ", "") : Tokens[j].Replace(" ", ""), (Tokens[j].Contains('=')) ? Tokens[j].Split('=')[1] : "");
                    }
                } if (Tokens[i][0] == '{')
                    TokenTable.Add(Types.Indent, string.Empty, string.Empty);
                if (Tokens[i][0] == '}')
                    TokenTable.Add(Types.Dedent, string.Empty, string.Empty);
            }
            return TokenTable;
        }*/
        // new tokenizer
        private static string word = "";
        public static Tokenizer GetTokenTable(Source line)
        {
            Tokenizer TokensTable = new Tokenizer();
            word = "";
            bool isInterpolate = false;
            bool isParamArea = false;

            for (int index = 0; index < line.Value.Length; index++) {
                char term = line.Value[index];
                word += term;

                if (term == '"') isInterpolate = (isInterpolate) ? false : true;
                if (isInterpolate) continue;
                switch (term) {

                    case ' ':
                        if (toKey() == "fn") {
                            TokensTable.Add(Types.FunctionDescribement, line.GetWordWrapList(" ")[1], (line.Value.Split(' ').Count() > 2) ? true : false);
                            for (int i = 2; i < line.GetWordWrapList(" ").Count(); i++)
                                TokensTable.Add(Types.FunctionParameter, line.GetWordWrapList(" ")[i]);
                        }
                        else if (toKey() == "class")
                            TokensTable.Add(Types.ClassDescribement, line.Value.Split(' ')[1]);
                        else if (toKey() == "load")
                            for (int i = 1; i < line.Value.Split(' ').Count(); i++)
                                TokensTable.Add(Types.Load, line.Value.Split(' ')[i]);
                        else {
                            if (isParamArea) {
                                if (toKey(word).Replace("(", "")[0] != '$')
                                    TokensTable.Add(Types.FunctionParameter, word);
                                word = "";
                            }
                        }
                        break;
                    case '$':
                        string textToTake = line.Value.Substring(index);
                        if (Source.GetEmptyStringStatic(textToTake).Contains(')')) {
                            isParamArea = true;
                            try {
                                TokensTable.Add(Types.CallingFunction, textToTake.Split(' ')[0].Substring(1));
                            } catch (IndexOutOfRangeException) {
                                TokensTable.Add(Types.CallingFunction, textToTake.Substring(1, textToTake.IndexOf(')')));
                            }

                        } else {
                            try {
                                TokensTable.Add(Types.CallingFunction, textToTake.Split(' ')[0].Substring(1));
                            } catch (IndexOutOfRangeException) {
                                TokensTable.Add(Types.CallingFunction, toKey(textToTake).Substring(1));
                            }
                            List<string> paramsToTake = Source.GetWordWrapListStatic(textToTake, " ");
                            for (int i = 1; i < paramsToTake.Count(); i++) {
                                TokensTable.Add(Types.FunctionParameter, paramsToTake[i]);
                            }
                        }

                            break;
                    case ')':
                        if (isParamArea) isParamArea = false;
                        break;
                    case '(':
                        break;
                    case '=':
                        // configurare un opzione di riconoscimento condizioni, evitare di confondere '=' con '==' e '!=', magari usando operatori condizionali quali 'is' 'not' 'in' 'than'
                        TokensTable.Add(Types.VariableAssigment, toKey(word.Replace("=", "")), line.Value.Substring(line.Value.IndexOf('=')+1));
                        break;
                    case '+':
                        // sistema di espressioni
                        break;
                    case '-':
                        // sistema di espressioni
                        break;
                    case '*':
                        // sistema di espressioni
                        break;
                    case '/':
                        // sistema di espressioni
                        break;
                    default:
                        // else
                        break;
                }

            }

            return TokensTable;
        }

        private static string toKey(string s)
        {
            return s.Replace(" ", "");
        }
        private static string toKey()
        {
            return word.Replace(" ", "");
        }

        public void Add(Types _tokenType, string _tokenName, object _tokenValue = null)
        {
            TokenType.Add(_tokenType);
            TokenName.Add(_tokenName);
            TokenValue.Add(_tokenValue);
        }
        public enum Types {
            Undefined = 0,
            FunctionDescribement,
            CallingFunction,
            ClassDescribement,
            InitClass,
            FunctionParameter,
            String,
            Object,
            Load,
            VariableAssigment,
            CallingVariable,
            Use,
            Indent,
            Dedent,
            OpenBrack,
            CloseBrack,
        }
    }
    class Interpreter
    {
        public void Run(Storage ProcessTable)
        {
            Storage.Program Ram = new Storage.Program();
            BuiltIn.InitializeComponent(Ram);
            for (int i = 0; i <  ProcessTable.ProcessCounter; i++) {
                Process.RunProcess(ProcessTable.GetProcessType()[i], ProcessTable.GetProcessName()[i], ProcessTable.GetProcessArg()[i], Ram);
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
        public static ProcessIt[] ProcessIndex = {Console.Write, Console.WriteLine};
        public static void RunProcess(Type ProcessType, string ProcessName, object ProcessArg, Storage.Program Ram)
        {
            ProcessIt run = ProcessIndex[Convert.ToInt32(ProcessType)];
            run(ProcessName, ProcessArg, Ram);
        }
        public delegate void ProcessIt(string ProcessName, object ProcessArg, Storage.Program Ram);
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
            Print = 0, // print
            Println = 1, // print + \n
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
        public static string GetEmptyStringStatic(string s)
        {
            string toReturn = "";
            bool isInterpolate = false;
            foreach (char chr in s) {
                if (chr == '"') {
                    toReturn += '"';
                    isInterpolate = (isInterpolate) ? false : true;
                } else if (!isInterpolate)
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
        public string RemoveComments(string commentsChar)
        {
            string result = "";
            bool isInterpolate = false;
            for (int i = 0; i < Value.Length; i++) {
                if (Value[i] == '"') {
                    isInterpolate = (!isInterpolate) ? true : false;
                    result += '"';
                } else if (Value[i] == commentsChar[0] && Value[i + 1] == commentsChar[1] && !isInterpolate) {
                    break;
                } else {
                    result += Value[i];
                }
            }
            return result;
        }
        public List<string> GetWordWrapList(string PatternToSplit)
        {
            string result = "";
            bool isInterpolate = false;
            for (int j = 0; j < Value.Length; j++) {
                if (Value[j] == '"') {
                    isInterpolate = (!isInterpolate) ? true : false;
                    result += '"';
                    continue;
                }
                else if (isInterpolate) {
                    result += Value[j];
                    continue;
                }
                else if (PatternToSplit.Contains(Value[j])) {
                    result += '⳿';
                    continue;
                } else result+=Value[j];
            }
            List<string> toReturn = new List<string>();
            string[] resultSplit = result.Split('⳿');
            for (int i = 0; i < resultSplit.Count(); i++) {
                if (!string.IsNullOrWhiteSpace(resultSplit[i])) toReturn.Add(resultSplit[i]);
            }
            return toReturn;
        }
        public static List<string> GetWordWrapListStatic(string s, string PatternToSplit)
        {
            string result = "";
            bool isInterpolate = false;
            for (int j = 0; j < s.Length; j++) {
                if (s[j] == '"') {
                    isInterpolate = (!isInterpolate) ? true : false;
                    result += '"';
                    continue;
                } else if (isInterpolate) {
                    result += s[j];
                    continue;
                } else if (PatternToSplit.Contains(s[j])) {
                    result += '⳿';
                    continue;
                } else result += s[j];
            }
            List<string> toReturn = new List<string>();
            string[] resultSplit = result.Split('⳿');
            for (int i = 0; i < resultSplit.Count(); i++) {
                if (!string.IsNullOrWhiteSpace(resultSplit[i])) toReturn.Add(resultSplit[i]);
            }
            return toReturn;
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
