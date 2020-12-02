.asm
	"classTest"
.end

.obj "person"
    .var "name" null .end
    .var "surname" null .end
    .var "age" null .end
    .var "title" null .end
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
    .fun "about(.)"
        load:c "Hi, my name is "
        load:arg 0
        load:var "name"
        call "cnctstr(..)"
        load:c " "
        load:arg 0
        load:var "surname"
        call "cnctstr(..)"
        load:c ", i am "
        load:arg 0
        load:var "age"
        call "cnctstr(..)"
        load:c " and so i am a "
        load:arg 0
        load:var "title"
        call "cnctstr(..)"

        call "cnctstr(..)"
        call "print(.)"
        ret
    .end
.end

.ctor ; main
    load:c "n"
    load:c "s"
    load:c 0
    load:c "baby"
    new:obj "person"
    call "about(.)"
    ret
.end
