using System.Collections.Generic;

class Storage {
    Stack<List<object>> stack = new Stack<List<object>>();
    List<object> storage = new List<object>();
    public void Push() { storage.Clear(); stack.Push(storage); }
    public void Pop() { storage = stack.Pop(); }
    public object this[short index] { get => storage[index]; set {
            if (storage.Count < index)
                storage[index] = value;
            else
                storage.Add(value);
        }
    }
}