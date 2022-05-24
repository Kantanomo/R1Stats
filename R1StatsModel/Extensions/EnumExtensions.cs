using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using R1StatsModel.Classes;

namespace R1StatsModel.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this System.Enum enumValue)
        {
            var a = enumValue.GetType().GetMember(enumValue.ToString()).First();
            return (a.GetCustomAttribute<DisplayAttribute>() != null ? a.GetCustomAttribute<DisplayAttribute>().GetName() : enumValue.ToString()) ?? string.Empty;
        }

        public static string GetStyle(this System.Enum enumValue, string key)
        {
            var a = enumValue.GetType().GetMember(enumValue.ToString()).First();
            return (a.GetCustomAttributes<StyleSheetAttribute>() != null
                ? a.GetCustomAttribute<StyleSheetAttribute>().Style(key)
                : "") ?? "";
        }
    }
}
