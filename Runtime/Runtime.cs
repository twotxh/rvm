using System;

class Runtime {
    static Storage storage = new Storage();
    public static int InstructionIndex = 0;
    public static Group[] Labels;
    public static void StoreF(dynamic[] args) => storage[args[0]] = Registers.lod[0];
    public static void Store(dynamic[] args) => storage[args[0]] = args[1];
    public static void Call(dynamic[] args) => Rvm.ExecuteLabel(Labels[args[0]]);
    public static void Load(dynamic[] args) => Registers.lod = args;
    public static void LoadF(dynamic[] args) {
        Registers.lod = new dynamic[args.Length];
        for (int i = 0; i < args.Length; i++)
            Registers.lod[i] = storage[args[i]];
    }
    public static void Add(dynamic[] args) => storage[args[0]] += args[1];
    public static void Sub(dynamic[] args) => storage[args[0]] -= args[1];
    public static void Div(dynamic[] args) => storage[args[0]] /= args[1];
    public static void Mul(dynamic[] args) => storage[args[0]] *= args[1];
    public static void Pow(dynamic[] args) => Math.Pow(storage[args[0]], args[0]);
    public static void RvmOutput(dynamic[] args) => Console.Write(Registers.lod[0]);
    public static void RvmInput(dynamic[] args) => Registers.lod = new dynamic[] { Console.ReadLine() };
    public static void Compare(dynamic[] args) {
        if (Registers.lod[0] == Registers.lod[1])
            Registers.lod = new dynamic[] { 0 };
        else {
            double f = Registers.lod[0];
            double s = Registers.lod[1];
            if (f > s)
                Registers.lod = new dynamic[] { 1 };
            if (f < s)
                Registers.lod = new dynamic[] { 2 };
        }
    }
    public static void Nop(dynamic[] args) { }
    public static void End(dynamic[] args) => Environment.Exit(0);
    public static void CompareJE(dynamic[] args) {
        if (Registers.lod[0] == Registers.lod[1])
            InstructionIndex = args[0] - 1;
    }
    public static void CompareJNE(dynamic[] args) {
        if (Registers.lod[0] != Registers.lod[1])
            InstructionIndex = args[0] - 1;
    }
    public static void CompareJG(dynamic[] args) {
        if (Registers.lod[0] > Registers.lod[1])
            InstructionIndex = args[0] - 1;
    }
    public static void CompareJL(dynamic[] args) {
        if (Registers.lod[0] < Registers.lod[1])
            InstructionIndex = args[0] - 1;
    }
    public static void Jump(dynamic[] args) => InstructionIndex = args[0]-1;
}