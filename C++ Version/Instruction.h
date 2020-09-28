#include <any>
#include "Runtime.h"

struct Instruction {
public:
    Instruction(void(*instruction) _instruction, std::any[] args) {
        instruction = _instruction(args);
    }
    static Instruction New(Runtime.compute instruction, dynamic arg) {
        new Instruction(instruction, new dynamic[]{ arg }); 
    }
    static Instruction New(Runtime.compute instruction, dynamic[] args) {
        new Instruction(instruction, args);
    }
    static Instruction New(Runtime.compute instruction) {
        new Instruction(instruction, null);
    }
    void (*instruction)(void(*compute)(std::any args););
};