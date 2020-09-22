using System.Collections.Generic;

/*class BytecodeBuilder {
    List<Label> labels = new List<Label>();
    List<Instruction> instructions = new List<Instruction>();
    public Bytecode Build() => new Bytecode() { Labels = labels.ToArray() };
    public void AppendLabel() { labels.Add(new Label() { Instructions = instructions.ToArray() }); instructions.Clear(); }
    public void AppendInstruction(string instruction, dynamic[] arguments) => instructions.Add(new Instruction() { instruction = instruction, arguments = arguments });
}*/