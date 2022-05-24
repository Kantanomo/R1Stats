using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R1StatsModel.Classes;

namespace R1StatsModel.Enum
{
    public enum Team
    {
        [StyleSheet("background: #C13D3F80;")]
        Red,
        [StyleSheet("background: #3638C880;")]
        Blue,
        [StyleSheet("background: #C8BA3680;")]
        Yellow,
        [StyleSheet("background: #1F891F80;")]
        Green,
        [StyleSheet("background: #B53B8980;")]
        Purple,
        [StyleSheet("background: #DF973480;")]
        Orange,
        [StyleSheet("background: #74482180;")]
        Brown,
        [StyleSheet("background: #EB7EC480;")]
        Pink,
        [StyleSheet("background: #D1D1D180;")]
        None
    }
}
