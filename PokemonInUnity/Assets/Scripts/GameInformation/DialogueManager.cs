﻿using Assets.Scripts.Interactables;
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
        public UnityEvent beforeDialogueFinished;

        public List<DialogueEvent> events;


        public string currentSentence;

        public bool isDialogueUp;

        public GameObject dialogueBox;
        public Text dialogueText;

        public DeltaTimer typingDelayTimer;

        public double delayForNextCharacter=0.01f;

        bool eatFirstInput;

        bool hasTriggeredBeforeFinishedAlready = false;

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

        public bool LastSentence
        {
            get
            {
                return currentDialogueIndex == currentDialogues.Count-1;
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

        public DialogueManager initializeDialogues(string speakerName, List<string> dialogues, UnityEvent onFinished = null, UnityEvent beforeFinished = null,List<DialogueEvent> Events=null)
        {
            Menu.ActiveMenu = this;
            this.speakerName = speakerName;
            this.currentDialogues = dialogues;
            this.currentDialogueIndex = 0;
            this.onDialogueFinished = onFinished;
            this.beforeDialogueFinished = beforeFinished;
            this.events = Events!=null ? Events: new List<DialogueEvent>();
            this.isDialogueUp = true;
            this.currentSentence = "";
            getNextChar();
            typingDelayTimer = new DeltaTimer(delayForNextCharacter, Enums.TimerType.CountDown, false, getNextChar);
            typingDelayTimer.start();
            return this;
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

                    foreach (DialogueEvent e in this.events)
                    {
                        if (e.index == this.currentDialogueIndex && e.hasTriggered == false)
                        {
                            e.invoke();
                            return;
                        }
                    }

                    if (LastSentence)
                    {
                        if (beforeDialogueFinished != null)
                        {
                            beforeDialogueFinished.Invoke();
                        }
                        currentDialogueIndex++;
                        return;
                    }
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
                Debug.Log("Dialogue should be finished...");
                //Close dialogue box;
                clearDialogue();
                if (onDialogueFinished != null)
                {
                    onDialogueFinished.Invoke();
                }
                clearEvents();
                GameInformation.GameManager.Manager.currentInteractable = null;
                return;
            }

        }


        public void forceNextSentence()
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
            this.dialogueBox.SetActive(false);
            isDialogueUp = false;
            eatFirstInput = false;
            Menu.ActiveMenu = null;
        }

        public void clearEvents()
        {
            this.onDialogueFinished = null;
            this.beforeDialogueFinished = null;
            this.events = new List<DialogueEvent>();
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
