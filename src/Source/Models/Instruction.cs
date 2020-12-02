namespace RobinVM.Models
{
    public struct Instruction
    {
        public Instruction(Runtime.RuntimePointer functionPointer, object argument)
        {
            this.FunctionPointer = functionPointer;
            this.Argument = argument;
        }
        public static Instruction New(Runtime.RuntimePointer functionPointer, object argument) => new Instruction(functionPointer, argument);
        public static Instruction New(Runtime.RuntimePointer functionPointer) => new Instruction(functionPointer, null);
        public Runtime.RuntimePointer FunctionPointer;
        public object Argument;
    }
}