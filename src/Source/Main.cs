using System;
using System.Diagnostics;

class Program {
    static void Main(string[] args) {
        Rvm.CompileJit(@"
main {
    load ""Hello World""
    call rvmoutput
    end: ret
}
");
    }
}