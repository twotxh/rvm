using System;
using System.Collections.Generic;

class Storage {
    dynamic[] storage = new dynamic[1000];
    public dynamic this[dynamic index] { get => storage[index]; set => storage[index] = value; }
}