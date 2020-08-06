# RobinScript
- Author: Carpal
- Name: RobinScript
- Date: 14/07/20 - 10:15
- Common Extention: .rsc <br>
***A fast Interpreted language, syntactically designed by me. Robin Script (Ext: .rsc ). This project will be written in C#.Net, in a VisualStudio solution (Ext: .sln), that can be downloaded and edited by anyone. The commits will not be accepted, but I will gladly watch them.***
_________________________________
# How To Use
- cmd: `rsc file.rsc`
> Output

- rshell: `load file`
> Output
________________________________
# About
- No Entry Level
- No forced OOP
- No Typed
- Old style
- Intelligent
- Modern
- OOP
________________________________
# Hello World
```
$print "Hello, World!"
$println "Hello, World!"
$printf "Author: {0}, Project: {1}, OpenSource: {2}" 'Carpal' 'RobinScript' true
$prints "spam " 5 // <text to spam> <integear: times to spam param0>
$printlns "." 100
```
____

```
var = "Hello," " " "World" "!"
$print var
```
____

```
fn add n n1
  return n+n1
end
$add 5 10
```
____

```
class Program
  fn main i
    x = $input
    $print x
  end
end
c = Program.main 5
```
____
```
x = 'Conditions'
if 'Hello' in 'Hello, World'
    $print 'Hello in Hello World'
elif 'Hello' is x
    $print 'Hello equals to x'
else
    x = x as num
end
```
# Icon: Logo
![RobinScript Logo](/Logos/RobinLogo.ico) <br>
***Program Icon***
________
# Icon: ScriptFile
![RobinScript Files Logo](/Logos/RobinScript.ico) <br>
***When a new .rbs script is created, that file will take this icon***
