using System;
using System.Diagnostics;
using System.Collections.Generic;
using RobinVM.Models;

using CacheTable = System.Collections.Generic.Dictionary<string, object>;
namespace RobinVM
{
    public static class Runtime
    {
        public delegate void RuntimePointer(object args);
        public delegate void CallPointer();
        public static object[] Storage = new object[byte.MaxValue];
        public static int ProgramCounter = 0;
        public static Image RuntimeImage;
        public static Function CurrentFunctionPointer;
        public static readonly RStack Stack = new RStack(16);

        /// <summary>
        /// Casts last element onto the stack to int32 and push result
        /// </summary>
        /// <param name="args"></param>
        public static void CastToInt(object args) => Stack.Push(Convert.ToInt32(Stack.Pop()));

        /// <summary>
        /// Casts last element onto the stack to int32 and push result
        /// </summary>
        /// <param name="args"></param>
        public static void Cast<T>(object args) => Stack.Push(Convert.ChangeType(Stack.Pop(), typeof(T)));

        /// <summary>
        /// Casts last element onto the stack to float and push result
        /// </summary>
        /// <param name="args"></param>
        public static void CastToFloat(object args) => Stack.Push(Convert.ToSingle(Stack.Pop()));

        /// <summary>
        /// Casts last element onto the stack to bool and push result
        /// </summary>
        /// <param name="args"></param>
        public static void CastToBool(object args) => Stack.Push(Convert.ToBoolean(Stack.Pop()));

        /// <summary>
        /// Casts last element onto the stack to string and push result
        /// </summary>
        /// <param name="args"></param>
        public static void CastToString(object args) => Stack.Push(Stack.Pop().ToString());
        /// <summary>
        /// Stores the value onto the stack in the local heap
        /// </summary>
        /// <param name="args">Index of the local heap into store last stack element</param>
        public static void Store(object args) => Storage[Convert.ToByte(args)] = Stack.Pop();

        /// <summary>
        /// Calls a function
        /// </summary>
        /// <param name="args">Index of function to call</param>
        public static void Call(object args) => RuntimeImage.FindFunction((string)args).ExecuteLabel();

        /// <summary>
        /// Loads onto the stack a constant
        /// </summary>
        /// <param name="args">Constant to load onto the stack</param>
        public static void Load(object args) => Stack.Push(args);

        /// <summary>
        /// Loads onto the stack a global variable
        /// </summary>
        /// <param name="args">Global variable id</param>
        public static void LoadGlobal(object args) => Stack.Push(((CacheTable)Stack.Pop())[(string)args]);

        /// <summary>
        /// Loads onto the stack a constant
        /// </summary>
        /// <param name="args">Constant to load onto the stack</param>
        public static void LoadFromArgs(object args) => Stack.Push(CurrentFunctionPointer.Arguments[Convert.ToByte(args)]);

        /// <summary>
        /// Loads onto the stack a global variable
        /// </summary>
        /// <param name="args">G-lobal variable id</param>
        public static void LoadRuntimeImage(object args) => Stack.Push(RuntimeImage.GetCacheTable());

        /// <summary>
        /// Takes last element on the stack as array, the second last as index of the element to change and tird last as value to replace with,
        /// changes the value and push it onto the stack
        /// </summary>
        /// <param name="args"></param>
        /// <typeparam name="T">Type of array<br/>Example: int[] => StoreElementIntoArray&lt;int&gt;()</typeparam>
        public static void StoreElementIntoArray(object args)
        {
            object arr = Stack.Pop();
            ((object[])arr)[(int)Stack.Pop()] = Stack.Pop();
            Stack.Push(arr);
        }

        /// <summary>
        /// Takes last element on the stack as array, the second last as index of the element to get and
        /// pushes it onto the stack
        /// </summary>
        /// <param name="args"></param>
        /// <typeparam name="T">Type of array<br/>Example: int[] => StoreElementIntoArray&lt;int&gt;()</typeparam>
        public static void LoadElementFromArray(object args) => Stack.Push(((object[])Stack.Pop())[(int)Stack.Pop()]);

        /// <summary>
        /// Clears stack
        /// </summary>
        /// <param name="args"></param>
        public static void Clear(object args) => Stack.Clear();

        /// <summary>
        /// Loads from local heap onto the stack
        /// </summary>
        /// <param name="args">Index of local heap to load</param>
        public static void LoadFromStorage(object args) => Stack.Push(Storage[Convert.ToByte(args)]);

