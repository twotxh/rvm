#include "instruction.h"
#include "functions.h"
struct group {
public:
   std::vector<instruction> Function;
   group(std::vector<instruction> function, const char* name) {
      Functions.push_back(name);
      Function = function;
   }
};