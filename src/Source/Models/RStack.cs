﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace RobinVM.Models
{
    public struct RStack
    {
        public RStack(int maxStack)
        {
            VirtualStack = new List<object>(maxStack);
        }
        List<object> VirtualStack;
        public int CountStack()
        {
            return VirtualStack.Count;
        }
        public void TransferToArguments(ref Function function)
        {
            if (VirtualStack.Count != 0)
                function.PassArguments(VirtualStack.ToArray());
        }
        public object Pop()
        {
            if (VirtualStack.Count == 0)
                BasePanic.Throw("Tryed to pop element from empty stack, use Runtime.Stack.DrawStack function at runtime to debug the stack container", "Runtime");
            var x0 = VirtualStack[^1];
            VirtualStack.RemoveAt(VirtualStack.Count - 1);
            return x0;
        }
        public T Pop<T>()
        {
            if (VirtualStack.Count == 0)
                BasePanic.Throw("Tryed to pop element from empty stack, use Runtime.Stack.DrawStack function at runtime to debug the stack container", "Runtime");
            var x0 = VirtualStack[^1];
            VirtualStack.RemoveAt(VirtualStack.Count - 1);
            return (T)x0;
        }
        public object Peek()
        {
            if (VirtualStack.Count == 0)
                BasePanic.Throw("Tryed to peek the value of element on empty stack, use Runtime.Stack.DrawStack function at runtime to debug the stack container", "Runtime");
            return VirtualStack[^1];
        }
        public T Peek<T>()
        {
            if (VirtualStack.Count == 0)
                BasePanic.Throw("Tryed to peek the value of element on empty stack, use Runtime.Stack.DrawStack function at runtime to debug the stack container", "Runtime");
            return (T)VirtualStack[^1];
        }
        public void Push(object value)
        {
            if (VirtualStack.Count == 15)
                BasePanic.Throw("Tryed to push element onto full stack, use Runtime.Stack.DrawStack function at runtime to debug the stack container", "Runtime");
            VirtualStack.Add(value);
        }
        public void PopNR()
        {
            if (VirtualStack.Count == 0)
                BasePanic.Throw("Tryed to pop element from empty stack, use Runtime.Stack.DrawStack function at runtime to debug the stack container", "Runtime");
            VirtualStack.RemoveAt(VirtualStack.Count - 1);
        }
        public void Clear()
        {
            if (VirtualStack.Count != 0)
            VirtualStack.Clear();
        }
        public void DrawStack(object nill = null)
        {
            Console.WriteLine("Stack Count: {0}/{1}\nStack Draw:", VirtualStack.Count, VirtualStack.Capacity);
            if (VirtualStack.Count == 0)
                Console.WriteLine("   Empty Stack");
            for (int i = VirtualStack.Count - 1; i >= 0; i--)
                Console.WriteLine("   {0} | Object[{2}]: `{1}`", i, VirtualStack[i], VirtualStack[i].GetType().ToString());
        }
    }
}
