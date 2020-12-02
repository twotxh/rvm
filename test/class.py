class person:
    name = None
    surname = None
    age = None
    title = None
    def __init__(self, name, surname, age, title):
        self.name = name
        self.surname = surname
        self.age = age
        self.title = title
    def about(self):
        print("Hi, my name is "+self.name+" "+self.surname+", i am "+self.age+" and so i am a "+self.title)

person("n", "s", 0, "baby").about()