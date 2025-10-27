using System;
using System.Reflection;

namespace Crux.Core.Editor.ExtensionMethods
{
    public static class TypeExtensionMethods
    {
        public static bool TryFindInheritedField(this Type type, string name, BindingFlags flags, out FieldInfo info)
        {
            Type currentType = type;

            while (currentType != null)
            {
                info = currentType.GetField(name, flags);

                if (info != null)
                    return true;

                currentType = currentType.BaseType;
            }

            info = default;
            return false;
        }

        public static bool TryFindInheritedProperty(this Type type, string name, BindingFlags flags,
            out PropertyInfo info)
        {
            Type currentType = type;

            while (currentType != null)
            {
                info = currentType.GetProperty(name, flags);

                if (info != null)
                    return true;

                currentType = currentType.BaseType;
            }

            info = default;
            return false;
        }
    }
}