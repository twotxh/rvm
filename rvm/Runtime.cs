using System;

class Runtime {
    public static Storage storage = new Storage();
    public static int InstructionIndex = 0;
    public static Group[] Labels;
    public static void Load(object[] args) => storage[Convert.ToInt16(args[0])] = args[1];
}