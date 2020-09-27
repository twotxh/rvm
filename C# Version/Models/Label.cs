public struct Group {
    public static Group New(Instruction[] instructions) => new Group(instructions);
    public Group(Instruction[] instructions) => Instructions = instructions;
    public Instruction[] Instructions;
}