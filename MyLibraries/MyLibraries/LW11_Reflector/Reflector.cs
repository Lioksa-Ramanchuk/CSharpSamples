#pragma warning disable S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization;

namespace OOP.LW11_Reflector
{
    public static class Reflector
    {
        public static string FilePath { get; set; } = "classInfo.txt";

        public static string? GetAssemblyName(string className)
        {
            Type? t = Type.GetType(className);
            string? result = t is null ? "?" : Assembly.GetAssembly(t)?.GetName().Name ?? "?";
            using StreamWriter sw = new(FilePath, true);
            sw.WriteLine($"Имя сборки: {result}");
            return result;
        }

        public static bool? HasPublicConstructors(string className)
        {
            Type? t = Type.GetType(className);
            bool? result = t?.GetConstructors().Any();
            using StreamWriter sw = new(FilePath, true);
            sw.WriteLine($"Публичные конструкторы: {(result switch
            {
                true => "есть",
                false => "нет",
                _ => "?"
            })}");
            return result;
        }

        public static IEnumerable<MethodInfo>? GetPublicMethods(string className)
        {
            Type? t = Type.GetType(className);
            IEnumerable<MethodInfo>? result = t?.GetMethods();
            using StreamWriter sw = new(FilePath, true);
            sw.WriteLine($"Публичные методы: {string.Join(", ", result?.Select(m => m.Name) ?? new string[] { "?" })}");
            return result;
        }

        public static IEnumerable<MemberInfo>? GetAllFieldsAndProperties(string className)
        {
            Type? t = Type.GetType(className);
            IEnumerable<MemberInfo>? result = t?
                .GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(m => m.MemberType == MemberTypes.Field || m.MemberType == MemberTypes.Property);
            using StreamWriter sw = new(FilePath, true);
            sw.WriteLine($"Поля и свойства: {string.Join(", ", result?.Select(m => m.Name) ?? new string[] { "?" })}");
            return result;
        }

        public static IEnumerable<Type>? GetInterfaces(string className)
        {
            Type? t = Type.GetType(className);
            IEnumerable<Type>? result = t?.GetInterfaces();
            using StreamWriter sw = new(FilePath, true);
            sw.WriteLine($"Интерфейсы: {string.Join(", ", result?.Select(t => t.Name) ?? new string[] { "?" })}");
            return result;
        }

        public static void PrintMethodNamesWithParamType(string className, string paramTypeName)
        {
            Type? ct = Type.GetType(className);
            Type? pt = Type.GetType(paramTypeName);
            string resultStr = $"Методы, тип хотя бы одного из параметров которых - {paramTypeName}: {(string.Join(", ", ct?.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                                      .Where(m => m.GetParameters().Any(p => p.ParameterType == pt)).Select(m => m.Name) ??
                                   new string[] { "?" }))}";
            using StreamWriter sw = new(FilePath, true);
            sw.WriteLine(resultStr);
            Console.WriteLine(resultStr);
        }

        public static object? Invoke(object obj, string methodName)
        {
            var method = (obj is Type t ? t : obj.GetType())
                            .GetMethod(methodName,
                                       BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.OptionalParamBinding);
            return method?.Invoke(obj, method?.GetParameters().Select(p => Activator.CreateInstance(p.ParameterType)).ToArray());
        }

        public static object? Invoke(object obj, string methodName, object?[]? pars)
        {
            return (obj is Type t ? t : obj.GetType())
                        .GetMethod(methodName,
                                   BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.OptionalParamBinding,
                                   pars?.Select(p => p?.GetType() ?? typeof(object))?.ToArray() ?? Array.Empty<Type>())?
                        .Invoke(obj, pars);
        }

        public static object? Invoke(object obj, string methodName, string paramsFilePath)
        {
            using StreamReader sr = File.OpenText(paramsFilePath);
            XmlSerializer serializer = new(typeof(Parameters));
            Parameters? pars = serializer.Deserialize(sr) as Parameters;
            return Invoke(obj, methodName, pars?.Pars);
        }

        public static object? Create<T>(T t, params object?[] pars) where T : Type
        {
            return Activator.CreateInstance(t, BindingFlags.OptionalParamBinding, null, pars, CultureInfo.CurrentCulture);
        }
    }
}
