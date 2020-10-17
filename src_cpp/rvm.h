#include "group.h"
#include "runtime.h"
namespace rvm {
   group *Program[100];
   int Storage[100];

   void executelabel(group function) {
      int olds = *Storage;
      int oldi = InstructionIndex;
      *Storage = int(100);
      for (InstructionIndex = 0; InstructionIndex < function.Function.size(); InstructionIndex++) {
         function.Function[InstructionIndex].fpointer(function.Function[InstructionIndex].argument);
      }
      *Storage = olds;
      InstructionIndex = oldi;
   }
   void execute(group program[]) {
      *Program = program;
      for (int i=0; i<Functions.size(); i++)
         if (Functions[i] == "main")
            executelabel(program[i]);
   }
}