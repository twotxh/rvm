<p align="center">
  <img src="extra/robin.ico" />
  <br>
  <h6>🟦 RVM is a bytecode simple virtual machine -> provides you tools to make and perform bytecode instructions!<br></h6>
  <h6>🟦 RVM is a pointer based vm -> it uses c# delegates (c++ pointers) to performs instructions!<br></h6>
  <h6>🟦 RVM is a fast program -> don't worry, c# doesn't make execution, slow!<br></h6>
  <h6>🟦 RVM is a experimental and simple product -> it doesn't use switch statement, increasing instructions performing!<br></h6>
  <br>
  <h6>🟩 That's how bytecode is managed by vm:</h6>
  <p>Models</p>
> - `Group` is a class contains all loaded into vm instructions in a `Instruction` array(Stored in `Models/Label.cs`)
> - `Instruction` is a class contains the delegate to perform and some arguments (Stored in `Models/Instructions.cs`)
- ___Virtual Components___
> - `Storage` is a class, works as a virtual component ex(Virtual Heap) and be initialized in `Runtime` static class at the start of the program with `1000` spaces (Stored in `VirtualComponents/Storage.cs`)
- ___Runtime___
> - `Runtime` is a static class contains all runtime executable instructions, storage initialization and instruction index counter (Stored in `Runtime/Runtime.cs`)
> - `Rvm` is a static class contains main methods to execute labels or main label (at index 0) (Stored in `Runtime/Rvm.cs`)
  <br>
  <h6>🟩 That's how bytecode instructions are performed by vm:</h6>
  <p>`Rvm` contains `ExecuteLabel`, takes `Group` as parameter and with a `for` indexes all instructions contained in an array performing it one by one taking from `Group label.Instructions[InstructionIndex]` delegate to perform and passing to it at the same class location the arguments -> `label.Instructions[InstructionIndex].Instruction(label.Instructions[InstructionIndex].Arguments)`</p>
  <p>`Rvm.ExecuteLabel` besides performs instructions contained in the label passed as parameter, pushes the old storage onto the stack, and instances a new `Storage` into the old to reset it and change its adress to avoid ambiguos behaviours, and restores it once finished reassigning the old value to the new storage variable</p>
  <br>
  <h6>🟩 That's a example:</h6>
  <img src="extra/text.png" />
</p>