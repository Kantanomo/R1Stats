using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace R1StatsModel.Extensions
{
    public static class XMLExtension
    {
        public static string GetValue(this XElement node, string node_name) 
            => node.Descendants(node_name).First().Value;

        public static int GetValueI(this XElement node, string node_name)
        {
            int res = 0;
            var a = int.TryParse(GetValue(node, node_name), out res);
            if (a)
                return res;
            else
                return GetValueTime(node, node_name);
        }

        public static int GetValueTime(this XElement node, string node_name)
        {
            var time = GetValue(node, node_name).Split(':');
            if (string.IsNullOrWhiteSpace(time[0]))
                time[0] = "0";
            return int.Parse(time[0]) * 60 + int.Parse(time[1]);
        }
    }
}
