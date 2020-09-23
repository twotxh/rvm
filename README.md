# RobinVirtualMachine
___________
###### RVM is a simple and cross-platform bytecode vm. The bytecode style and logic working takes inspiration from python vm.<br>
###### RVM is a pointer based vm -> it uses c# delegates (c++ pointers) to performs instructions.<br>
###### This may allow to the vm to skip the compare and jump like with `switch` statement process and execute directly instructions, but how?<br>
###### Reading the source code, you can see that there are no `InstructionsKind` enums, in fact to perform instructions the vm uses delegates. These delegates are distributed in some classes to make order in executing bytecode.<br>
###### ***That's how bytecode is managed by vm:***
- `Group` is a class contains all loaded into vm instructions in a `Instruction` array(Stored in `Models/Label.cs`)
- `Instruction` is a class contains the delegate to perform and some arguments (Stored in `Models/Instructions.cs`)
- `Storage` is a class, works as a virtual component ex(Virtual Heap) and be initialized in `Runtime` static class at the start of the program with `10000` spaces (Stored in `VirtualComponents/Storage.cs`)
- `Registers` is a static class contains virtual registers into load and unload values like `lod`, general purpose, and `par`, parameters passing purpose (Stored in `VirtualComponents/Registers.cs`)
- `Runtime` is a static class contains all runtime executable instructions, storage initialization and instruction index counter (Stored in `Runtime/Runtime.cs`)
- `Rvm` is a static class contains main methods to execute labels or main label (at index 0) (Stored in `Runtime/Rvm.cs`)
###### ***That's how bytecode instructions are performed by vm:***
- `Rvm` contains `ExecuteLabel`, takes `Group` as parameter and with a `for` indexes all instructions contained in an array performing it one by one taking from `Group label.Instructions[InstructionIndex]` delegate to perform and passing to it at the same class location the arguments -> `label.Instructions[InstructionIndex].Instruction(label.Instructions[InstructionIndex].Arguments)`
- `Rvm.ExecuteLabel` besides performs instructions contained in the label passed as parameter, pushes the old storage onto the stack, and instances a new `Storage` into the old to reset it and change its adress to avoid ambiguos behaviours, and restores it once finished reassigning the old value to the new storage variable
