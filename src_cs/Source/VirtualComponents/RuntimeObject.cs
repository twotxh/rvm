using System;
using System.Linq;
enum RuntimeType {
    _int,
    _float,
    _string,
    _char,
    _bool,
    _array,
    _runtime,
}
class RuntimeObject {
    public virtual/* abstract */ RuntimeType Type {get;}
    public virtual/* abstract */ void Set(RuntimeObject obj) {}
    public virtual void Set(char val) {}
    public virtual void Set(string val) {}
    public virtual void Set(float val) {}
    public virtual void Set(int val) {}
    public virtual void Set(bool val) {}
    public /* abstract */ override string ToString() {return null;}
    public virtual int? ToInt() {return null;}
    public virtual float? ToFloat() {return null;}
    public virtual char? ToChar() {return null;}
    public virtual bool? ToBool() {return null;}
    public virtual/* abstract */ object ToObject() {return null;}
    public virtual/* abstract */ void Add(RuntimeObject obj) {}
    public virtual/* abstract */ void Sub(RuntimeObject obj) {}
    public virtual/* abstract */ void Mul(RuntimeObject obj) {}
    public virtual/* abstract */ void Div(RuntimeObject obj) {}
    public virtual void Add(int val) {}
    public virtual void Sub(int val) {}
    public virtual void Mul(int val) {}
    public virtual void Div(int val) {}
    public virtual void Add(string val) {}
    public virtual void Add(float val) {}
    public virtual void Sub(float val) {}
    public virtual void Mul(float val) {}
    public virtual void Div(float val) {}
}
class RuntimeInt : RuntimeObject {
    int Value = 0;
    public override RuntimeType Type => RuntimeType._int;
    public RuntimeInt(int val) => Value = val;
    public RuntimeInt() {}
    public override void Add(RuntimeObject obj) {
        if (obj.Type == RuntimeType._int)
            Value += obj.ToInt().Value;
        else if (obj.Type == RuntimeType._float)
            Value += Convert.ToInt32(obj.ToFloat().Value);
    }
    public override void Sub(RuntimeObject obj) {
        if (obj.Type == RuntimeType._int)
            Value -= obj.ToInt().Value;
        else if (obj.Type == RuntimeType._float)
            Value -= Convert.ToInt32(obj.ToFloat().Value);
    }
    public override void Mul(RuntimeObject obj) {
        if (obj.Type == RuntimeType._int)
            Value *= obj.ToInt().Value;
        else if (obj.Type == RuntimeType._float)
            Value *= Convert.ToInt32(obj.ToFloat().Value);
    }
    public override void Div(RuntimeObject obj) {
        if (obj.Type == RuntimeType._int)
            Value /= obj.ToInt().Value;
        else if (obj.Type == RuntimeType._float)
            Value /= Convert.ToInt32(obj.ToFloat().Value);
    }
    public override void Add(int val) => Value += val;
    public override void Sub(int val) => Value -= val;
    public override void Mul(int val) => Value *= val;
    public override void Div(int val) => Value /= val;
    public override void Set(RuntimeObject obj) {
        if (obj.Type == RuntimeType._int)
            Value = obj.ToInt().Value;
        else if (obj.Type == RuntimeType._float)
            Value = Convert.ToInt32(obj.ToFloat().Value);
    }
    public override void Set(int val) => Value = val;
    public override object ToObject() => Value;
    public override int? ToInt() => Value;
    public override string ToString() => Value.ToString();
    public static implicit operator RuntimeInt(int val) => new RuntimeInt(val);
    public static implicit operator RuntimeInt(float val) => new RuntimeInt(Convert.ToInt32(val));
}
class RuntimeString : RuntimeObject {
    string Value = "";
    public override RuntimeType Type => RuntimeType._string;
    public RuntimeString(string val) => Value = val;
    public RuntimeString() {}
    public override void Add(RuntimeObject obj) => Value += obj.ToString();
    public override void Add(int val) => Value += val;
    public override void Mul(int val) {
        for (;val>=0;val--)
            Value += Value;
    }
    public override void Set(RuntimeObject obj) => Value = obj.ToString();
    public override void Set(string val) => Value = val;
    public void Set(object val) => Value = val.ToString();
    public override object ToObject() => Value;
    public override string ToString() => Value;
    public static implicit operator RuntimeString(string val) => new RuntimeString(val);
}
class RuntimeObjectArray : RuntimeObject {
    RuntimeObject[] Value = {};
    public override RuntimeType Type => RuntimeType._array;
    public RuntimeObjectArray(RuntimeObject[] arr) => Value = arr;
    public RuntimeObjectArray() {}
    public void Set(RuntimeObject[] obj) => Value = obj;
    public override object ToObject() => ToString();
    public override string ToString() {
        string a = "[";
        for (int i=0;i<Value.Length;i++) {
            if (Value[i].Type == RuntimeType._char || Value[i].Type == RuntimeType._string)
                a+="\""+Value[i]+(i<Value.Length-1 ? "\", " : "\"");
            else
                a+=Value[i]+(i<Value.Length-1 ? ", " : "");
        }
        return a+"]";
    }
    public static implicit operator RuntimeObjectArray(RuntimeObject[] arr) => new RuntimeObjectArray(arr);
}