using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

class Bytecode {
    public delegate void compute(dynamic[] args);
    public Label[] Labels = { };
    //public static Bytecode ParseFromString(string s) {
    //    var label = new List<Label>();
    //    var instructions = new List<Instruction>();
    //    bool isLabel = false;
    //    string[] lines = s.Split(new char[] { '\n', '\r' });
    //    for (int i = 0; i < lines.Length; i++) {
    //        if (isLabel && lines[i].Replace(" ", "") != "}") {
    //            string t = lines[i].Trim();
    //            if (t.Contains(" ")) {
    //                instructions.Add(new Instruction(t.Remove(t.IndexOf(' ')), getArgs(t.Substring(t.IndexOf(' ')))));
    //                Console.WriteLine(".{0} .{1} .{2}", t.Remove(t.IndexOf(' ')), t.Substring(t.IndexOf(' ')), string.Join(" ",getArgs(t.Substring(t.IndexOf(' ')))));
    //            } else
    //                instructions.Add(new Instruction(t, new object[0]));
    //        } else if (lines[i].Replace(" ", "") == "}") {
    //            label.Add(new Label() { Instructions = instructions.ToArray() });
    //            instructions.Clear();
    //            isLabel = false;
    //        } else if (lines[i].Replace(" ", "") == "{")
    //            isLabel = true;
    //    }
    //    object[] getArgs(string h) {
    //        h = h.Replace("\\n", "\n").Replace("\\r", "\r");
    //        List<object> obj = new List<object>();
    //        bool isString = false;
    //        StringBuilder sb = new StringBuilder();
    //        for (int i = 0; i < h.Length; i++) {
    //            Console.WriteLine(sb.ToString());
    //            if (isString) {
    //                if (h[i] == '\"' && h[i - 1] != '\\')
    //                    isString = false;
    //                else if (h[i] == '\\')
    //                    continue;
    //                sb.Append(h[i]);
    //                continue;
    //            }
    //            if (h[i] == '\"') {
    //                sb.Append(h[i]);
    //                isString = true;
    //            } else if (h[i] == ',') {
    //                obj.Add(sb.ToString().Trim().Trim('\"'));
    //                sb.Clear();
    //            } else
    //                sb.Append(h[i]);
    //        }
    //        if (string.IsNullOrWhiteSpace(sb.ToString()))
    //            obj.Add(sb.ToString().Trim().Trim('\"'));
    //        return obj.ToArray();
    //    }
    //    return new Bytecode() { Labels = label.ToArray() };
    //}
    //public static Bytecode ParseFromString(string[] lines) {
    //    var label = new List<Label>();
    //    var instructions = new List<Instruction>();
    //    bool isLabel = false;
    //    for (int i = 0; i < lines.Length; i++) {
    //        if (isLabel && lines[i].Trim() != "}") {
    //            string t = lines[i].Trim();
    //            if (t.Contains(" ")) {
    //                Console.WriteLine("{0}, {1}, {2}", lines[i], t, getArgs(t[(t.IndexOf(' ') + 1)..t.Length]).Length);
    //                instructions.Add(new Instruction(t[0..t.IndexOf(' ')], getArgs(t[(t.IndexOf(' ') + 1)..^1])));
    //            } else
    //                instructions.Add(new Instruction(t, null));
    //        } else if (lines[i].Trim() == "}") {
    //            label.Add(new Label() { Instructions = instructions.ToArray() });
    //            instructions.Clear();
    //            isLabel = false;
    //        } else if (lines[i].Trim() == "{")
    //            isLabel = true;
    //    }
    //    object[] getArgs(string h) {
    //        h = h.Replace("\\n", "\n").Replace("\\r", "\r");
    //        List<object> obj = new List<object>();
    //        bool isString = false;
    //        StringBuilder sb = new StringBuilder();
    //        for (int i = 0; i < h.Length; i++) {
    //            if (isString) {
    //                if (h[i] == '\"' && h[i - 1] != '\\')
    //                    isString = false;
    //                else if (h[i] == '\\')
    //                    continue;
    //                sb.Append(h[i]);
    //                continue;
    //            }
    //            if (h[i] == '\"') {
    //                sb.Append(h[i]);
    //                isString = true;
    //            } else if (h[i] == ',') {
    //                obj.Add(sb.ToString().Trim().Trim('\"'));
    //                sb.Clear();
    //            } else
    //                sb.Append(h[i]);
    //        }
    //        if (string.IsNullOrWhiteSpace(sb.ToString()))
    //            obj.Add(sb.ToString().Trim().Trim('\"'));
    //        return obj.ToArray();
    //    }
    //    return new Bytecode() { Labels = label.ToArray() };
    //}
    public Group[] GenerateExecutions() {
        List<Group> labels = new List<Group>();
        List<compute> computes = new List<compute>();
        List<dynamic[]> args = new List<dynamic[]>();
        for (int i = 0; i < Labels.Length; i++) {
            for (int j = 0; j < Labels[i].Length; j++) {
                computes.Add(Isa.Get[Labels[i].Instructions[j].instruction.ToUpper()]);
                args.Add(Labels[i].Instructions[j].arguments);
            }
            labels.Add(new Group(computes.ToArray(), args.ToArray()));
            computes.Clear();
            args.Clear();
        }
        return labels.ToArray();
    }
}