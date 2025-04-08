using System;
using System.Reflection;
using JetBrains.Annotations;

namespace Crux.Core.Editor.ExtensionMethods
{
    [PublicAPI]
    public static class MemberInfoExtensionMethods
    {
        public static bool TryGetAttribute<T>(this MemberInfo info, out T result, bool inherit = true) where T : Attribute
        {
            var attrs = info.GetCustomAttributes(typeof(T), inherit);

            if (attrs.Length > 0)
            {
                if (attrs[0] is T attr)
                {
                    result = attr;
                    return true;
                }
            }

            result = null;
            return false;
        }
    }
}