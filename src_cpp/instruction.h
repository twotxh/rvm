struct instruction {
   void (*fpointer)(int);
   int argument = 0;
   instruction(void (*function)(int), int arg) {
       fpointer = function;
       argument = arg;
   }
   instruction(void (*function)(int)) {
       fpointer = function;
   }
};