using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Infrastructure.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum element)
        {
            if (element != null)
            {
                FieldInfo field = element.GetType().GetField(element.ToString());
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                return attribute == null ? element.ToString() : attribute.Description;
            }
            return string.Empty;
        }
    }
}
