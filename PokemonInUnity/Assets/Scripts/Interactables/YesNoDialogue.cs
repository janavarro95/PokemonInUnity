using Assets.Scripts.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interactables
{
    public class YesNoDialogue : ChoiceDialogueInteractable
    {
        public YesNoMenu yesNoMenu;
        public bool startedPrompt;

        public override void Awake()
        {

        }

        public override void Start()
        {

        }

        public override void Update()
        {
            if(yesNoMenu.currentSelection== YesNoMenu.YesNoSelect.Yes)
            {
                goodPrompt.interact();
                this.yesNoMenu.exitMenu();
            }
            else if(yesNoMenu.currentSelection == YesNoMenu.YesNoSelect.Yes)
            {
                badPrompt.interact();
                this.yesNoMenu.exitMenu();
            }
        }

        public override void interact()
        {
            if (startedPrompt==false)
            {
                prompt.interact();
                startedPrompt = true;

                Menu.Instantiate<YesNoMenu>();
                yesNoMenu = (YesNoMenu)Menu.ActiveMenu;

            }


        }
    }
}
