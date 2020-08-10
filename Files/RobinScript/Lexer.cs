using System;
using System.Collections.Generic;

namespace RobinScript
{
    enum Tokens {
        FunctionCall,
        FunctionDefine,
        FunctionParameter,
        ClassIstanziate,
        ClassDefine,
        ClassParameter,
        VariableDefine,
        ConditionIf,
        ConditionElif,
        ConditionElse,
        BinaryAdd,
        BinarySub,
        BinaryMul,
        BinaryDiv,
        OpenBracket,
        CloseBracket,
        Colon,
        Loop,
        While,
        For,
        Foreach,
        Break,
        Continue,
        CompareIs,
        CompareIsnot,
        As,
        CompareIn,
        CompareMajor,
        CompareMinor,
        MethodCall,
        PropertyCall,
        Return,
        Try,
        Except,
        Finally,
        BuildArray,
        BuildString,
        BuildInt,
        BuildDecimal,
        BuildBool,
        BuildEnumerable,
        BuildDictionary,
        VariableCall,
        FunctionReturnValue,
        Load,
        Use,
    }
    class Lexer
    {
        public int Count {
            get {
                return SyntaxTree.Token.Count;
            }
            set {
            }
        }
        public SyntaxTree SyntaxTree = new SyntaxTree();
        public static string CurrentLine = "";
        public static int CurrentLineNum = 0;
        public static Lexer Lex(string[] Code)
        {
            Lexer lexingCode = new Lexer();
            for (int i = 0; i < Code.Length; i++) {

                // for extern info
                CurrentLine = Code[i];
                CurrentLineNum = i;

            }
            return lexingCode;
        }
    }
    class SyntaxTree
    {
        public List<Tokens> Token { get; set; } = new List<Tokens>();
        public List<object> Value { get; set; } = new List<object>();
        public void Add(Tokens token, object value)
        {
            Token.Add(token);
            Value.Add(value);
        }
    }
}