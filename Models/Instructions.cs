struct Instruction {
    public Instruction(Runtime.compute instruction, dynamic[] args) {
        this.instruction = instruction;
        this.arguments = args;
    }
    public Runtime.compute instruction;
    public dynamic[] arguments;
}