namespace RobinVM.Models
{
    public struct Function
    {
        public static Function New(Instruction[] instructions) => new Function(instructions);
        public Function(Instruction[] instructions)
        {
            Instructions = instructions;
            Arguments = null;
        }
        public object[] Arguments;
        public Instruction[] Instructions;
    }
}