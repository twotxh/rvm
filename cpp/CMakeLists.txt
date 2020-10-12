# CMakeList.txt: progetto CMake per rvm. Includere l'origine e definire
# qui la logica specifica del progetto.
#
cmake_minimum_required (VERSION 3.8)

project ("rvm")

# Aggiungere l'origine all'eseguibile di questo progetto.
add_executable (rvm "rvm.cpp" "rvm.h")

# TODO: Aggiungere i test e, se necessario, installare le destinazioni.
