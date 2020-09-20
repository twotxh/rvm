using System.Collections.Generic;
static class Isa {
    public static Dictionary<string, Bytecode.compute> Get { get; } = new Dictionary<string, Bytecode.compute>() {
        {"STORE", Runtime.Store },
        {"STOREF", Runtime.StoreF },
        {"LOAD", Runtime.Load },
        {"LOADF", Runtime.LoadF },
        {"END", Runtime.End },
        {"CALL", Runtime.Call },
        {"JUMP", Runtime.Jump },
        {"CMP", Runtime.Compare },
        {"CMPJE", Runtime.CompareJE }, // jump equals
        {"CMPJNE", Runtime.CompareJNE }, // jump not equals
        {"CMPJG", Runtime.CompareJG }, // jump greater
        {"CMPJL", Runtime.CompareJL }, // jump less
        {"PUSH", Runtime.Push },
        {"POP", Runtime.Pop },
        {"ADD", Runtime.Add },
        {"SUB", Runtime.Sub },
        {"DIV", Runtime.Div },
        {"MUL", Runtime.Mul },
        {"POW", Runtime.Pow },
        {"NOP", Runtime.Nop },
        {"RVM_OUT", Runtime.RvmOutput },
        {"RVM_IN", Runtime.RvmInput },
    };
}
