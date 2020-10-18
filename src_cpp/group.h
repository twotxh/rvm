#include "instruction.h"
struct group {
public:
   std::vector<instruction> Function;
   group(std::vector<instruction> function) {
       Function = function;
   }
};