        /// <summary>
        /// Adds last element with second last and pushes it onto the stack
        /// </summary>
        /// <param name="args"></param>
        public static void Add(object args)
        {
            object p = Stack.Pop();
            object p1 = Stack.Pop();
            if (p.GetType() != p1.GetType())
                throw new NotSupportedException("Cannot perform operation `Add` between types `" + p1.GetType() + "` & `"+p.GetType()+"`");
            if (p is string) Stack.Push((string)p1 + (string)p);
            else if (p is byte) Stack.Push((byte)p1 + (byte)p);
            else if (p is short) Stack.Push((short)p1 + (short)p);
            else if (p is int) Stack.Push((int)p1 + (int)p);
            else if (p is long) Stack.Push((long)p1 + (long)p);
            else if (p is float) Stack.Push((float)p1 + (float)p);
            else if (p is double) Stack.Push((double)p1 + (double)p);
            else if (p is decimal) Stack.Push((decimal)p1 + (decimal)p);
            else throw new NotSupportedException("Cannot perform operation `Add` with type `" + p1.GetType() + "`");
        }

        /// <summary>
        /// Subs last element with second last and pushes it onto the stack
        /// </summary>
        /// <param name="args"></param>
        public static void Sub(object args)
        {
            object p = Stack.Pop();
            object p1 = Stack.Pop();
            if (p.GetType() != p1.GetType())
                throw new NotSupportedException("Cannot perform operation `Sub` between types `" + p1.GetType() + "` & `" + p.GetType() + "`");
            if (p is byte) Stack.Push((byte)p1 - (byte)p);
            else if (p is short) Stack.Push((short)p1 + (short)p);
            else if (p is int) Stack.Push((int)p1 - (int)p);
            else if (p is long) Stack.Push((long)p1 - (long)p);
            else if (p is float) Stack.Push((float)p1 - (float)p);
            else if (p is double) Stack.Push((double)p1 - (double)p);
            else if (p is decimal) Stack.Push((decimal)p1 - (decimal)p);
            else throw new NotSupportedException("Cannot perform operation `Sub` with type `" + p1.GetType() + "`");
        }

        /// <summary>
        /// Divides last element with second last and pushes it onto the stack
        /// </summary>
        /// <param name="args"></param>
        public static void Div(object args)
        {
            object p = Stack.Pop();
            object p1 = Stack.Pop();
            if (p.GetType() != p1.GetType())
                throw new NotSupportedException("Cannot perform operation `Div` between types `" + p1.GetType() + "` & `" + p.GetType() + "`");
            if (p is byte) Stack.Push((byte)p1 / (byte)p);
            else if (p is short) Stack.Push((short)p1 / (short)p);
            else if (p is int) Stack.Push((int)p1 / (int)p);
            else if (p is long) Stack.Push((long)p1 / (long)p);
            else if (p is float) Stack.Push((float)p1 / (float)p);
            else if (p is double) Stack.Push((double)p1 / (double)p);
            else if (p is decimal) Stack.Push((decimal)p1 / (decimal)p);
            else throw new NotSupportedException("Cannot perform operation `Div` with type `" + p1.GetType() + "`");
        }

        /// <summary>
        /// Multiplies last element with second last and pushes it onto the stack
        /// </summary>
        /// <param name="args"></param>
        public static void Mul(object args)
        {
            object p = Stack.Pop();
            object p1 = Stack.Pop();
            if (p.GetType() != p1.GetType())
                throw new NotSupportedException("Cannot perform operation `Mul` between types `" + p1.GetType() + "` & `" + p.GetType() + "`");
            if (p is byte) Stack.Push((byte)p1 * (byte)p);
            else if (p is short) Stack.Push((short)p1 * (short)p);
            else if (p is int) Stack.Push((int)p1 * (int)p);
            else if (p is long) Stack.Push((long)p1 * (long)p);
            else if (p is float) Stack.Push((float)p1 * (float)p);
            else if (p is double) Stack.Push((double)p1 * (double)p);
            else if (p is decimal) Stack.Push((decimal)p1 * (decimal)p);
            else throw new NotSupportedException("Cannot perform operation `Mul` with type `" + p1.GetType() + "`");
        }

        /// <summary>
        /// Prints the last element onto the stack into the console
        /// </summary>
        /// <param name="args"></param>
        public static void RvmOutput(object args) => Console.Write(Stack.Pop());

        /// <summary>
        /// Asks for input and returns onto the stack
        /// </summary>
        /// <param name="args"></param>
        public static void RvmInput(object args) => Stack.Push(Console.ReadLine());

        /// <summary>
        /// Calls shell using the last element onto the stack
        /// </summary>
        /// <param name="args"></param>
        public static void RvmShell(object args) => Process.Start(new ProcessStartInfo() { FileName = "cmd", Arguments = "/C " + Stack.Pop(), UseShellExecute = false });

        /// <summary>
        /// Calls a built in rvm function reference stored onto the stack
        /// </summary>
        /// <param name="args"></param>
        public static void RvmCall(object args) => ((CallPointer)Stack.Pop())();

        /// <summary>
        /// Pops the last element of the stack
        /// </summary>
        /// <param name="args"></param>
        public static void Unload(object args) => Stack.PopNR();

        /// <summary>
        /// Dupplicates the last element of the stack
        /// </summary>
        /// <param name="args"></param>
        public static void Duplicate(object args) => Stack.Push(Stack.Peek());

