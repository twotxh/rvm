namespace RobinVM.Models
{
    public struct Function
    {
        public static Function New(Instruction[] instructions, string name) => new Function(instructions, name);
        public Function(Instruction[] instructions, string name)
        {
            if (!Runtime.SwitchFunctions.Contains(name))
                Runtime.SwitchFunctions.Add(name);
            else
                throw new System.ArgumentException("Function <`" + name + "`> already declared at: <0x" + Runtime.SwitchFunctions.IndexOf(name) + ">");
            Instructions = instructions;
        }
        public Instruction[] Instructions;
    }
}