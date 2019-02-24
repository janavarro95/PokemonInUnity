using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Content.GameContent
{
    public class StatChangeInfo
    {
        public string statName;
        public int amountToChange;

        public StatChangeInfo()
        {

        }

        public StatChangeInfo(string StatName,int Change)
        {
            this.statName = StatName;
            this.amountToChange = Change;
        }
    }
}
