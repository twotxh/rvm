using System;
using System.IO;
public static class BuiltInRuntime {
   public static void ReadFromFile() => Registers.lod = new dynamic[] { File.ReadAllText(Registers.par[1]) };
   public static void WriteInFile() => File.WriteAllText(Registers.par[1], Registers.par[2]);
   public static void CheckFile() => Registers.lod = new dynamic[] { File.Exists(Registers.par[1]) };
}