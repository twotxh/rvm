struct Label {
    public int Length => Instructions.Length;
    public Instruction[] Instructions;
}
struct Group {
    public Group(Bytecode.compute[] instructions, dynamic[][] arguments) {
        Instructions = instructions;
        Arguments = arguments;
    }
    public Bytecode.compute[] Instructions;
    public dynamic[][] Arguments;
}