using System;
using System.Collections.Generic;
using System.Text;

namespace RobinVM.Models.BuiltIn
{
    public static class Extensions
    {
        public static void SysList_Find(object nill = null)
        {
            var arr = (List<object>)((Dictionary<string, object>)Runtime.CurrentFunctionPointer.FindArgument(0))["arr"];
            Runtime.Stack.Push(arr[(int)Runtime.CurrentFunctionPointer.FindArgument(1)]);
        }
        public static void SysList_Count(object nill = null)
        {
            Runtime.Stack.Push(((List<object>)((Dictionary<string, object>)Runtime.CurrentFunctionPointer.FindArgument(0))["arr"]).Count);
        }
        public static void SysList_Ctor(object nill = null)
        {
            Runtime.LoadFromArgs(0);
            Runtime.CurrentFunctionPointer.LoadArgumentsAsList();
            Runtime.StoreGlobal("arr");
        }
    }
}


namespace RobinVM.Models
{
    public partial struct Image
    {
        void InitializeBuiltIn()
        {
            CacheTable.Add("basepanic",
                new Obj
                {
                    Ctor = Function.New
                    (
                        new Instruction[]
                        {
                            Instruction.New(Runtime.LoadFromArgs, 0),
                            Instruction.New(Runtime.LoadFromArgs, 1),
                            Instruction.New(Runtime.StoreGlobal, "msg"),
                            Instruction.New(Runtime.LoadFromArgs, 0),
                            Instruction.New(Runtime.LoadFromArgs, 2),
                            Instruction.New(Runtime.StoreGlobal, "code"),
                            Instruction.New(Runtime.LoadFromArgs, 0),
                            Instruction.New(Runtime.LoadFromArgs, 3),
                            Instruction.New(Runtime.StoreGlobal, "type"),
                            Instruction.New(Runtime.Return)
                        }
                    ),
                    CacheTable = new Dictionary<string, object>()
                    {
                        { "$", "sys::basepanic" },
                        { "msg", null },
                        { "code", null },
                        { "type", "BasePanic" },
                        { "throw(.)",
                            Function.New
                            (
                                new Instruction[]
                                {
                                    Instruction.New(Runtime.LoadFromArgs, 0),
                                    Instruction.New(Runtime.RvmThrow),
                                    Instruction.New(Runtime.Return),
                                }
                            )
                        }
                    }
                });

            CacheTable.Add("list",
                new Obj
                {
                    Ctor = Function.New
                    (
                        new Instruction[]
                        {
                            Instruction.New(BuiltIn.Extensions.SysList_Ctor),
                            Instruction.New(Runtime.Return)
                        }
                    ),
                    CacheTable = new Dictionary<string, object>()
                    {
                        { "$", "sys::list" },
                        { "arr", null },
                        { "count(.)",
                            Function.New
                            (
                                new Instruction[]
                                {
                                    Instruction.New(BuiltIn.Extensions.SysList_Count),
                                    Instruction.New(Runtime.Return)
                                }
                            )
                        },
                        { "find(..)",
                            Function.New
                            (
                                new Instruction[]
                                {
                                    Instruction.New(BuiltIn.Extensions.SysList_Find),
                                    Instruction.New(Runtime.Return)
                                }
                            )
                        }
                    }
                });
        }
    }
}
