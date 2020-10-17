#include "rvm.h"
int main() {
   const std::vector<instruction> main = {
      instruction(runtime::load, '!'),
      instruction(runtime::load, 'd'),
      instruction(runtime::load, 'l'),
      instruction(runtime::load, 'r'),
      instruction(runtime::load, 'o'),
      instruction(runtime::load, 'W'),
      instruction(runtime::load, ' '),
      instruction(runtime::load, 'o'),
      instruction(runtime::load, 'l'),
      instruction(runtime::load, 'l'),
      instruction(runtime::load, 'e'),
      instruction(runtime::load, 'H'),
      instruction(runtime::rvm_outputc),
      instruction(runtime::rvm_outputc),
      instruction(runtime::rvm_outputc),
      instruction(runtime::rvm_outputc),
      instruction(runtime::rvm_outputc),
      instruction(runtime::rvm_outputc),
      instruction(runtime::rvm_outputc),
      instruction(runtime::rvm_outputc),
      instruction(runtime::rvm_outputc),
      instruction(runtime::rvm_outputc),
      instruction(runtime::rvm_outputc),
      instruction(runtime::rvm_outputc),
   };
   group program[] {
      group(main, "main")
   };
   rvm::execute(program);
}