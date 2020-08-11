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
        protected Crash(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    } //
    enum ProcessTypes
    {
        Null,
        NewFunction,
        NewClass,
        For,
        While,
        Loop,
        If,
        Elseif,
        Else,
        SetVariable,
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
    }  //
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
        OpAdd,
        OpSub,
        OpSplit,
        OpMolt,
        Eol,
    } //
    enum VariableTypes
    {
        Function,
        Variable,
        Class,
        Const_Int,
        Const_String,
        Const_Bool,
        Const_Float,
    } //
    class Program
    {
        static void Main(string[] args)
        {
            switch (args.Count())
            {
                case 0:
                    Console.Title = "RobinScript";
                    Welcome();
                    while (true) {
                        Console.Write("\n:: ");
                        try { Tools.ExecLine(Console.ReadLine()); } catch (Crash) { }
                    }
                default:
                    for (int i = 0; i < args.Count(); i++) {
                        if (args[i].ToLower().Replace(" ", "") == "-c") {
                            var icc = new Microsoft.CSharp.CSharpCodeProvider().CreateCompiler();
                            var parameters = new System.CodeDom.Compiler.CompilerParameters() { GenerateExecutable = true, OutputAssembly = args[i].Remove(args[i].LastIndexOf('.')) + ".exe"};
                            var results = icc.CompileAssemblyFromSource(parameters, Tools.Compile(args[i+1]));
                            Console.WriteLine("Compiled in "+args[i+1].Remove(args[i].LastIndexOf('.')) + ".exe");
                        }
                        Console.Title = args[i];
                        try { Tools.ExecFile(args[i]); Storage.Reset(); Tools.LineCounter = 0; } catch (Crash) { Console.ReadKey(); continue; }
                        Tools.GetExecuteTime();
                    }break;
            }
        }

        private static void Welcome()
        {
            Console.Write("RobinScript 0.5 (32 bit/ 64 bit) - State: OpenSource, Licese: Apache Licese 2.0 \nAuthor: Carpal, Repository: https://github.com/Carpall/RobinScript");
        }
    } // 
    class Tools
    {
        //public static string CompilerService;
        public static int LineCounter = 0;
        public static string CurrentString = "";
        private static System.Diagnostics.Stopwatch ExecuteTimer = new System.Diagnostics.Stopwatch();

        public static void ExecLine(string _line)
        {
            CurrentString = _line;
            if (!string.IsNullOrWhiteSpace(_line))
                Interpreter.Run(Lexer.GetProcessTable(new Source() { Value = Source.RemoveCommentsStatic(_line) }, new Source() { Value = Source.GetEmptyStringStatic(Source.RemoveCommentsStatic(_line)) }));
        }
        public static void ExecFile(string Path) 
        {
            string[] Code = System.IO.File.ReadAllLines(Path);
            ExecuteTimer.Start();
            for (int i = 0; i < Code.Count(); i++) {
                CurrentString = Code[i];
                if (!string.IsNullOrWhiteSpace(Code[i])) {
                    Interpreter.Run(Lexer.GetProcessTable(new Source() { Value = Source.RemoveCommentsStatic(Code[i]) }, new Source() { Value = Source.GetEmptyStringStatic(Source.RemoveCommentsStatic(Code[i])) }));
                }
                LineCounter++;
            }
            ExecuteTimer.Stop();
        }
        public static void ExecCode(string _code)
        {
            string[] Code = _code.Split(new char[] { '\r', '\n' });
            for (int i = 0; i < Code.Count(); i++) {
                CurrentString = Code[i];
                if (!string.IsNullOrWhiteSpace(Code[i])) {
                    Interpreter.Run(Lexer.GetProcessTable(new Source() { Value = Source.RemoveCommentsStatic(Code[i]) }, new Source() { Value = Source.GetEmptyStringStatic(Source.RemoveCommentsStatic(Code[i])) }));
                }
                LineCounter++;
            }
        }
        public static string Compile(string Path)
        {
            //CompilerService = "using System;using System.Collections.Generic;using System.Text;using System.Linq;namespace RobinCompiled{ class Program { static void Main(string[] args) {string[] code = \""+System.IO.File.ReadAllText(Path)+"\".Split(new char[] {'\n', '\r'}); List<List<ProcessTable>> Bytecode = new List<string>();for (int i=0;i<code.Count();i++){Bytecode.Add()}}} }" +
            //    "class Debuger { public static void Except(string error, string tip){ string istruction = Tools.CurrentString + '\n';for (int i = 0; i < Tools.LineCounter.ToString().Length + 3; i++)istruction += ' ';for (int j=0;j< Tools.CurrentString.Count();j++) {istruction += '^';} istruction+= \" -> \"; Console.WriteLine(Tools.LineCounter + \" | \"+istruction+error+\"\nTip: \"+tip);throw new Crash();} }" +
            //    "class Interpreter { public static void Run(ProcessTable ProcessTable) { ProcessTypes ProcessType = ProcessTypes.Null; try { ProcessType = ProcessTable.ProcessType; } catch (ArgumentOutOfRangeException) { goto Finish; } List<string> ProcessArg = ProcessTable.ProcessArgument; StringBuilder result = new StringBuilder(); switch (ProcessType) { case ProcessTypes.Print:for (int i = 1; i < ProcessArg.Count(); i++)result.Append(Source.FixString(ProcessArg[i]));Console.Write(result);break;case ProcessTypes.Println:for (int i = 1; i < ProcessArg.Count(); i++)result.Append(Source.FixString(ProcessArg[i]));Console.WriteLine(result);break;case ProcessTypes.Printf:string tmp = \"for (int i = 2; i < ProcessArg.Count(); i++tmp = tmp.Replace('{'+(i-2)+'}', ProcessArg[i]);Console.Write(Source.FixString(tmp));break;case ProcessTypes.Prints:try { for (int i = 1; i <= int.Parse(ProcessArg[2]); i++) result.Append(Source.FixString(ProcessArg[1])); Console.Write(result); } catch (ArgumentNullException) { Debuger.Except(\"Cannot find all requested parameters\", $\"Try with \'{Tools.CurrentString} 5\', where \'5\' is the times to spam first parameter\"); } catch (FormatException) { Debuger.Except($\"Cannot use \'{ProcessArg[2]}\' as int\", $\"Try with '{Tools.CurrentString.Remove(Tools.CurrentString.LastIndexOf(ProcessArg[2]))} 5\', where \'5\' is the times to spam first parameter\"); } catch (ArgumentOutOfRangeException) { Debuger.Except(\"Cannot find all requested parameters\", $\"Try with '{Tools.CurrentString} 5', where '5' is the times to spam first parameter\"); } break;case ProcessTypes.Printlnstry { for (int i = 1; i <= int.Parse(ProcessArg[2]); i++) result.AppendLine(Source.FixString(ProcessArg[1])); Console.Write(result); } catch (ArgumentNullException) { Debuger.Except(\"Cannot find all requested parameters\", $\"Try with '{Tools.CurrentString} 5', where '5' is the times to spam first parameter\"); } catch (FormatException) { Debuger.Except($\"Cannot use '{ProcessArg[2]}' as int\", $\"Try with '{Tools.CurrentString.Remove(Tools.CurrentString.LastIndexOf(ProcessArg[2]))} 5', where '5' is the times to spam first parameter\"); } catch (ArgumentOutOfRangeException) { Debuger.Except(\"Cannot find all requested parameters\", $\"Try with '{Tools.CurrentString} 5', where '5' is the times to spam first parameter\"); break;case ProcessTypes.CallFunction:Storage.GetFunctionReturnValue(ProcessTable.OptionalProcessAttribute, ProcessArg);break;default:Debuger.Except(\"Invalid istruction!\", \"Check the documentation to solve the problem\");break;}Finish:;}}" +
            //    "class ProcessTable { public ProcessTypes ProcessType { get; set; } = new ProcessTypes(); public List<string> ProcessArgument { get; set; } = new List<string>(); public string OptionalProcessAttribute { get; set; } = \"\"; public void Add(ProcessTypes pt, List<string> pa, string opa = \"\") { ProcessType = pt; ProcessArgument = pa; OptionalProcessAttribute = opa;}}"+
            //    "}";
            return "";
        }
        public static void GetExecuteTime()
        {
            Console.WriteLine("\nExecute time: {0}ms", ExecuteTimer.ElapsedMilliseconds);
            Console.ReadKey();
        }
    } //
    class Lexer
    {

        public static bool isIndentArea = false;
        private static ProcessTypes _LastProcessType = ProcessTypes.Null;
        private static StringBuilder _LastProcessArg = new StringBuilder();
        private static List<string> _LastProcessParameters = new List<string>();
        private static string _LastProcessName = "";
        public static ProcessTable GetProcessTable(Source Line, Source LineEmptyString)
        {
            // configurare un metodo di compressione delle espressioni racchiuse tra '[' e ']'                
            // configurare un metodo di riconoscimento delle condizioni e tramite della variabili booleane controllare la propria esecuzione
            // per le condizioni 'if' e 'elif' controllare il numero di elementi dello split di ' ', in caso 3 -> 'arg0' 'op' 'arg1', in caso 1 -> metodo booleano sottoposto ad una variabile in posizione 'arg0'

            TokenTable TokenTable = TokenTable.GetTokenTable(Line);
            ProcessTable ProcessTable = new ProcessTable();
            for (int index = 0; index < TokenTable.TokenType.Count(); index++) {
                TokenTypes token = TokenTable.TokenType[index];
                Console.WriteLine("Type: {0} Name: {1} Value: {2}", TokenTable.TokenType[index], TokenTable.TokenName[index], TokenTable.TokenValue[index]);

                if (isIndentArea) {
                    if (token == TokenTypes.Dedent) {
                        isIndentArea = false;
                        ProcessTable.Add(_LastProcessType, _LastProcessParameters, _LastProcessName+" "+_LastProcessArg.ToString() );
                        continue;
                    } else
                        _LastProcessArg.AppendLine(Line.Value);
                    break;
                }

                switch (token) {
                    case TokenTypes.Indent:
                        isIndentArea = true;
                        break;
                    case TokenTypes.FunctionDescribement:
                        List<string> parameters = new List<string>();
                        for (int i = index; i < TokenTable.TokenType.Count(); i++) {
                            if (TokenTable.TokenType[i] == TokenTypes.FunctionParameter)
                                parameters.Add(TokenTable.TokenName[i]);
                        }
                        _LastProcessName = TokenTable.TokenName[index];
                        _LastProcessParameters = parameters;
                        _LastProcessType = ProcessTypes.NewFunction;
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
                            List<string> result = new List<string>();
                            for (int i = 0; i < TokenTable.TokenType.Count(); i++) {
                                if (TokenTable.TokenType[i] == TokenTypes.FunctionParameter)
                                    result.Add(Storage.FixValue(TokenTable, TokenTable.TokenName[i]).ToString());
                            }
                            ProcessTable.Add(ProcessTypes.CallFunction, result, TokenTable.TokenName[index]);
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
                        object value = Storage.FixValue(TokenTable, TokenTable.TokenValue[index]);
                        ProcessTable.Add(ProcessTypes.SetVariable, new List<string>() { TokenTable.TokenName[index] , value.ToString() } );
                        goto EOL;
                        break;
                    case TokenTypes.CallingVariable:
                        break;
                    case TokenTypes.Use:
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
    } //
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

            for (int index = 0; index < line.Value.Length; index++) {
                char term = line.Value[index];
                word += term;

                if (term == '"' || term == '\'') isInterpolate = (isInterpolate) ? false : true;
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
        public void RemoveAt(int index)
        {
            TokenType.RemoveAt(index);
            TokenName.RemoveAt(index);
            TokenValue.RemoveAt(index);
        }
    } //
    class Interpreter
    {
        public static void Run(ProcessTable ProcessTable)
        {
            ProcessTypes ProcessType = ProcessTypes.Null;
            try {
                ProcessType = ProcessTable.ProcessType;
            } catch (ArgumentOutOfRangeException) { goto Finish; }
            List<string> ProcessArg = ProcessTable.ProcessArgument;
            StringBuilder result = new StringBuilder();

            switch (ProcessType) {
                case ProcessTypes.Print:
                    for (int i = 1; i < ProcessArg.Count(); i++)
                        result.Append(Source.FixString(ProcessArg[i]));
                    Console.Write(result);
                    break;
                case ProcessTypes.Println:
                    for (int i = 1; i < ProcessArg.Count(); i++)
                        result.Append(Source.FixString(ProcessArg[i]));
                    Console.WriteLine(result);
                    break;
                case ProcessTypes.Printf:
                    string tmp = "";
                    for (int i = 2; i < ProcessArg.Count(); i++)
                        tmp = tmp.Replace("{"+(i-2)+"}", ProcessArg[i]);
                    Console.Write(Source.FixString(tmp));
                    break;
                case ProcessTypes.Prints:
                    try { for (int i = 1; i <= int.Parse(ProcessArg[2]); i++) result.Append(Source.FixString(ProcessArg[1])); Console.Write(result); } catch (ArgumentNullException) { Debuger.Except("Cannot find all requested parameters", $"Try with '{Tools.CurrentString} 5', where '5' is the times to spam first parameter"); } catch (FormatException) { Debuger.Except($"Cannot use '{ProcessArg[2]}' as int", $"Try with '{Tools.CurrentString.Remove(Tools.CurrentString.LastIndexOf(ProcessArg[2]))} 5', where '5' is the times to spam first parameter"); } catch (ArgumentOutOfRangeException) { Debuger.Except("Cannot find all requested parameters", $"Try with '{Tools.CurrentString} 5', where '5' is the times to spam first parameter"); }
                    break;
                case ProcessTypes.Printlns:
                    try { for (int i = 1; i <= int.Parse(ProcessArg[2]); i++) result.AppendLine(Source.FixString(ProcessArg[1])); Console.Write(result); } catch (ArgumentNullException) { Debuger.Except("Cannot find all requested parameters", $"Try with '{Tools.CurrentString} 5', where '5' is the times to spam first parameter"); } catch (FormatException) { Debuger.Except($"Cannot use '{ProcessArg[2]}' as int", $"Try with '{Tools.CurrentString.Remove(Tools.CurrentString.LastIndexOf(ProcessArg[2]))} 5', where '5' is the times to spam first parameter"); } catch (ArgumentOutOfRangeException) { Debuger.Except("Cannot find all requested parameters", $"Try with '{Tools.CurrentString} 5', where '5' is the times to spam first parameter"); }
                    break;
                    /// -- ///
                case ProcessTypes.CallFunction:
                    Storage.GetFunctionReturnValue(ProcessTable.OptionalProcessAttribute, ProcessArg);
                    break;
                case ProcessTypes.SetVariable:
                    Storage.SetVariable(ProcessTable.ProcessArgument[0], ProcessTable.ProcessArgument[1]);
                    break;
                case ProcessTypes.NewFunction:
                    Storage.NewFunction(ProcessTable.OptionalProcessAttribute.Remove(ProcessTable.OptionalProcessAttribute.IndexOf(' ')), ProcessTable.ProcessArgument, ProcessTable.OptionalProcessAttribute.Substring(ProcessTable.OptionalProcessAttribute.IndexOf(' ')));
                    break;
                default:
                    Debuger.Except("Invalid istruction!", "Check the documentation to solve the problem");
                    break;
            }
            Finish:;
        }
    } //
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
    }  //
    class Storage
    {
        // memory
        static private Dictionary<string, object> Variables = new Dictionary<string, object>();
        static private Dictionary<string, string> Functions = new Dictionary<string, string>();
        static private Dictionary<string, string> Classes = new Dictionary<string, string>();
        // get
        static public object GetVariable(string name)
        {
            if (Variables.ContainsKey(name))
                return Variables[name];
            else
                Debuger.Except($"'{name}' is not definied yet!", $"Define your variable, Example: '{name} = 5' where '5' is the value of '{name}'");
            return null;
        }
        static public string GetFunction(string name)
        {
            if (Functions.ContainsKey(name))
                return Functions[name];
            Debuger.Except($"'{name}' function does not definied jet!", "Check the function name");
            return null;
        }
        static public string GetClass(string name)
        {
            if (Classes.ContainsKey(name))
                return Classes[name];
            else
                return null;
        }
        static public object GetFunctionReturnValue(string name, List<string> paramaters)
        {
            for (int i = 0; i < paramaters.Count(); i++)
                SetVariable(Functions[name].Split(' ')[i+1], paramaters[i]);
            Tools.ExecCode(GetFunction(name));
            return null;
        }
        static public object FixValue(TokenTable tokenTable, object value)
        {
            string result = "";
            for (int i = 0; i < tokenTable.TokenType.Count(); i++) {
                if (tokenTable.TokenType[i] == TokenTypes.CallingFunction) {
                    List<string> parameters = new List<string>();
                    for (int j = i; j < tokenTable.TokenType.Count(); j++) {
                        if (tokenTable.TokenType[i] == TokenTypes.FunctionParameter)
                            parameters.Add(tokenTable.TokenName[j]);
                    } result += GetFunctionReturnValue(tokenTable.TokenName[i], parameters);
                } else if (tokenTable.TokenType[i] != TokenTypes.FunctionParameter) {
                    if (tokenTable.TokenName[i].Contains('"') || tokenTable.TokenName[i].Contains('\''))
                        result += Source.FixString(result);
                    else {
                        string varCharacters = "abcdefghilmnopqrstuvzxywkj_òèéòçàù§";
                        for (int k = 0; k < varCharacters.Length; k++) {
                            if (tokenTable.TokenName[i].ToLower().Contains(varCharacters[k])) {
                                result += GetVariable(tokenTable.TokenName[i]);
                                break;
                            }
                            else if (k == varCharacters.Length - 1) {
                                result += tokenTable.TokenName[i];
                            }
                        }
                    }
                }
            }
            try {
                return new System.Data.DataTable().Compute(result, "");
            } catch (Exception) {
                return result;
            }
        }
        // set or init
        static public void SetVariable(string name, object value)
        {
            if (!Variables.ContainsKey(name))
                Variables.Add(name, value);
            else
                Variables[name] = value;
        }
        static public void NewFunction(string name, List<string> parameters, string value)
        {
            if (!Functions.ContainsKey(name)) {
                string param = "";
                for (int i = 0; i < parameters.Count(); i++) {
                    param += ' ' + parameters[i];
                }
                Functions.Add(name+param, value);
            }
            else
                Debuger.Except($"'{name}' function exists yet!", $"Try to rename it, example: '{name}NewFunction'");
        }
        static public void NewClass(string key, string value)
        {
            if (!Classes.ContainsKey(key))
                Classes.Add(key, value);
            else
                throw new Exception("'" + key + "' already exists!");
        }
        static public void Reset()
        {
            Variables.Clear();
            Functions.Clear();
            Classes.Clear();
        }
    } //
    class Source
    {
        public string Value { get; set; } = "";
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
        public static string RemoveCommentsStatic (string line)
        {
            string result = "";
            bool isInterpolate = false;
            for (int i = 0; i < line.Length; i++) {
                if (line[i] == '"') {
                    isInterpolate = (!isInterpolate) ? true : false;
                    result += '"';
                } else if (line[i] == '/' && line[i + 1] == '/' && !isInterpolate) {
                    break;
                } else {
                    result += line[i];
                }
            }
            return result;
        }
        public List<string> GetWordWrapList(string PatternToSplit)
        {
            string result = "";
            bool isInterpolate = false;
            for (int j = 0; j < Value.Length; j++) {
                if (Value[j] == '"' || Value[j] == '\'') {
                    isInterpolate = (!isInterpolate) ? true : false;
                    result += Value[j];
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
        public static string[] SmartSplit(string text, char charToSplit)
        {
            string[] result = "";
            int count = 0;
            bool isInterpolate = false;
            for (int i = 0; i < text.Lenght; i++){
                if (text[i] == charToSplit)
                if (text[i] == '\'') {
                    isInterpolate = (isInterpolate) ? false : true;
                    result[count] +='\'';
                }
            }
            return result;
        }
        public static string FixString(string toFix)
        {
            try {
                return toFix
                .Replace("\\n", "\n")
                .Replace("\\r", "\r")
                .Replace("\\t", "\t")
                .Replace("\\b", "\b")
                .Replace("\\a", "\a")
                .Replace("\\v", "\v")
                .Replace("\\\"", "[double_intentional_apex]")
                .Replace("\\'", "[single_intentional_apex]")
                .Replace("\"", "")
                .Replace("'", "")
                .Replace("[single_intentional_apex]", "'")
                .Replace("[double_intentional_apex]", "\"");
            } catch (ArgumentNullException) { } catch (NullReferenceException) { }
            return toFix;
        }
    } //
    class Debuger
    {
        public static void print(string toPrint, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null) { Console.WriteLine(toPrint + ' ' + arg0 + ' ' + arg1 + ' ' + arg2 + ' ' + arg3); }
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
    } //
}