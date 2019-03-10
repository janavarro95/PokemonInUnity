using Assets.Scripts.Menus.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

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

        public MenuComponent yesSnap;
        public MenuComponent noSnap;

        public override void Start()
        {
            this.canvas = this.gameObject.transform.Find("Canvas").gameObject;
            if (Menu.GetCursorFromParentMenu() == null)
            {
                this.menuCursor = canvas.transform.Find("GameCursor").gameObject.GetComponent<GameInput.GameCursor>();
            }
            else
            {
                this.menuCursor = Menu.GetCursorFromParentMenu();
                canvas.transform.Find("GameCursor").gameObject.SetActive(false);
            }
            setUpForSnapping();
        }

        public override void exitMenu()
        {
            base.exitMenu();
            Menu.ActiveMenu = null;
        }

        public override void Update()
        {
            checkForInput();
        }

        public void checkForInput()
        {
            if (this.menuCursor.simulateMousePress(yesSnap))
            {
                this.currentSelection = YesNoSelect.Yes;
            }
            else if (this.menuCursor.simulateMousePress(noSnap))
            {
                this.currentSelection = YesNoSelect.No;
            }
        }

        public override void setUpForSnapping()
        {
            yesSnap = new MenuComponent(canvas.gameObject.transform.Find("Yes").Find("SnapComponent").gameObject.GetComponent<Image>());
            noSnap = new MenuComponent(canvas.gameObject.transform.Find("No").Find("SnapComponent").gameObject.GetComponent<Image>());

            yesSnap.setNeighbors(null, null, null, noSnap);
            noSnap.setNeighbors(null, null, yesSnap, null);

            this.selectedComponent = yesSnap;
            this.selectedComponent.snapToThisComponent();
        }

        public override bool snapCompatible()
        {
            return true;
        }

        

    }
}
