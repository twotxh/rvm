# ***Let's start***
To start, download the files from nuget or release.
The package provides you dll with previously quoted classes you can use to run bytecode.
The dll does not provide you a bytecode builder. You have to make a class to build your bytecode.
Apart from this you can run your bytecode by calling `void Rvm.Execute(Group[] groups);` that'll load all code into `Runtime` and performs the first label in `Group[] groups`.
