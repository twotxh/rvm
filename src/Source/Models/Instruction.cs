namespace RobinVM.Models
{
    public struct Instruction
    {
        public Instruction(Runtime.runtime functionPointer, object argument)
        {
            this.FunctionPointer = functionPointer;
            this.Argument = argument;
        }
        public static Instruction New(Runtime.runtime functionPointer, object argument) => new Instruction(functionPointer, argument);
        public static Instruction New(Runtime.runtime functionPointer) => new Instruction(functionPointer, null);
        public Runtime.runtime FunctionPointer;
        public object Argument;
    }
}