        /// <summary>
        /// Breaks function executing returning to previous
        /// </summary>
        /// <param name="args"></param>
        public static void Return(object args) => ProgramCounter = int.MaxValue-1;

        /// <summary>
        /// Compares last two elements onto the stack and pushes true if are equals or false
        /// </summary>
        /// <param name="args"></param>
        public static void CompareEQ(object args) => Stack.Push(Stack.Pop().Equals(Stack.Pop()));

        /// <summary>
        /// Compares last two elements onto the stack and pushes true if last is greater than second last or false
        /// </summary>
        /// <param name="args"></param>
        public static void CompareGreater(object args)
        {
            object p = Stack.Pop();
            object p1 = Stack.Pop();
            if (p.GetType() != p1.GetType())
                throw new NotSupportedException("Cannot perform operation `CompareGreater` between types `" + p1.GetType() + "` & `" + p.GetType() + "`");
            if (p is byte) Stack.Push((byte)p1 > (byte)p);
            else if (p is short) Stack.Push((short)p1 > (short)p);
            else if (p is int) Stack.Push((int)p1 > (int)p);
            else if (p is long) Stack.Push((long)p1 > (long)p);
            else if (p is float) Stack.Push((float)p1 > (float)p);
            else if (p is double) Stack.Push((double)p1 > (double)p);
            else if (p is decimal) Stack.Push((decimal)p1 > (decimal)p);
            else throw new NotSupportedException("Cannot perform operation `CompareGreater` with type `" + p1.GetType() + "`");
        }

        /// <summary>
        /// Compares last two elements onto the stack and pushes true if last is less than second last or false
        /// </summary>
        /// <param name="args"></param>
        public static void CompareLess(object args)
        {
            object p = Stack.Pop();
            object p1 = Stack.Pop();
            if (p.GetType() != p1.GetType())
                throw new NotSupportedException("Cannot perform operation `CompareLess` between types `" + p1.GetType() + "` & `" + p.GetType() + "`");
            if (p is byte) Stack.Push((byte)p1 < (byte)p);
            else if (p is short) Stack.Push((short)p1 < (short)p);
            else if (p is int) Stack.Push((int)p1 < (int)p);
            else if (p is long) Stack.Push((long)p1 < (long)p);
            else if (p is float) Stack.Push((float)p1 < (float)p);
            else if (p is double) Stack.Push((double)p1 < (double)p);
            else if (p is decimal) Stack.Push((decimal)p1 < (decimal)p);
            else throw new NotSupportedException("Cannot perform operation `CompareLess` with type `" + p1.GetType() + "`");
        }

        /// <summary>
        /// Compares last two elements onto the stack and pushes true if are not equals or false
        /// </summary>
        /// <param name="args"></param>
        public static void CompareNEQ(object args) => Stack.Push(!Stack.Pop().Equals(Stack.Pop()));

        /// <summary>
        /// Exits from the program
        /// </summary>
        /// <param name="args">ExitCode</param>
        public static void Exit(object args) => Environment.Exit((int)args);

        /// <summary>
        /// Pops last element of the stack and jump to <paramref name="args"/> if true
        /// </summary>
        /// <param name="args">Index of instruction to jump on</param>
        public static void JumpTrue(object args)
        {
            if ((bool)Stack.Pop())
                ProgramCounter = (int)args - 1;
        }

        /// <summary>
        /// Pops last element of the stack and jump to <paramref name="args"/> if false
        /// </summary>
        /// <param name="args">Index of instruction to jump on</param>
        public static void JumpFalse(object args)
        {
            if (!(bool)Stack.Pop())
                ProgramCounter = (int)args - 1;
        }

        /// <summary>
        /// Pops last element of the stack and jump to <paramref name="args"/> if false
        /// </summary>
        /// <param name="args">Index of instruction to jump on</param>
        public static void SkipTrue(object args)
        {
            if ((bool)Stack.Pop())
                ProgramCounter += (int)args;
        }

        /// <summary>
        /// Pops last element of the stack and jump to <paramref name="args"/> if false
        /// </summary>
        /// <param name="args">Index of instruction to jump on</param>
        public static void SkipFalse(object args)
        {
            if (!(bool)Stack.Pop())
                ProgramCounter += (int)args;
        }

        /// <summary>
        /// Pops last element of the stack and jump to <paramref name="args"/> if false
        /// </summary>
        /// <param name="args">Index of instruction to jump on</param>
        public static void BackTrue(object args)
        {
            if ((bool)Stack.Pop())
                ProgramCounter -= (int)args;
        }

        /// <summary>
        /// Pops last element of the stack and jump to <paramref name="args"/> if false
        /// </summary>
        /// <param name="args">Index of instruction to jump on</param>
        public static void BackFalse(object args)
        {
            if (!(bool)Stack.Pop())
                ProgramCounter -= (int)args;
        }

        /// <summary>
        /// Jumps to <paramref name="args"/>
        /// </summary>
        /// <param name="args">Number of instruction to jump on</param>
        public static void Jump(object args) => ProgramCounter = (int)args - 1;
    }
}