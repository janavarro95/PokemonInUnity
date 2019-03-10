using Assets.Scripts.GameInformation;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Delegates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Interactables
{
    public class DialogueInteractable : Interactable
    {
        [TextArea(3, 10)]
        public List<string> dialogueStrings;
        public string speakerName;

        public List<string> extraSentences;

        public UnityEvent onFinished;
        public UnityEvent beforeFinished;

        public List<DialogueEvent> events;

        public DialogueManager dialogueMenu;

        public override void Start()
        {
            //interact();
            dialogueStrings = StringUtilities.ParseAndSanitizeDialogueList(dialogueStrings);
        }

        public override void interact()
        {
            GameInformation.GameManager.Manager.currentInteractable = this;
            dialogueMenu=GameInformation.GameManager.Manager.dialogueManager.initializeDialogues(this.speakerName, this.dialogueStrings, onFinished,beforeFinished,this.events);          
        }




    }
}
