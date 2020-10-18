#include "runtime_object.h"
struct instruction {
   void (*fpointer)(RuntimeObject);
   RuntimeObject argument;
   instruction(void (*function)(RuntimeObject), RuntimeObject arg) {
       fpointer = function;
       argument = arg;
   }
   instruction(void (*function)(RuntimeObject)) {
       fpointer = function;
   }
};