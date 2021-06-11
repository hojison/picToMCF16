using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace picToMCF
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class AliasNameAttribute : Attribute
    {
        public string AliasName { get; private set; }

        public AliasNameAttribute(string aliasName)
        {
            AliasName = aliasName;
        }
    }
    public static class Ext
    {
        // どうしてもメソッドチェインを崩したくない人用
        public static T ThrowIf<T>(this T value, Func<T, bool> predicate, Exception exception)
        {
            if (predicate(value)) throw exception;
            else return value;
        }

        public static string ToAliasName(this Enum value)
        {
            return value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(AliasNameAttribute), false)
                .Cast<AliasNameAttribute>()
                .FirstOrDefault()
                .ThrowIf(a => a == null, new ArgumentException("属性が設定されていません。"))
                .AliasName;
        }
    }
}
