.asm
	"test"
.end

.var "myglobal" str
    "Result of [23, 2]: "
.end

.fun "main" void
    load:img                ; push current module onto the stack
    load:var "myglobal"
    load:c 23
    load:c 2
    new:list i32 2          ; instantiate a new list of type i32 of size 2 
    call "add([i32]) i32"
    call "getstr(i32) str"
    call "cnctstr(str, str) str"
    call "print(str) void"  ; output: Result of [23, 2]: 25
    ret
.end

.fun "add([i32])" i32
    load:c 0
    store:loc 0
    load:arg 0
                            ; end of for body
    for:iter "enditer"      ; push itered item
        load:loc 0
        add
        store:loc 0
    ."enditer"              ; label

    load:loc 0
    ret
.end
