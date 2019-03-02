using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.GameInformation
{
    public class DialogueManager: MonoBehaviour
    {
        public string speakerName;
        public List<string> currentDialogues;
        public int currentDialogueIndex;
        public UnityEvent onDialogueFinished;


        public string currentSentence;

        public bool isDialogueUp;

        public GameObject dialogueBox;
        public Text dialogueText;

        public string targetSentence
        {
            get
            {
                return currentDialogues[currentDialogueIndex];
            }
        }

        public bool IsFinished
        {
            get
            {
                return currentDialogueIndex >= currentDialogues.Count;
            }
        }

        public void Start()
        {
            this.dialogueBox=this.transform.Find("DialogueBox").gameObject;
            this.dialogueText = this.dialogueBox.transform.Find("Canvas").Find("Image").Find("DialogueText").gameObject.GetComponent<Text>();
            this.dialogueBox.SetActive(false);
            DontDestroyOnLoad(this.dialogueBox);
        }

        public void initializeDialogues(string speakerName, List<string> dialogues, UnityEvent onFinished = null)
        {
            this.speakerName = speakerName;
            this.currentDialogues = dialogues;
            this.currentDialogueIndex = 0;
            this.onDialogueFinished = onFinished;
            this.isDialogueUp = true;
            this.currentSentence = "";
        }

        public void Update()
        {
            if (this.isDialogueUp)
            {
                checkForInput();
                getNextChar();
                this.dialogueText.text = currentSentence;
            }
        }

        private void checkForInput() {

            if (this.isDialogueUp == true)
            {
                this.dialogueBox.SetActive(true);
            }
            else
            {
                this.dialogueBox.SetActive(false);
            }

            if (GameInput.InputControls.APressed)
            {
                if (currentSentence != targetSentence)
                {
                    currentSentence = targetSentence;
                    return;
                }
                else if (currentSentence == targetSentence)
                {
                    currentSentence = "";
                    currentDialogueIndex++;
                    return;
                }
            }
            if (IsFinished)
            {
                //Close dialogue box;
                clearDialogue();
                if (onDialogueFinished != null)
                {
                    onDialogueFinished.Invoke();
                }
                return;
            }

        }

        private void getNextChar()
        {
            if (IsFinished) return;
            if (this.currentSentence.Length < this.targetSentence.Length)
            {
                this.currentSentence += this.targetSentence[this.currentSentence.Length];
            }
        }

        public void clearDialogue()
        {
            this.currentDialogues.Clear();
            this.currentDialogueIndex = 0;
            this.currentSentence = "";
            this.speakerName = "";
            this.onDialogueFinished = null;
            this.dialogueBox.SetActive(false);
            isDialogueUp = false;
        }

    }
}
