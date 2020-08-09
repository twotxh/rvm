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
                return SyntaxTree.Token.Length;
            }
            set {
            }
        }
        public SyntaxTree SyntaxTree = new SyntaxTree();
        public static Lexer Lex(string[] Code)
        {
            Lexer lexingCode = new Lexer();
            for (int i = 0; i < Code.Length; i++) {
            }
            return lexingCode;
        }
    }
    class SyntaxTree
    {
        public Tokens[] Token { get; set; }
        public object[] Value { get; set; }
        private int counter = 0;
        public void Add(Tokens token, object value)
        {
            Token[counter] = token;
            Value[counter] = value;
            counter++;
        }
    }
}