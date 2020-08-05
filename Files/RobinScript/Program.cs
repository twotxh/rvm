using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobinScript
{

    [Serializable]
    public class Crash : Exception
    {
        public Crash() { }
        public Crash(string message) : base(message) { }
        public Crash(string message, Exception inner) : base(message, inner) { }
        protected Crash(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    enum ProcessTypes
    {
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
        Pause, // pause console process
        Init, // initialize a class in a variable
        Cast, // cast a variable: param -> (var_to_cast, int) where int is the type in to cast 'var_to_cast'
    }
    enum TokenTypes
    {
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
        Eol,
    }
    class Program
    { 
        static void Main(string[] args)
        {
            Console.Title = "RobinScript";
            switch (args.Count())
            {
                case 0:
                    Welcome();
                    Storage.Program Ram = new Storage.Program();
                    while (true) {
                        Console.Write("\n:: ");
                        try { Ram = Tools.ExecLine(Console.ReadLine(), Ram); } catch (Crash) { }
                    }
                default:
                    for (int i = 0; i < args.Count(); i++) {
                        try { Tools.ExecFile(args[i]); } catch (Crash) { Console.ReadKey(); continue; }
                        Tools.GetExecuteTime();
                    }break;
            }
        }

        private static void Welcome()
        {
            Console.Write("RobinScript 0.5 (32 bit/ 64 bit) - State: OpenSource, Licese: Apache Licese 2.0 \nAuthor: Carpal, Repository: https://github.com/Carpall/RobinScript");
        }
    }
    class Tools
    {
        public static int LineCounter = 0;
        public static string CurrentString = "";
        public static Storage.Program ExecLine(string _line, Storage.Program Ram)
        {
            CurrentString = _line;
            if (!string.IsNullOrWhiteSpace(_line))
            {
                Source Line = new Source();
                Line.Value = _line;
                Line.Value = Line.RemoveComments("//");
                Source LineEmptyString = new Source();
                LineEmptyString.Value = _line; LineEmptyString.Value = LineEmptyString.GetEmptyString();
                return Interpreter.Run(Lexer.GetProcessTable(Line, LineEmptyString), Ram);
            }
            return Ram;
        }
        private static System.Diagnostics.Stopwatch ExecuteTimer = new System.Diagnostics.Stopwatch();
        public static void ExecFile(string Path)
        {
            string[] Code = System.IO.File.ReadAllLines(Path);
            Source Line = new Source();
            Source LineEmptyString = new Source();
            Storage.Program Ram = new Storage.Program();
            ExecuteTimer.Start();
            for (int i = 0; i < Code.Count(); i++) {
                CurrentString = Code[i];
                if (!string.IsNullOrWhiteSpace(Code[i])) {
                    Line.Value = Code[i];
                    Line.Value = Line.RemoveComments("//");
                    LineEmptyString.Value = Line.GetEmptyString();
                    Ram = Interpreter.Run(Lexer.GetProcessTable(Line, LineEmptyString), Ram);
                }
                LineCounter++;
            }
            ExecuteTimer.Stop();
        }
        public static void GetExecuteTime()
        {
            Console.WriteLine("\nExecute time: {0}ms", ExecuteTimer.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
    class Lexer
    {

        public static bool isIndentArea = false; private static ProcessTypes _LastProcessType = ProcessTypes.Null; private static StringBuilder _LastProcessArg = new StringBuilder();
        public static ProcessTable GetProcessTable(Source Line, Source LineEmptyString)
        {
            // configurare un metodo di compressione delle espressioni racchiuse tra '[' e ']'                

            TokenTable TokenTable = TokenTable.GetTokenTable(Line);
            ProcessTable ProcessTable = new ProcessTable();
            for (int index = 0; index < TokenTable.TokenType.Count(); index++) {
                TokenTypes token = TokenTable.TokenType[index];
                Console.WriteLine("Type: {0} Name: {1} Value: {2}", TokenTable.TokenType[index], TokenTable.TokenName[index], TokenTable.TokenValue[index]);

                if (isIndentArea) {
                    if (token == TokenTypes.Dedent) {
                        isIndentArea = false;
                        ProcessTable.Add(_LastProcessType, new List<string>() { _LastProcessArg.ToString() });
                    } else
                        _LastProcessArg.AppendLine(Line.Value);
                    break;
                }
                if (token == TokenTypes.Indent) {
                    isIndentArea = true;
                    break;
                }

                switch (token) {

                    case TokenTypes.FunctionDescribement:
                        break;
                    case TokenTypes.CallingFunction:
                        if (TokenTable.TokenName[index] == "print")
                            ProcessTable.Add(ProcessTypes.Print, TokenTable.TokenName);
                        else if (TokenTable.TokenName[index] == "println")
                            ProcessTable.Add(ProcessTypes.Println, TokenTable.TokenName);
                        else if (TokenTable.TokenName[index] == "printf")
                            ProcessTable.Add(ProcessTypes.Printf, TokenTable.TokenName);
                        else if (TokenTable.TokenName[index] == "prints")
                            ProcessTable.Add(ProcessTypes.Prints, TokenTable.TokenName);
                        else if (TokenTable.TokenName[index] == "printlns")
                            ProcessTable.Add(ProcessTypes.Printlns, TokenTable.TokenName);
                        else {
                            List<string> parameters = new List<string>();
                            // collegare tutti i parametri alla lista, ciclando il token table a partire dall'index corrente
                            for (int i=index; i< TokenTable.TokenType.Count(); i++) {
                                if (TokenTable.TokenType[i] == TokenTypes.FunctionParameter)
                                    parameters.Add(TokenTable.TokenName[i]);
                                else
                                    break;
                            }
                            ProcessTable.Add(ProcessTypes.CallFunction, parameters);
                        }
                        break;
                    case TokenTypes.ClassDescribement:
                        break;
                    case TokenTypes.InitClass:
                        break;
                    case TokenTypes.String:
                        break;
                    case TokenTypes.Object:
                        break;
                    case TokenTypes.Load:
                        break;
                    case TokenTypes.VariableAssigment:
                        break;
                    case TokenTypes.CallingVariable:
                        break;
                    case TokenTypes.Use:
                        break;
                    case TokenTypes.Indent:
                        break;
                    case TokenTypes.Dedent:
                        break;
                    case TokenTypes.OpenBrack:
                        break;
                    case TokenTypes.CloseBrack:
                        break;
                    case TokenTypes.Eol:
                        goto EOL;
                    default:
                        break;
                }
            }

            EOL:
            return ProcessTable;
        }
    }
    class TokenTable
    {
        public List<TokenTypes> TokenType { get; set; } = new List<TokenTypes>();
        public List<string> TokenName { get; set; } = new List<string>();
        public List<object> TokenValue { get; set; } = new List<object>();

        private static string word = "";
        public static TokenTable GetTokenTable(Source line)
        {
            TokenTable TokensTable = new TokenTable();
            word = "";
            bool isInterpolate = false;
            bool isParamArea = false;
            //line.Value = line

            for (int index = 0; index < line.Value.Length; index++) {
                char term = line.Value[index];
                word += term;

                if (term == '"') isInterpolate = (isInterpolate) ? false : true;
                if (isInterpolate) continue;
                switch (term) {

                    case ' ':
                        if (toKey() == "fn") {
                            TokensTable.Add(TokenTypes.FunctionDescribement, line.GetWordWrapList(" ")[1], (line.Value.Split(' ').Count() > 2) ? true : false);
                            for (int i = 2; i < line.GetWordWrapList(" ").Count(); i++)
                                TokensTable.Add(TokenTypes.FunctionParameter, line.GetWordWrapList(" ")[i]);
                            TokensTable.Add(TokenTypes.Indent, null);
                        }
                        else if (toKey() == "class") {
                            TokensTable.Add(TokenTypes.ClassDescribement, line.Value.Split(' ')[1]);
                            TokensTable.Add(TokenTypes.Indent, null);
                        }
                        else if (toKey() == "load")
                            for (int i = 1; i < line.Value.Split(' ').Count(); i++)
                                TokensTable.Add(TokenTypes.Load, line.Value.Split(' ')[i]);
                        else {
                            if (isParamArea) {
                                if (toKey(word).Replace("(", "")[0] != '$')
                                    TokensTable.Add(TokenTypes.FunctionParameter, word);
                                word = "";
                            }
                        }
                        break;
                    case '$':
                        string textToTake = line.Value.Substring(index);
                        if (Source.GetEmptyStringStatic(textToTake).Contains(')')) {
                            isParamArea = true;
                            try {
                                TokensTable.Add(TokenTypes.CallingFunction, textToTake.Split(' ')[0].Substring(1));
                            } catch (IndexOutOfRangeException) {
                                TokensTable.Add(TokenTypes.CallingFunction, textToTake.Substring(1, textToTake.IndexOf(')')));
                            }

                        } else {
                            try {
                                TokensTable.Add(TokenTypes.CallingFunction, textToTake.Split(' ')[0].Substring(1));
                            } catch (IndexOutOfRangeException) {
                                TokensTable.Add(TokenTypes.CallingFunction, toKey(textToTake).Substring(1));
                            }
                            List<string> paramsToTake = Source.GetWordWrapListStatic(textToTake, " ");
                            for (int i = 1; i < paramsToTake.Count(); i++) {
                                TokensTable.Add(TokenTypes.FunctionParameter, paramsToTake[i]);
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
                        TokensTable.Add(TokenTypes.VariableAssigment, toKey(word.Replace("=", "")), line.Value.Substring(line.Value.IndexOf('=')+1));
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
                        if (toKey(word) == "end") {
                            TokensTable.Add(TokenTypes.Dedent, null);
                        }
                        break;
                }

            }
            TokensTable.Add(TokenTypes.Eol, null);

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

        public void Add(TokenTypes _tokenType, string _tokenName, object _tokenValue = null)
        {
            TokenType.Add(_tokenType);
            TokenName.Add(_tokenName);
            TokenValue.Add(_tokenValue);
        }
    }
    class Interpreter
    {
        public static Storage.Program Run(ProcessTable ProcessTable, Storage.Program Ram)
        {
            ProcessTypes ProcessType = ProcessTypes.Null;
            try {
                ProcessType = ProcessTable.ProcessType;
            } catch (ArgumentOutOfRangeException) { goto Finish; }
            List<string> ProcessArg = ProcessTable.ProcessArgument;

            switch (ProcessType) {
                case ProcessTypes.Print:
                    for (int i = 1; i < ProcessArg.Count(); i++)
                        Console.Write(ProcessArg[i]);
                    break;
                case ProcessTypes.Println:
                    for (int i = 1; i < ProcessArg.Count(); i++)
                        Console.Write(ProcessArg[i]);
                    Console.WriteLine();
                    break;
                case ProcessTypes.Printf:
                    string result = ProcessArg[1];
                    for (int i = 2; i < ProcessArg.Count(); i++)
                        result = result.Replace("{"+(i-2)+"}", ProcessArg[i]);
                    Console.Write(result);
                    break;
                case ProcessTypes.Prints:
                    try { for (int i = 1; i <= int.Parse(ProcessArg[2]); i++) Console.Write(ProcessArg[1]); } catch (ArgumentNullException) { Debuger.Except("Cannot find all requested parameters", $"Try with '{Tools.CurrentString} 5', where '5' is the times to spam first parameter"); } catch (FormatException) { Debuger.Except($"Cannot use '{ProcessArg[2]}' as int", $"Try with '{Tools.CurrentString.Remove(Tools.CurrentString.LastIndexOf(ProcessArg[2]))} 5', where '5' is the times to spam first parameter"); } catch (ArgumentOutOfRangeException) { Debuger.Except("Cannot find all requested parameters", $"Try with '{Tools.CurrentString} 5', where '5' is the times to spam first parameter"); }
                    break;
                case ProcessTypes.Printlns:
                    try { for (int i = 1; i <= int.Parse(ProcessArg[2]); i++) Console.WriteLine(ProcessArg[1]); } catch (ArgumentNullException) { Debuger.Except("Cannot find all requested parameters", $"Try with '{Tools.CurrentString} 5', where '5' is the times to spam first parameter"); } catch (FormatException) { Debuger.Except($"Cannot use '{ProcessArg[2]}' as int", $"Try with '{Tools.CurrentString.Remove(Tools.CurrentString.LastIndexOf(ProcessArg[2]))} 5', where '5' is the times to spam first parameter"); } catch (ArgumentOutOfRangeException) { Debuger.Except("Cannot find all requested parameters", $"Try with '{Tools.CurrentString} 5', where '5' is the times to spam first parameter"); }
                    break;
                case ProcessTypes.CallFunction:
                    // configurare un sistema di assegnamento e corrispondenza parametri funzione
                    Ram.GetFunction(ProcessTable.OptionalProcessAttribute, ProcessTable.ProcessArgument);
                    break;
                default:
                    Debuger.Except("Invalid istruction!", "Check the documentation to solve the problem");
                    break;
            }
            Finish:
            return Ram;
        }
    }
    class ProcessTable
    {
        public ProcessTypes ProcessType { get; set; } = new ProcessTypes();
        public List<string> ProcessArgument { get; set; } = new List<string>();
        public string OptionalProcessAttribute { get; set; } = "";
        public void Add(ProcessTypes pt, List<string> pa, string opa = "")
        {
            ProcessType = pt;
            ProcessArgument = pa;
            OptionalProcessAttribute = opa;
        }
    }
    class Storage
    {
        public class Program
        {
            // memory
            private Dictionary<string, object> Variables = new Dictionary<string, object>();
            private Dictionary<Tuple<string, List<string>>, Source> Functions = new Dictionary<Tuple<string, List<string>>, Source>();
            private Dictionary<string, Source> Classes = new Dictionary<string, Source>();
            // get
            public string GetVariable(string name)
            {
                if (Variables.ContainsKey(name))
                    return Variables[name].ToString();
                else
                    return null;
            }
            public Source GetFunction(string name, List<string> param)
            {
                if (Functions.ContainsKey(new Tuple<string, string>(name, param)))
                    return Functions[new Tuple<string, string>(name, param)].ToString();
                Debuger.Except($"'{name}' function does not definied jet!", $"Check the function name");
                return null;
            }
            public Source GetClass(string name)
            {
                if (Classes.ContainsKey(name))
                    return Classes[name].ToString();
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
            public void InitFunction(string key, List<string> param, Source value)
            {
                if (!Functions.ContainsKey(new Tuple<string, List<string>>(key, param)))
                    Functions.Add(new Tuple<string, List<string>>(key, param), value);
                else
                    Debuger.Except($"'{key}' function exists jet!", $"Try to rename it, example: '{key}NewFunction'");
            }
            public void InitClass(string key, Source value)
            {
                if (!Classes.ContainsKey(key))
                    Classes.Add(key, value);
                else
                    throw new Exception("'" + key + "' already exists!");
            }
        }
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
        public static void Except(string error, string tip)
        {
            string istruction = Tools.CurrentString + '\n';
            for (int i = 0; i < Tools.LineCounter.ToString().Length + 3; i++)
                istruction += ' ';
            for (int j=0;j< Tools.CurrentString.Count();j++) {
                istruction += '^';
            }
            istruction+= " -> ";
            Console.WriteLine(Tools.LineCounter + " | "+istruction+error+"\nTip: "+tip);
            throw new Crash();
        }
    }
}