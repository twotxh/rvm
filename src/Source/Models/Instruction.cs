public struct Instruction {
    public Instruction(Runtime.runtime instruction, dynamic args) {
        this.instruction = instruction;
        this.arguments = args;
    }
    public static Instruction New(Runtime.runtime instruction, dynamic arg) => new Instruction(instruction, arg);
    public static Instruction New(Runtime.runtime instruction) => new Instruction(instruction, null);
    public Runtime.runtime instruction;
    public dynamic arguments;
}