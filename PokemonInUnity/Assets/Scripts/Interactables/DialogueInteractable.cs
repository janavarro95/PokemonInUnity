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

        public UnityEvent onFinished;

        public override void Start()
        {
            //interact();
        }

        public override void interact()
        {
            GameInformation.GameManager.Manager.currentInteractable = this;
            GameInformation.GameManager.Manager.dialogueManager.initializeDialogues(this.speakerName, this.dialogueStrings, null);
            /*
            if (onFinished != null)
            {
                onFinished.Invoke();
            }
            */
        }

    }
}
