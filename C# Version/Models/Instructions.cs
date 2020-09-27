public struct Instruction {
    public Instruction(Runtime.compute instruction, dynamic[] args) {
        this.instruction = instruction;
        this.arguments = args;
    }
    public static Instruction New(Runtime.compute instruction, dynamic arg) => new Instruction(instruction, new dynamic[] { arg });
    public static Instruction New(Runtime.compute instruction, dynamic[] args) => new Instruction(instruction, args);
    public static Instruction New(Runtime.compute instruction) => new Instruction(instruction, null);
    public Runtime.compute instruction;
    public dynamic[] arguments;
}