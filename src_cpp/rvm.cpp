#include <stack>
#include <iostream>
#include "group.h"
namespace rvm {
    std::vector<group> Program;
    RuntimeObject Storage[100];
    int InstructionIndex;
    std::stack<RuntimeObject> Stack;

    void executelabel(std::vector<instruction> function) {
        //RuntimeObject olds = *Storage;
        //int oldi = InstructionIndex;
        //*Storage = 100;
        for (InstructionIndex = 0; InstructionIndex < function.size(); InstructionIndex++)
            function[InstructionIndex].fpointer(function[InstructionIndex].argument);
        //*Storage = olds;
        //InstructionIndex = oldi;
    }
    void execute(std::vector<group> program) {
        Program = program;
        executelabel(program[0].Function);
    }
    /// Runtime RobinVirtualMachine's Instruction Performing
    namespace runtime {
        void load(RuntimeObject arg) {
            Stack.push(arg);
        }
        void jump(RuntimeObject arg) {
            InstructionIndex = arg.sub(1).to_int();
        }
        void skip(RuntimeObject arg) {
            InstructionIndex+=arg.to_int();
        }
        void back(RuntimeObject arg) {
            InstructionIndex-=arg.to_int();
        }
        void jumpfalse(RuntimeObject arg) {
            if (!Stack.top()) {
                Stack.pop();
                InstructionIndex=arg.to_int()-1;
            }
        }
        void jumptrue(RuntimeObject arg) {
            if (Stack.top() == true) {
                Stack.pop();
                InstructionIndex=arg.to_int()-1;
            }
        }
        void skiptrue(RuntimeObject arg) {
            if (Stack.top() == true) {
                Stack.pop();
                InstructionIndex+=arg.to_int();
            }
        }
        void backtrue(RuntimeObject arg) {
            if (Stack.top() == true) {
                Stack.pop();
                InstructionIndex -= arg.to_int();
            }
        }
        void skipfalse(RuntimeObject arg) {
            if (!Stack.top()) {
                Stack.pop();
                InstructionIndex += arg.to_int();
            }
        }
        void backfalse(RuntimeObject arg) {
            if (!Stack.top()) {
                Stack.pop();
                InstructionIndex -= arg.to_int();
            }
        }
        void compare_e(RuntimeObject arg) {
            RuntimeObject l = Stack.top();
            Stack.pop();
            RuntimeObject r = Stack.top();
            Stack.pop();
            Stack.push(Bool(r.equals(l)));
        }
        void compare_ne(RuntimeObject arg) {
            RuntimeObject l = Stack.top();
            Stack.pop();
            RuntimeObject r = Stack.top();
            Stack.pop();
            Stack.push(Bool(r.notequals(l)));
        }
        void compare_gt(RuntimeObject arg) {
            RuntimeObject l = Stack.top();
            Stack.pop();
            RuntimeObject r = Stack.top();
            Stack.pop();
            Stack.push(Bool(r.greater(l)));
        }
        void compare_ls(RuntimeObject arg) {
            RuntimeObject l = Stack.top();
            Stack.pop();
            RuntimeObject r = Stack.top();
            Stack.pop();
            Stack.push(Bool(r.to_int()<l.to_int()));
        }
        void compare_ngt(RuntimeObject arg) {
            RuntimeObject l = Stack.top();
            Stack.pop();
            RuntimeObject r = Stack.top();
            Stack.pop();
            Stack.push(Bool(!(r.greater(l))));
        }
        void compare_nls(RuntimeObject arg) {
            RuntimeObject l = Stack.top();
            Stack.pop();
            RuntimeObject r = Stack.top();
            Stack.pop();
            Stack.push(Bool(!(r.less(l))));
        }
        void add(RuntimeObject arg) {
            RuntimeObject l = Stack.top();
            Stack.pop();
            RuntimeObject r = Stack.top();
            Stack.pop();
            Stack.push(r.add(l));
        }
        void sub(RuntimeObject arg) {
            RuntimeObject l = Stack.top();
            Stack.pop();
            RuntimeObject r = Stack.top();
            Stack.pop();
            Stack.push(r.sub(l));
        }
        void mul(RuntimeObject arg) {
            RuntimeObject l = Stack.top();
            Stack.pop();
            RuntimeObject r = Stack.top();
            Stack.pop();
            Stack.push(r.mul(l));
        }
        void div(RuntimeObject arg) {
            RuntimeObject l = Stack.top();
            Stack.pop();
            RuntimeObject r = Stack.top();
            Stack.pop();
            Stack.push(r.div(l));
        }
        void unload(RuntimeObject arg) {
            Stack.pop();
        }
        void call(RuntimeObject arg) {
            executelabel(Program[arg.to_int()].Function);
        }
        void ret(RuntimeObject arg) {
            InstructionIndex = 100000000;
        }
        void rvm_output(RuntimeObject arg) {
            std::cout<<Stack.top().to_string();
            Stack.pop();
        }
    }
}