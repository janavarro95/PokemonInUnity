using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Menus
{
    public class YesNoMenu:Menu
    {
        public enum YesNoSelect
        {
            None,
            Yes,
            No
        }

        public YesNoSelect currentSelection;

        public override void exitMenu()
        {
            base.exitMenu();
            Menu.ActiveMenu = null;
        }

        public override void Start()
        {
            
        }

        public override void Update()
        {
           
        }

        public override void setUpForSnapping()
        {
            
        }

        public override bool snapCompatible()
        {
            return false;
        }

        

    }
}
