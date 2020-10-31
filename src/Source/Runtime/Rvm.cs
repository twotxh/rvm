using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public static class Rvm {
    /// <summary>
    /// Performs main function of <paramref name="program"/>
    /// <code>
    /// <see cref="Instruction"/>[] main = {<br/>
    /// ••• <see cref="Instruction"/>.New(<see cref="Runtime"/>.Load, "Hello World!"),<br/>
    /// ••• <see cref="Instruction"/>.New(<see cref="Runtime"/>.RvmOutput),<br/>
    /// ••• <see cref="Instruction"/>.New(<see cref="Runtime"/>.Return),<br/>
    /// };<br/>
    /// <see cref="Function"/>[] program = {<br/>
    /// ••• <see cref="Function"/>.New(main, "main")<br/>
    /// };<br/>
    /// <see cref="Rvm"/>.Execute(program);
    /// </code>
    /// </summary>
    /// <param name="program">Functions</param>
    public static void Execute(Function[] program) {
        Runtime.RuntimeFunctions = program;
        ExecuteLabel(program[Runtime.SwitchFunctions.IndexOf("main")]);
    }
    public static void ExecuteLabel(Function label) {
        Storage x0 = Runtime.storage;
        int x1 = Runtime.InstructionIndex;
        Runtime.storage = new Storage();
        for (Runtime.InstructionIndex = 0; Runtime.InstructionIndex < label.Instructions.Length; Runtime.InstructionIndex++)
            label.Instructions[Runtime.InstructionIndex].instruction(label.Instructions[Runtime.InstructionIndex].arguments);
        Runtime.storage = x0;
        Runtime.InstructionIndex = x1;
    }
    struct Token
    {
        public static string[] KeyWords = new string[] {"load", "call", "ret"};
        public enum tokenKind
        {
            Identifier,
            ConstString,
            OpenBrace,
            CloseBrace,
            Colon,
            ConstInt,
            OpCode
        }
        readonly public tokenKind Kind;
        readonly public string Value;
        public Token(tokenKind kind, string value)
        {
            Kind = kind;
            Value = value;
        }
        public Token(tokenKind kind)
        {
            Kind = kind;
            Value = null;
        }
        public override string ToString() => "@Kind: " + Kind + " @Value: " + Value;
    }
    /// <summary>
    /// Returns a program performable by Execute(Function[])<br/>
    /// <code>
    /// var program = <see cref="Rvm"/>.CompileJit(<see cref="System.IO.File"/>.ReadAllText("bytecode.ext"));<br/>
    /// <see cref="Rvm"/>.Execute(program);
    /// </code>
    /// </summary>
    /// <param name="code"></param>
    public static Function[] CompileJit(string code)
    {
        List<Function> functions = new List<Function>();
        Token[] tokens = tokenize(code);
        Console.WriteLine(string.Join('\n', tokens));
        for (int i = 0; i < tokens.Length; i++)
        {
            if (tokens[i].Kind == Token.tokenKind.Identifier && tokens[i+1].Kind == Token.tokenKind.OpenBrace)
            functions.Add();
        }
        return functions.ToArray();
    }
    static Token[] tokenize(string code)
    {
        List<Token> tokens = new List<Token>();
        string token = "";
        bool str = false;
        for (int i = 0; i < code.Length; i++)
        {
            if (str)
            {
                token += code[i];
                if (code[i] == '"' && code[i - 1] != '\\')
                {
                    tokens.Add(new Token(Token.tokenKind.ConstString, token));
                    token = "";
                    str = false;
                }
            }
            else
            {
                if (char.IsLetterOrDigit(code[i]))
                    token += code[i];
                else
                {
                    if (token.Length != 0)
                    {
                        tokens.Add(new Token(isConst(token), token));
                        token = "";
                    }
                    switch (code[i])
                    {
                        case ':': tokens.Add(new Token(Token.tokenKind.Colon)); break;
                        case '{': tokens.Add(new Token(Token.tokenKind.OpenBrace)); break;
                        case '}': tokens.Add(new Token(Token.tokenKind.CloseBrace)); break;
                        case '"': token += '"'; str = true; break;
                    }
                }
            }
        }
        return tokens.ToArray();
    }
    static Token.tokenKind isConst(string token) => char.IsDigit(token[0]) ? Token.tokenKind.ConstInt : (Token.KeyWords.Contains(token) ? Token.tokenKind.OpCode : Token.tokenKind.Identifier);
}