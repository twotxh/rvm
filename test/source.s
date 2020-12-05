.asm
	"test"
.end

.var "myglobal"
    "Result of [23, 2]: "
.end

.ctor
    load:img                ; push current module onto the stack
    load:var "myglobal"
    load:c 23
    load:c 2
    new:list                ; instantiate a new list
    call "add(.)"
    call "getstr(.)"
    call "cnctstr(..)"
    call "print(.)"         ; output: Result of [23, 2]: 25
    ret
.end

.fun "add(.)"
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
