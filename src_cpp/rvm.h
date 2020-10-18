#include <stack>
#include <iostream>
#include "group.h"
namespace rvm {
   std::vector<group> Program;
   int Storage[100];
   int InstructionIndex;
   std::stack<RuntimeObject> Stack;

    void executelabel(std::vector<instruction> function) {
        int olds = *Storage;
        int oldi = InstructionIndex;
        *Storage = int(100);
        for (InstructionIndex = 0; InstructionIndex < function.size(); InstructionIndex++)
            function[InstructionIndex].fpointer(function[InstructionIndex].argument);
        *Storage = olds;
        InstructionIndex = oldi;
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
        void jump(int arg) {
            InstructionIndex = arg-1;
        }
        void skip(int arg) {
            InstructionIndex+=arg;
        }
        void back(int arg) {
            InstructionIndex-=arg;
        }
        void jumpfalse(int arg) {
            if (!Stack.top()) {
                Stack.pop();
                InstructionIndex=arg-1;
            }
        }
        void jumptrue(int arg) {
            if (Stack.top()) {
                Stack.pop();
                InstructionIndex=arg-1;
            }
        }
        void skiptrue(int arg) {
            if (Stack.top()) {
                Stack.pop();
                InstructionIndex+=arg;
            }
        }
        void backtrue(int arg) {
            if (Stack.top()) {
                Stack.pop();
                InstructionIndex -= arg;
            }
        }
        void skipfalse(int arg) {
            if (!Stack.top()) {
                Stack.pop();
                InstructionIndex += arg;
            }
        }
        void backfalse(int arg) {
            if (!Stack.top()) {
                Stack.pop();
                InstructionIndex -= arg;
            }
        }
        void compare_e(int arg) {
            int l = Stack.top();
            Stack.pop();
            int r = Stack.top();
            Stack.pop();
            Stack.push(r==l);
        }
        void compare_ne(int arg) {
            int l = Stack.top();
            Stack.pop();
            int r = Stack.top();
            Stack.pop();
            Stack.push(r!=l);
        }
        void compare_gt(int arg) {
            int l = Stack.top();
            Stack.pop();
            int r = Stack.top();
            Stack.pop();
            Stack.push(r>l);
        }
        void compare_ls(int arg) {
            int l = Stack.top();
            Stack.pop();
            int r = Stack.top();
            Stack.pop();
            Stack.push(r<l);
        }
        void compare_ngt(int arg) {
            int l = Stack.top();
            Stack.pop();
            int r = Stack.top();
            Stack.pop();
            Stack.push(!(r>l));
        }
        void compare_nls(int arg) {
            int l = Stack.top();
            Stack.pop();
            int r = Stack.top();
            Stack.pop();
            Stack.push(!(r<l));
        }
        void add(int arg) {
            int l = Stack.top();
            Stack.pop();
            int r = Stack.top();
            Stack.pop();
            Stack.push(r+l);
        }
        void sub(int arg) {
            int l = Stack.top();
            Stack.pop();
            int r = Stack.top();
            Stack.pop();
            Stack.push(r-l);
        }
        void mul(int arg) {
            int l = Stack.top();
            Stack.pop();
            int r = Stack.top();
            Stack.pop();
            Stack.push(r*l);
        }
        void div(int arg) {
            int l = Stack.top();
            Stack.pop();
            int r = Stack.top();
            Stack.pop();
            Stack.push(r/l);
        }
        void pow(int arg) {
            int l = Stack.top();
            Stack.pop();
            int r = Stack.top();
            Stack.pop();
            Stack.push(r^l);
        }
        void unload(int arg) {
            Stack.pop();
        }
        void call(int arg) {
            executelabel(Program[arg].Function);
        }
        void ret(int arg) {
            InstructionIndex = 100000000;
        }
        void rvm_output(int arg) {
            std::cout<<Stack.top();
            Stack.pop();
        }
        void rvm_outputc(int arg) {
            std::cout.put(Stack.top());
            Stack.pop();
        }
    }
}