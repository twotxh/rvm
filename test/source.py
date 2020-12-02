myglobal = "Result of [23, 2]: "


def add(arr: list) -> int:
    res = 0
    for n in arr:
        res+=n
    return res

print(myglobal+add([23, 2]))