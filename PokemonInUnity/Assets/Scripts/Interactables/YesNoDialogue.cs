using Assets.Scripts.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
            if (yesNoMenu != null)
            {
                if (yesNoMenu.currentSelection == YesNoMenu.YesNoSelect.Yes)
                {
                    goodPrompt.interact();
                    this.yesNoMenu.exitMenu();
                    startedPrompt = false;
                }
                else if (yesNoMenu.currentSelection == YesNoMenu.YesNoSelect.No)
                {
                    badPrompt.interact();
                    this.yesNoMenu.exitMenu();
                    startedPrompt = false;
                }
            }
        }

        public override void interact()
        {
            if (startedPrompt==false)
            {
                if (prompt.beforeFinished == null)
                {
                    prompt.beforeFinished = new UnityEngine.Events.UnityEvent();
                }
                //prompt.beforeFinished.AddListener(new UnityEngine.Events.UnityAction(beforeFinishedOpenYesNoMenu));
                prompt.interact();
                startedPrompt = true;

            }
        }

        public void beforeFinishedOpenYesNoMenu()
        {
            Menu.Instantiate<YesNoMenu>();
            yesNoMenu = (YesNoMenu)Menu.ActiveMenu;
        }


    }
}
