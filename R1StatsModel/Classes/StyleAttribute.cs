using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace R1StatsModel.Classes
{
    [AttributeUsage(AttributeTargets.All)]
    public class StyleSheetAttribute : Attribute
    {
        // Private fields.
        private Dictionary<string, string> _styles = new Dictionary<string, string>();
        // This constructor defines two required parameters: name and level.

        public StyleSheetAttribute(string styles)
        {
            var s = styles.Split(';', StringSplitOptions.RemoveEmptyEntries);
            foreach (var sty in s)
            {
                var p = sty.Split(": ", StringSplitOptions.RemoveEmptyEntries);
                _styles.Add(p[0], p[1]);
            }
        }

        public virtual string Style(string key) => 
            $"{key}: {_styles[key]}";
    }
}
