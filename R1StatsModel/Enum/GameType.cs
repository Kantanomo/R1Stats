using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R1StatsModel.Enum
{
    public enum GameType
    {
        [Display(Name = "Slayer")]
        Slayer,
        [Display(Name = "Capture the Flag")]
        CTF,
        [Display(Name = "Assault")]
        Assault,
        [Display(Name = "Territories")]
        Territories,
        [Display(Name = "Oddball")]
        Oddball,
        [Display(Name = "King of the Hill")]
        KOTH,
        [Display(Name = "Juggernaut")]
        Juggernaut
    }
}
