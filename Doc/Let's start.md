# ***Let's start***
To start, download the files from nuget or release.
The package provides you a dll with previously quoted classes you can use to run and instantiate bytecode.
The dll does not provide you a bytecode builder. You have to make a class to build your bytecode.
Apart from this you can run your bytecode by calling `void Rvm.Execute(Group[] groups);` that'll load all code into `Runtime` and performs the first label in `Group[] groups`.<br>
Rvm is stack and pointer based -> stack based because use virtual stack to pass arguments and more while pointer based because does not use a switch statement to performs instructions but uses a array of runtime fucntions.<br>
Loading a constant, a array or more you are saying to the vm to load args onto the virtual stack. To understand this, you have to understand how stack works. Think to the stack as a list of items: when you load some value it will be pushed on the top of the stack, so when you push an item you are sending a value onto the stack while when you pop an item you are trying to remove the last element onto the stack returning its value in a specified location. Here a reference: `https://www.cs.cmu.edu/~adamchik/15-121/lectures/Stacks%20and%20Queues/Stacks%20and%20Queues.html`
So here an example:
```
   void main {
      load 10 // load a constant onto the stack, that can be a string, a array, an int or more value types, so now the stack contains [10]
      store 0 // pop the last element onto the stack and store its value in the local heap at index:'0', so now the stack contains []
      loadfromstorage 0 // load a reference onto the stack from local heap at index:'0', so now the stack contains [10]
      load 10
      add // pop last two elements and sum them returning the value onto the stack, so now the stack contains [20]
      store 0
   }
```