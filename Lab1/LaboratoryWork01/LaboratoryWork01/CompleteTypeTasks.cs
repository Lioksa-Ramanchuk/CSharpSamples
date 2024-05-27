#pragma warning disable CS0219 // Переменная назначена, но ее значение не используется
#pragma warning disable S125 // Sections of code should not be commented out
#pragma warning disable S1481 // Unused local variables should be removed
#pragma warning disable S2583 // Conditionally executed code should be reachable
#pragma warning disable S2589 // Boolean expressions should not be gratuitous

using System;

namespace OOP
{
    static partial class LaboratoryWork01
    {
        static void CompleteTypeTasks()
        {
            Console.WriteLine("\n===================================================");
            Console.WriteLine(" ТИПЫ");
            Console.WriteLine("===================================================\n");

            // Задание a
            Console.WriteLine("== Задание a ==\n");

            bool b = true;
            byte by = byte.MaxValue;
            sbyte sby = sbyte.MaxValue;
            char c = '#';
            decimal dec = decimal.MaxValue;
            double d = double.MaxValue;
            float f = float.MaxValue;
            int i = int.MaxValue;
            uint u = uint.MaxValue;
            long l = long.MaxValue;
            ulong ul = ulong.MaxValue;
            short s = short.MaxValue;
            ushort us = ushort.MaxValue;
            nint ni = nint.MaxValue;
            nuint nu = nuint.MaxValue;

            printVarValues();
            Console.WriteLine();

            EnterVar("bool", () => bool.TryParse(Console.ReadLine(), out b));
            EnterVar("byte", () => byte.TryParse(Console.ReadLine(), out by));
            EnterVar("sbyte", () => sbyte.TryParse(Console.ReadLine(), out sby));
            EnterVar("char", () => char.TryParse(Console.ReadLine(), out c));
            EnterVar("decimal", () => decimal.TryParse(Console.ReadLine(), out dec));
            EnterVar("double", () => double.TryParse(Console.ReadLine(), out d));
            EnterVar("float", () => float.TryParse(Console.ReadLine(), out f));
            EnterVar("int", () => int.TryParse(Console.ReadLine(), out i));
            EnterVar("uint", () => uint.TryParse(Console.ReadLine(), out u));
            EnterVar("long", () => long.TryParse(Console.ReadLine(), out l));
            EnterVar("ulong", () => ulong.TryParse(Console.ReadLine(), out ul));
            EnterVar("short", () => short.TryParse(Console.ReadLine(), out s));
            EnterVar("ushort", () => ushort.TryParse(Console.ReadLine(), out us));
            EnterVar("nint", () => nint.TryParse(Console.ReadLine(), out ni));
            EnterVar("nuint", () => nuint.TryParse(Console.ReadLine(), out nu));

            Console.WriteLine();
            printVarValues();

            void printVarValues()
            {
                Console.WriteLine($"bool b      = {b}");
                Console.WriteLine($"byte by     = {by}");
                Console.WriteLine($"sbyte sby   = {sby}");
                Console.WriteLine($"char c      = {c}");
                Console.WriteLine($"decimal dec = {dec}");
                Console.WriteLine($"double d    = {d}");
                Console.WriteLine($"float f     = {f}");
                Console.WriteLine($"int i       = {i}");
                Console.WriteLine($"uint u      = {u}");
                Console.WriteLine($"long l      = {l}");
                Console.WriteLine($"ulong ul    = {ul}");
                Console.WriteLine($"short s     = {s}");
                Console.WriteLine($"ushort us   = {us}");
                Console.WriteLine($"nint n      = {ni}");
                Console.WriteLine($"nuint nu    = {nu}");
            }

            static void EnterVar(string typeNmae, Func<bool> InputParsedSuccessfully)
            {
                Console.Write($"Введите значение переменной типа {typeNmae}: ");
                while (true)
                {
                    if (InputParsedSuccessfully())
                    {
                        break;
                    }

                    Console.Write("Некорректный ввод. Введите ещё раз: ");
                }
            }


            // Задание b
            Console.WriteLine("\n== Задание b ==\n");

            try
            {
                i = Convert.ToInt32(dec);
                Console.WriteLine($"i = Convert.ToInt32(dec) = {i}");
            }
            catch (OverflowException)
            {
                Console.WriteLine($"Ошибка при приведении числа dec ({dec}) к типу int (dec вне диапазона значений int).");
            }

            i = (int)d;
            Console.WriteLine($"i = (int)d = {i}");
            i = (int)f;
            Console.WriteLine($"i = (int)f = {i}");
            u = (uint)i;
            Console.WriteLine($"u = (uint)i = {i}");
            l = (long)ul;
            Console.WriteLine($"l = (long)ul = {l}");

            Console.Write($"\ni = {i}, by = {by}");
            i = by;
            Console.WriteLine($", i = by = {i}");

            Console.Write($"i = {i}, sby = {sby}");
            i = sby;
            Console.WriteLine($", i = sby = {i}");

            Console.Write($"d = {d}, f = {f}");
            d = f;
            Console.WriteLine($", d = f = {d}");

            Console.Write($"l = {l}, i = {i}");
            l = i;
            Console.WriteLine($", l = i = {l}");

            Console.Write($"ul = {ul}, u = {u}");
            ul = u;
            Console.WriteLine($", ul = u = {ul}");


            // Задание c
            Console.WriteLine("\n== Задание c ==\n");

            object obj;
            int i1 = 257, i2 = 20;

            obj = i1;
            Console.WriteLine($"i1 = {i1}, (int)(obj = i1) = {(int)obj}");
            Console.Write($"i2 = {i2}");
            i2 = (int)obj;
            Console.WriteLine($", i2 = (int)obj = {i2}");


            // Задание d
            Console.WriteLine("\n== Задание d ==\n");

            var v1 = d;
            Console.WriteLine($"{v1.GetType().Name} v = {v1}");


            // Задание e
            Console.WriteLine("\n== Задание e ==\n");

            int? nullInt = null;
            Console.WriteLine($"nullInt = {(nullInt is null ? "null" : nullInt)}");
            i = nullInt ?? 0;
            Console.WriteLine($"i = nullInt ?? 0 = {i}");
            nullInt = 5;
            Console.WriteLine($"nullInt = {nullInt}");
            i = nullInt ?? 0;
            Console.WriteLine($"i = nullInt ?? 0 = {i}");


            // Задание f

            var v2 = 345u;
            // v2 = -6;

            Console.ReadLine();
        }
    }
}