﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DotNetExplorer
{
    public static class Extensions
    {
        public static string GetFriendlyName(this Type type)
        {
            return type.Name;
        }

        public static string GetTechnicalName(this Type x)
        {
            var b = new StringBuilder();

            if (x.IsPublic)
                b.Append("public ");

            if (x.IsSealed)
                b.Append("sealed ");

            if (x.IsInterface)
                b.Append("interface ");
            else if (x.IsAbstract)
                b.Append("abstract ");

            if (x.IsEnum)
                b.Append("enum ");

            if (x.IsClass)
                b.Append("class ");

            b.Append(x.GetFriendlyName());

            var baseTypes = new List<Type>();
            if (x.BaseType != null && x.BaseType != typeof(object)) baseTypes.Add(x.BaseType);
            baseTypes.AddRange(x.GetInterfaces());

            if (baseTypes.Count >= 1)
            {
                b.Append(" : ");
                for (int i = 0; i < baseTypes.Count; i++)
                {
                    b.Append(baseTypes[i].GetFriendlyName());
                    if (i < baseTypes.Count - 1)
                        b.Append(", ");
                }
            }

            return b.ToString();
        }

        public static string GetTechnicalName(this MethodInfo x)
        {
            var b = new StringBuilder();

            if (x.IsPublic)
                b.Append("public ");

            if (x.IsPrivate)
                b.Append("private ");

            if (x.IsStatic)
                b.Append("static ");

            b.Append((x.ReturnType == null ? "void" : x.ReturnType.GetFriendlyName()) + " ");
            b.Append(x.Name);
            b.Append("(");

            var args = x.GetParameters();
            for (int i = 0; i < args.Length; i++)
            {
                b.Append(args[i].ParameterType.GetFriendlyName() + " ");
                b.Append(args[i].Name);

                if (i < args.Length - 1)
                    b.Append(", "); 
            }

            b.Append(")");

            return b.ToString();
        }

        public static string GetTechnicalName(this FieldInfo x)
        {
            var b = new StringBuilder();

            if (x.IsPublic)
                b.Append("public ");

            if (x.IsPrivate)
                b.Append("private ");

            if (x.IsStatic)
                b.Append("static ");

            b.Append(x.FieldType.GetFriendlyName() + " ");
            b.Append(x.Name);

            return b.ToString();
        }

        public static string GetTechnicalName(this PropertyInfo x)
        {
            var b = new StringBuilder();
            
            b.Append(x.PropertyType.GetFriendlyName() + " ");
            b.Append(x.Name + " ");
            b.Append("{ ");
            b.Append("TODO accesors");
            b.Append(" }");
            return b.ToString();
        }
    }
}
