enum type {
    _string,
    _int,
    _float,
    _char,
    _runtime_object
};
struct RuntimeObject {
    virtual const char* to_string();
    virtual int to_int();
    virtual char to_char();
    virtual float to_float();
    virtual void set();
    virtual void sub(RuntimeObject obj);
    virtual void add(RuntimeObject obj);
    virtual void mul(RuntimeObject obj);
    virtual void div(RuntimeObject obj);
    virtual void sub(int val);
    virtual void add(int val);
    virtual void mul(int val);
    virtual void div(int val);
    virtual void sub(float val);
    virtual void add(float val);
    virtual void mul(float val);
    virtual void div(float val);
    virtual void add(const char* val);
    virtual void add(char val);
    virtual type type();
    virtual bool is_string();
    virtual bool is_int();
    virtual bool is_char();
    virtual bool is_float();
};