.asm
	"classTest"
.end

.obj "person"
    .var "name" str null .end
    .var "surname" str null .end
    .var "age" i32 null .end
    .var "title" str null .end
    .ctor
        load:arg 0
        load:arg 1
        store:var "name"
        load:arg 0
        load:arg 2
        store:var "surname"
        load:arg 0
        load:arg 3
        store:var "age"
        load:arg 0
        load:arg 4
        store:var "title"
        ret
    .end
    .fun "about(self)" void
        load:c "Hi, my name is "
        load:arg 0
        load:var "name"
        call "cnctstr(str, str) str"
        load:c " "
        load:arg 0
        load:var "surname"
        call "cnctstr(str, str) str"
        load:c ", i am "
        load:arg 0
        load:var "age"
        call "cnctstr(str, str) str"
        load:c " and so i am a "
        load:arg 0
        load:var "title"
        call "cnctstr(str, str) str"

        call "cnctstr(str, str) str"
        call "print(str) void"
        ret
    .end
.end

.fun "main" void
    load:c "n"
    load:c "s"
    load:c 0
    load:c "baby"
    new:obj "person"
    call "about(self)"
    ret
.end
