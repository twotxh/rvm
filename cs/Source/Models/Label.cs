public struct Group {
    public static Group New(Instruction[] instructions, string name) => new Group(instructions, name);
    public Group(Instruction[] instructions, string name) {
        Instructions = instructions;
        if (!Runtime.Functions.Contains(name))
            Runtime.Functions.Add(name);
        else
            throw new System.ArgumentException("Function <`"+name+"`> already declared at: <0x"+Runtime.Functions.IndexOf(name)+">");
    }
    public Instruction[] Instructions;
}