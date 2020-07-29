# RobinScript
- Author: Carpal
- Name: RobinScript
- Date: 14/07/20 - 10:15
- Common Extention: .rsc <br>
***A fast Interpreted language, syntactically designed by me. Robin Script (Ext: .rsc ). This project will be written in C#.Net, in a VisualStudio solution (Ext: .sln), that can be downloaded and edited by anyone. The commits will not be accepted, but I will gladly watch them.***
_________________________________
# How To Use
- cmd: `robin file.rbs`
> Output

- rshell: `load file`
> Output
________________________________
# Hello World
```
$print("Hello, World!")
```
____

```
var = "Hello, World"+"!"
$print(%var%)
```
____

```

fn add(n, n1) {
  return %n%+%n1%;
}
$add(420, 69)
```
____

```
class Program {
  fn main {
    x = $input
    $print(x)
  }
}
c = $init(Program.main)
```
____
```
fn noParamFunc
{
    return false
} if $noParamFunc {
    $print("Parameterless functions don't need brackets '(', ')'")
}
```
# Icon: Logo
![RobinScript Logo](/Logos/RobinLogo.ico) <br>
***Program Icon***
________
# Icon: ScriptFile
![RobinScript Files Logo](/Logos/RobinScript.ico) <br>
***When a new .rbs script is created, that file will take this icon***
