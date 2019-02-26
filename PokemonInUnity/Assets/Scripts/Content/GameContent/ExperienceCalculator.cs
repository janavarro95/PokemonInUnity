using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Content.GameContent
{
    public class ExperienceCalculator
    {

        public static int ExperienceToNextLevel(int NextLevel, Enums.ExperienceGrowthRate GrowthRate)
        {
            if (GrowthRate == Enums.ExperienceGrowthRate.Erratic || GrowthRate== Enums.ExperienceGrowthRate.SlowThenVeryFast)
            {
                if (NextLevel <= 50)
                {
                    return ((int)Math.Pow(NextLevel, 3) * (100 - NextLevel)) / 50;
                }
                if(NextLevel>=50 && NextLevel <= 68)
                {
                    return ((int)Math.Pow(NextLevel, 3) * (150 - NextLevel)) / 100;
                }
                if(NextLevel>=68 && NextLevel <= 98)
                {
                    return ((int)Math.Pow(NextLevel, 3) * ((1911 - NextLevel*10)/3)) / 500;
                }
                if(NextLevel>=98 && NextLevel <= 100)
                {
                    return ((int)Math.Pow(NextLevel, 3) * (160 - NextLevel)) / 100;
                }
            }
            else if(GrowthRate== Enums.ExperienceGrowthRate.Fast)
            {
                return (4 * (int)Math.Pow(NextLevel, 3)) / 5;
            }
            else if(GrowthRate== Enums.ExperienceGrowthRate.MediumFast)
            {
                return (int)Math.Pow(NextLevel, 3);
            }
            else if(GrowthRate == Enums.ExperienceGrowthRate.Medium)
            {
                return (int)Math.Pow(NextLevel, 3);
            }
            else if(GrowthRate== Enums.ExperienceGrowthRate.MediumSlow)
            {
                return (int)((6.0f/5.0f)* (int)Math.Pow(NextLevel, 3)- (15 * (int)Math.Pow(NextLevel,2))+ (100*NextLevel)-140);
            }
            else if(GrowthRate== Enums.ExperienceGrowthRate.Slow)
            {
                return (5 * (int)Math.Pow(NextLevel, 3)) / 4;
            }
            else if (GrowthRate== Enums.ExperienceGrowthRate.Fluctuating || GrowthRate== Enums.ExperienceGrowthRate.FastThenVerySlow)
            {
                if (NextLevel <= 15)
                {
                    return (int)Math.Pow(NextLevel, 3) * (( ((NextLevel+1)/3) +24)/50) ;
                }
                else if(NextLevel>=15 && NextLevel <= 36)
                {
                   return  (int)Math.Pow(NextLevel, 3) * (NextLevel+14)/50;
                }
                else if(NextLevel>=36 && NextLevel <= 100)
                {
                    return (int)Math.Pow(NextLevel, 3) * (((NextLevel) / 2 + 32) / 50);
                }
            }
            return 0;
        }
    }
}
