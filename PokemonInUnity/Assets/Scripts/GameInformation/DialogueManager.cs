using Assets.Scripts.Menus;
using Assets.Scripts.Utilities.Timers;
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
    public class DialogueManager: Menu
    {
        public string speakerName;
        public List<string> currentDialogues;
        public int currentDialogueIndex;
        public UnityEvent onDialogueFinished;


        public string currentSentence;

        public bool isDialogueUp;

        public GameObject dialogueBox;
        public Text dialogueText;

        public DeltaTimer typingDelayTimer;

        public double delayForNextCharacter=0.01f;

        bool eatFirstInput;

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

        public override void Start()
        {
            this.dialogueBox=this.transform.Find("DialogueBox").gameObject;
            this.dialogueText = this.dialogueBox.transform.Find("Canvas").Find("Image").Find("DialogueText").gameObject.GetComponent<Text>();
            this.dialogueBox.SetActive(false);
            DontDestroyOnLoad(this.dialogueBox);
            typingDelayTimer = new DeltaTimer(delayForNextCharacter, Enums.TimerType.CountDown, false, getNextChar);
            typingDelayTimer.start();
        }

        public void initializeDialogues(string speakerName, List<string> dialogues, UnityEvent onFinished = null)
        {
            Menu.ActiveMenu = this;
            this.speakerName = speakerName;
            this.currentDialogues = dialogues;
            this.currentDialogueIndex = 0;
            this.onDialogueFinished = onFinished;
            this.isDialogueUp = true;
            this.currentSentence = "";
            getNextChar();
            typingDelayTimer = new DeltaTimer(delayForNextCharacter, Enums.TimerType.CountDown, false, getNextChar);
            typingDelayTimer.start();
        }

        public override void Update()
        {
            if (this.isDialogueUp && Menu.ActiveMenu==this)
            {
                typingDelayTimer.Update();
                checkForInput();
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
                if (eatFirstInput == false)
                {
                    eatFirstInput = true;
                    GameManager.Manager.soundManager.playSound(GameManager.Manager.soundEffects.selectSound);
                    return;
                }
                if (currentSentence != targetSentence)
                {
                    currentSentence = targetSentence;
                    GameManager.Manager.soundManager.playSound(GameManager.Manager.soundEffects.selectSound);
                    return;
                }
                else if (currentSentence == targetSentence)
                {
                    currentSentence = "";
                    currentDialogueIndex++;
                    getNextChar();
                    typingDelayTimer = new DeltaTimer(delayForNextCharacter, Enums.TimerType.CountDown, false, getNextChar);
                    typingDelayTimer.start();

                    if (IsFinished == false)
                    {
                        GameManager.Manager.soundManager.playSound(GameManager.Manager.soundEffects.selectSound);
                    }
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
                GameInformation.GameManager.Manager.currentInteractable = null;
                return;
            }

        }

        private void getNextChar()
        {
            if (IsFinished) return;
            if (this.currentSentence.Length < this.targetSentence.Length)
            {
                this.currentSentence += this.targetSentence[this.currentSentence.Length];
                typingDelayTimer = new DeltaTimer(delayForNextCharacter, Enums.TimerType.CountDown, false, getNextChar);
                typingDelayTimer.start();
            }
        }

        public void clearDialogue()
        {
            //this.currentDialogues.Clear();
            this.currentDialogueIndex = 0;
            this.currentSentence = "";
            this.speakerName = "";
            this.onDialogueFinished = null;
            this.dialogueBox.SetActive(false);
            isDialogueUp = false;
            eatFirstInput = false;
            Menu.ActiveMenu = null;
        }

        public override void exitMenu()
        {
            clearDialogue();
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
