using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Content.GameContent
{
    public class Move
    {
        public int currentPP;
        public MoveInfo moveInfo;

        public Move()
        {

        }

        public Move(MoveInfo Info)
        {
            this.moveInfo = Info;
            this.currentPP = Info.pp;
        }

        public Move clone()
        {
            return new Move(this.moveInfo);
        }

    }
}
