using Assets.Scripts.GameInformation;
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

        public DialogueInteractable prompt;
        public DialogueInteractable goodPrompt;
        public DialogueInteractable badPrompt;

        public DialogueInteractable completedPrompt;

        public YesNoMenu yesNoMenu;
        public bool startedPrompt;

        public bool canRepeatYesPrompt = true;
        public bool completedYesPrompt;

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
                    this.yesNoMenu.exitMenu();
                    goodPrompt.interact();
                    startedPrompt = false;

                    if (canRepeatYesPrompt == false)
                    {
                        completedYesPrompt = true;
                    }
                    else
                    {
                        completedYesPrompt = false;
                    }
                }
                else if (yesNoMenu.currentSelection == YesNoMenu.YesNoSelect.No)
                {
                    this.yesNoMenu.exitMenu();
                    badPrompt.interact();
                    startedPrompt = false;
                }
            }
        }

        public override void interact()
        {
            if (completedYesPrompt)
            {
                completedPrompt.interact();
                return;
            }
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

        public void sayPokemonNameInitialize()
        {
            Menus.Menu.Instantiate<Menus.PokemonPartyMenu>();
            (Menus.Menu.ActiveMenu as Menus.PokemonPartyMenu).onPokemonSelected.AddListener(sayPokemonNameCleanUp);
        }

        public void sayPokemonNameCleanUp()
        {
            foreach (string extra in this.goodPrompt.extraSentences)
            {
                GameManager.Manager.dialogueManager.currentDialogues.Add(Utilities.StringUtilities.ParseAndSanitizeDialogueString(extra, new object[] { (Menus.Menu.ParentMenu() as Menus.PokemonPartyMenu).selectedPokemon.Name }));
            }
            Menus.Menu.exitMenusUntilThisOne(goodPrompt.dialogueMenu);
            GameManager.Manager.dialogueManager.forceNextSentence();
        }

    }
}
