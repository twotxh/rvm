#include <stack>
#include <iostream>
#include "instruction_index.h"
namespace runtime {
   std::stack<int> Stack;
   void load(int arg) {
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
   void rvm_output(int arg) {
      std::cout<<Stack.top();
      Stack.pop();
   }
   void rvm_outputc(int arg) {
      std::cout.put(Stack.top());
      Stack.pop();
   }
}