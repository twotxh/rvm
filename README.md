<p align="center">
  <img src="https://github.com/Carpall/rvm/tree/master/Extra/robin.ico" />
  <br>
  <h4>🟦 RVM is a bytecode simple virtual machine -> provides you tools to make and perform bytecode instructions!<br></h6>
  <h4>🟦 RVM is a pointer based vm -> it uses c# delegates (c++ pointers) to performs instructions!<br></h6>
  <h4>🟦 RVM is a fast program -> don't worry, c# doesn't make execution, slow!<br></h6>
  <h4>🟦 RVM is a experimental and simple product -> it doesn't use switch statement, increasing instructions performing!<br></h6>
  <br>
  <h4>🟩 That's how bytecode is managed by vm:</h6>
  <h5>Models</h5>
  <t><code>Group</code><p> is a class contains all loaded into vm instructions in a </p><code>Instruction</code><p> array(Stored in <p><code>Models/Label.cs</code>)
  <code>Instruction</code><p>is a class contains the delegate to perform and some arguments (Stored in </p><code>Models/Instructions.cs</code>)
  <h5>Virtual Components</h5>
  <code>Storage</code><p> is a class, works as a virtual component ex(Virtual Heap) and be initialized in </p><code>Runtime</code><p> static class at the start of the program with </p><code>1000</code><p> spaces (Stored in </p><code>VirtualComponents/Storage.cs</code>)
  </h5>Runtime</h5>
  <code>Runtime</code><p> is a static class contains all runtime executable instructions, storage initialization and instruction index counter (Stored in </p><code>Runtime/Runtime.cs</code>)
  <code>Rvm</code><p> is a static class contains main methods to execute labels or main label (at index 0) (Stored in </p><code>Runtime/Rvm.cs</code>)
  <br>
  <h6>🟩 That's how bytecode instructions are performed by vm:</h6>
  <code>Rvm</code> contains <code>ExecuteLabel</code>, takes <code>Group</code> as parameter and with a <code>for</code> indexes all instructions contained in an array performing it one by one taking from <code>Group label.Instructions[InstructionIndex]</code> delegate to perform and passing to it at the same class location the arguments -> <code>label.Instructions[InstructionIndex].Instruction(label.Instructions[InstructionIndex].Arguments)</code>
  <code>Rvm.ExecuteLabel</code> besides performs instructions contained in the label passed as parameter, pushes the old storage onto the stack, and instances a new <code>Storage</code> into the old to reset it and change its adress to avoid ambiguos behaviours, and restores it once finished reassigning the old value to the new storage variable
  <br>
  <h6>🟩 That's a example:</h6>
  <img src="https://github.com/Carpall/rvm/tree/master/Extra/test.png" />
</p>