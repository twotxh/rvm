#include <vector>
#include "rvm.h"
class Int : RuntimeObject {
    int value = 0;
    void add(RuntimeObject obj) override {
        value += obj.to_int();
    }
    void add(int val) override {
        value += val;
    }
    void sub(int val) override {
        value -= val;
    }
    void mul(int val) override {
        value *= val;
    }
    void div(int val) override {
        value /= val;
    }
    int to_int() override {
        return value;
    }
    enum type type() override {
        return type::_int;
    }
};
void put(RuntimeObject chr) {
    std::cout.put(chr.to_char());
}
int main() {

    const std::vector<instruction> main = {
        instruction(rvm::runtime::call, 1),
        instruction(rvm::runtime::ret),
    };
    const std::vector<instruction> printhelloro = {
        instruction(rvm::runtime::load, Int()),
        instruction(rvm::runtime::ret),
    };
    std::vector<group> program {
        group(main),
        group(printhelloro),
    };
    rvm::execute(program);
}