using System.Collections.Generic;
static class Isa {
    public static Dictionary<string, Bytecode.compute> Get { get; } = new Dictionary<string, Bytecode.compute>() {
        {"LOAD", Runtime.Load },
    };
}
