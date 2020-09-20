struct Instruction {
    public Instruction(string instruction, object[] args) {
        this.instruction = instruction;
        this.arguments = args;
    }
    public string instruction;
    public object[] arguments;
}