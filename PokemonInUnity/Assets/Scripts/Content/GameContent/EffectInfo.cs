using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Content.GameContent
{
    public class EffectInfo
    {
        public string effect;
        public string shortEffect;

        public EffectInfo()
        {

        }

        public EffectInfo(string Effect, string ShortEffect)
        {
            this.effect = Effect;
            this.shortEffect = ShortEffect;
        }
    }
}
