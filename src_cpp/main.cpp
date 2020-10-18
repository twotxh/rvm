#include <vector>
#include "rvm.h"
int main() {
    Int x = 10;
    std::cout<<x.to_int();
//    const std::vector<instruction> main = {
//        instruction(rvm::runtime::call, 1),
//        instruction(rvm::runtime::ret),
//    };
//    const std::vector<instruction> printhelloro = {
//        instruction(rvm::runtime::load, Int()),
//        instruction(rvm::runtime::ret),
//    };
//    std::vector<group> program {
//        group(main),
//        group(printhelloro),
//    };
//    rvm::execute(program);
}