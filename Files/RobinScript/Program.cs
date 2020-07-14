
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinScript
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    class Interpreter
    {

    }
    class Storage
    {
        public class Program
        {

        }
        public enum TokensType
        {
            Function,
            Class,
            For,
            While,
            Loop,
            If,
            Elseif,
            Else,
            Variable,
            CallFunction,
            CallVariable,
            CallClass,
            Break,
            Continue,
            Return,
            Load,
            Use,
        }
        public enum StdFunction
        {
            ReadFile,
            WriteFile,
            AppendFile,
            CreateFile,
            MoveFile,
            CopyFile,
            CutFile,
            DeleteFile,
            CompressFile,
            DecompressFile,
            EncryptFile,
            DecryptFile,
            RenameFile,
            Print, // print
            Println, // print + \n
            Prints, // spam string: param -> ("string", 10) where int is the time to spam "string"
            Printf, // print a format string
            Input, // console input: param -> (var_into_store_input_value)
            Init, // initialize a class in a variable
            Cast, // cast a variable: param -> (var_to_cast, int) where int is the type in to cast 'var_to_cast'
        }
    }
}
