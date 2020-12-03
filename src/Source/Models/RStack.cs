using System;
using System.Collections.Generic;

namespace RobinVM.Models
{
    public struct RStack
    {
        public RStack(int maxStack)
        {
            VirtualStack = new List<object>(maxStack);
        }
        List<object> VirtualStack;
        public void TransferToArguments(ref Function function)
        {
            function.Arguments = new object[VirtualStack.Count];
            while (VirtualStack.Count > 0)
                function.Arguments[VirtualStack.Count-1] = Pop();
        }
        public object Pop()
        {
            var x0 = VirtualStack[^1];
            VirtualStack.RemoveAt(VirtualStack.Count - 1);
            return x0;
        }
        public T Pop<T>()
        {
            var x0 = VirtualStack[^1];
            VirtualStack.RemoveAt(VirtualStack.Count - 1);
            return (T)x0;
        }
        public object Peek()
        {
            return VirtualStack[^1];
        }
        public void Push(object value)
        {
            VirtualStack.Add(value);
        }
        public void PopNR()
        {
            VirtualStack.RemoveAt(VirtualStack.Count - 1);
        }
        public void Clear()
        {
            VirtualStack.Clear();
        }
        public void DrawStack(object nill = null)
        {
            Console.WriteLine("Stack Count: {0}/{1}\nStack Draw:", VirtualStack.Count, VirtualStack.Capacity);
            if (VirtualStack.Count == 0)
                Console.WriteLine("   Empty Stack");
            for (int i = VirtualStack.Count-1; i >= 0; i--)
                Console.WriteLine("   {0} | Object[{2}]: `{1}`", i, VirtualStack[i], VirtualStack[i].GetType().ToString());
        }
    }
}
