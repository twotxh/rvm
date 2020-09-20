struct Instruction {
    public Instruction(string instruction, dynamic[] args) {
        this.instruction = instruction;
        this.arguments = args;
    }
    public string instruction;
    public dynamic[] arguments;
}