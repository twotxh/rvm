struct Label {
    public int Length => Instructions.Length;
    public Instruction[] Instructions;
}
struct Group {
    public Group(Bytecode.compute[] instructions, object[][] arguments) {
        Instructions = instructions;
        Arguments = arguments;
    }
    public Bytecode.compute[] Instructions;
    public object[][] Arguments;
}