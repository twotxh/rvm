using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace RobinVM
{
    public static class Runtime
    {
        public delegate void runtime(object args);
        public delegate void CallPointer();
        public static object[] storage = new object[256];
        public static int InstructionIndex = 0;
        public static Models.Function[] RuntimeFunctions;
        public static readonly Stack<object> Stack = new Stack<object>();
        public static readonly List<string> SwitchFunctions = new List<string>();

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
        /// Stores the value onto the stack in the local heap
        /// </summary>
        /// <param name="args">Index of the local heap into store last stack element</param>
        public static void Store(object args) => storage[(byte)args] = Stack.Pop();

        /// <summary>
        /// Calls a function
        /// </summary>
        /// <param name="args">Index of function to call</param>
        public static void Call(object args) => Robin.ExecuteLabel(RuntimeFunctions[SwitchFunctions.IndexOf((string)args)]);

        /// <summary>
        /// Loads onto the stack a constant
        /// </summary>
        /// <param name="args">Constant to load onto the stack</param>
        public static void Load(object args) => Stack.Push(args);

        /// <summary>
        /// Takes last element on the stack as array, the second last as index of the element to change and tird last as value to replace with,
        /// changes the value and push it onto the stack
        /// </summary>
        /// <param name="args"></param>
        /// <typeparam name="T">Type of array<br/>Example: int[] => StoreElementIntoArray&lt;int&gt;()</typeparam>
        public static void StoreElementIntoArray<T>(object args)
        {
            object arr = Stack.Pop();
            ((T[])arr)[(int)Stack.Pop()] = (T)Stack.Pop();
            Stack.Push(arr);
        }

        /// <summary>
        /// Takes last element on the stack as array, the second last as index of the element to get and
        /// pushes it onto the stack
        /// </summary>
        /// <param name="args"></param>
        /// <typeparam name="T">Type of array<br/>Example: int[] => StoreElementIntoArray&lt;int&gt;()</typeparam>
        public static void LoadElementFromArray<T>(object args) => Stack.Push(((T[])Stack.Pop())[(int)Stack.Pop()]);

        /// <summary>
        /// Clears stack
        /// </summary>
        /// <param name="args"></param>
        public static void Clear(object args) => Stack.Clear();

        /// <summary>
        /// Loads from local heap onto the stack
        /// </summary>
        /// <param name="args">Index of local heap to load</param>
        public static void LoadFromStorage(object args) => Stack.Push(storage[(byte)args]);

        /// <summary>
        /// Adds last element with second last and pushes it onto the stack
        /// </summary>
        /// <param name="args"></param>
        public static void Add(object args) { dynamic p = Stack.Pop(); Stack.Push(Stack.Pop()+p); }

        /// <summary>
        /// Subs last element with second last and pushes it onto the stack
        /// </summary>
        /// <param name="args"></param>
        public static void Sub(object args) { dynamic p = Stack.Pop(); Stack.Push(Stack.Pop() - p); }

        /// <summary>
        /// Divides last element with second last and pushes it onto the stack
        /// </summary>
        /// <param name="args"></param>
        public static void Div(object args) { dynamic p = Stack.Pop(); Stack.Push(Stack.Pop() / p); }

        /// <summary>
        /// Multiplies last element with second last and pushes it onto the stack
        /// </summary>
        /// <param name="args"></param>
        public static void Mul(object args) => Stack.Push((dynamic)Stack.Pop() * Stack.Pop());

        /// <summary>
        /// Does the power between last element and second last and pushes it onto the stack
        /// </summary>
        /// <param name="args"></param>
        public static void Pow(object args) { double p = (double)Stack.Pop(); Stack.Push(Math.Pow((double)Stack.Pop(), p)); }

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
        public static void Unload(object args) => Stack.Pop();

        /// <summary>
        /// Dupplicates the last element of the stack
        /// </summary>
        /// <param name="args"></param>
        public static void Duplicate(object args) => Stack.Push(Stack.Peek());

        /// <summary>
        /// Breaks function executing returning to previous
        /// </summary>
        /// <param name="args"></param>
        public static void Return(object args) => InstructionIndex = int.MaxValue-1;

        /// <summary>
        /// Compares last two elements onto the stack and pushes true if are equals or false
        /// </summary>
        /// <param name="args"></param>
        public static void CompareEQ(object args) => Stack.Push(Stack.Pop() == Stack.Pop());

        /// <summary>
        /// Compares last two elements onto the stack and pushes true if last is greater than second last or false
        /// </summary>
        /// <param name="args"></param>
        public static void CompareGreater(object args) => Stack.Push((dynamic)Stack.Pop() < Stack.Pop());

        /// <summary>
        /// Compares last two elements onto the stack and pushes true if last is less than second last or false
        /// </summary>
        /// <param name="args"></param>
        public static void CompareLess(object args) => Stack.Push((dynamic)Stack.Pop() > Stack.Pop());

        /// <summary>
        /// Compares last two elements onto the stack and pushes true if are not equals or false
        /// </summary>
        /// <param name="args"></param>
        public static void CompareNEQ(object args) => Stack.Push((dynamic)Stack.Pop() != Stack.Pop());

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
                InstructionIndex = (int)args - 1;
        }

        /// <summary>
        /// Pops last element of the stack and jump to <paramref name="args"/> if false
        /// </summary>
        /// <param name="args">Index of instruction to jump on</param>
        public static void JumpFalse(object args)
        {
            if (!(bool)Stack.Pop())
                InstructionIndex = (int)args - 1;
        }

        /// <summary>
        /// Pops last element of the stack and jump to <paramref name="args"/> if false
        /// </summary>
        /// <param name="args">Index of instruction to jump on</param>
        public static void SkipTrue(object args)
        {
            if ((bool)Stack.Pop())
                InstructionIndex += (int)args;
        }

        /// <summary>
        /// Pops last element of the stack and jump to <paramref name="args"/> if false
        /// </summary>
        /// <param name="args">Index of instruction to jump on</param>
        public static void SkipFalse(object args)
        {
            if (!(bool)Stack.Pop())
                InstructionIndex += (int)args;
        }

        /// <summary>
        /// Pops last element of the stack and jump to <paramref name="args"/> if false
        /// </summary>
        /// <param name="args">Index of instruction to jump on</param>
        public static void BackTrue(object args)
        {
            if ((bool)Stack.Pop())
                InstructionIndex -= (int)args;
        }

        /// <summary>
        /// Pops last element of the stack and jump to <paramref name="args"/> if false
        /// </summary>
        /// <param name="args">Index of instruction to jump on</param>
        public static void BackFalse(object args)
        {
            if (!(bool)Stack.Pop())
                InstructionIndex -= (int)args;
        }

        /// <summary>
        /// Jumps to <paramref name="args"/>
        /// </summary>
        /// <param name="args">Number of instruction to jump on</param>
        public static void Jump(object args) => InstructionIndex = (int)args - 1;
    }
}