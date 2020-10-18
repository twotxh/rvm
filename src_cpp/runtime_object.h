enum type {
    _string,
    _int,
    _bool,
    _float,
    _char,
    _runtime_object
};
struct RuntimeObject {
    virtual const char* to_string();
    virtual int to_int();
    virtual char to_char();
    virtual float to_float();
    virtual float to_bool();
    virtual RuntimeObject set();
    virtual RuntimeObject sub(RuntimeObject obj);
    virtual RuntimeObject add(RuntimeObject obj);
    virtual RuntimeObject mul(RuntimeObject obj);
    virtual RuntimeObject div(RuntimeObject obj);
    virtual RuntimeObject sub(int val);
    virtual RuntimeObject add(int val);
    virtual RuntimeObject mul(int val);
    virtual RuntimeObject div(int val);
    virtual RuntimeObject sub(float val);
    virtual RuntimeObject add(float val);
    virtual RuntimeObject mul(float val);
    virtual RuntimeObject div(float val);
    virtual RuntimeObject add(const char* val);
    virtual RuntimeObject add(char val);
    virtual type type();
    virtual bool is_string();
    virtual bool is_int();
    virtual bool is_char();
    virtual bool is_float();
    virtual bool is_bool();
    virtual bool equals(RuntimeObject obj);
    virtual bool greater(RuntimeObject obj);
    virtual bool less(RuntimeObject obj);
    virtual bool notequals(RuntimeObject obj);
    virtual bool operator !() {
        return !(this->is_bool() && this->to_bool());
    }
    virtual bool operator ==(bool expr) {
        return this->is_bool() && this->to_bool() == expr;
    }
};


class Bool : public RuntimeObject {
public:
    bool value;
    Bool(bool val) {
        value = val;
    }
};
class Int : public RuntimeObject {
public:
    int value = 0;
    RuntimeObject add(RuntimeObject obj) override {
        value += obj.to_int();
        return *this;
    }
    RuntimeObject add(int val) override {
        value += val;
        return *this;
    }
    RuntimeObject sub(int val) override {
        value -= val;
        return *this;
    }
    RuntimeObject mul(int val) override {
        value *= val;
        return *this;
    }
    RuntimeObject div(int val) override {
        value /= val;
        return *this;
    }
    int to_int() override {
        return value;
    }
    enum type type() override {
        return type::_int;
    }
    Int(int val) {
        value = val;
    }
    Int operator =(int val) {
        value = val;
        return Int(val);
    }
};