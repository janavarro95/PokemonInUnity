﻿using Assets.Scripts.GameInformation;
using Assets.Scripts.Interactables;
using Assets.Scripts.Menus;
using Assets.Scripts.Utilities;
using Assets.Scripts.Utilities.Timers;
using SuperTiled2Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Characters
{

    public class PlayerMovement : CharacterMovement
    {

        [SerializeField]
        AudioClip playerBumpSound;

        DeltaTimer bumpSoundTimer;


        public Sprite leftBikeSprite;
        public Sprite rightBikeSprite;
        public Sprite upBikeSprite;
        public Sprite downBikeSprite;

        // Start is called before the first frame update
        protected override void Start()
        {
            this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
            this.characterAnimator = this.gameObject.GetComponent<Animator>();
            this.oldPosition = this.gameObject.transform.position;
            this.currentPosition = this.gameObject.transform.position;
            this.newPosition = this.gameObject.transform.position;
            bumpSoundTimer = new DeltaTimer(0.5f, Enums.TimerType.CountDown, false);
            bumpSoundTimer.start();

            /*
            this.setMovementPath(new List<Enums.Direction>()
            {
                Enums.Direction.Right,
                Enums.Direction.Right,
                Enums.Direction.Up,
                Enums.Direction.Right,
                Enums.Direction.Down,
            });
            */
            facingDirection = Enums.Direction.Down;

            this.playMovementAnimation(facingDirection, false);

            DontDestroyOnLoad(this.gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            bumpSoundTimer.Update();
        }

        protected override void FixedUpdate()
        {
            //this.gameObject.transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime;
            getNextMovementPositionFromPath();
            checkForCollisionMovementRaycast();
            moveLerp();
            Camera.main.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -10);
            if (GameInput.InputControls.APressed&& this.CanMove)
            {
                checkForCollisionInteractionRaycast();
            }

            if (Menus.Menu.IsMenuUp == false && CanMove && GameInput.InputControls.StartPressed)
            {
                Menus.Menu.Instantiate<GameMenu>();
                return;
            }
            else if(Menus.Menu.IsMenuUp==true && GameInput.InputControls.StartPressed)
            {
                Menus.Menu.ActiveMenu.exitMenu();
            }

            //resetMovementAnimation();
        }

        /// <summary>
        /// For determining input movement
        /// </summary>
        private void checkForCollisionMovementRaycast()
        {
            if (CanMove == false) return;
            Vector2 delta = new Vector3(GameInput.InputControls.LeftJoystickHorizontal, GameInput.InputControls.LeftJoystickVertical, 0) * Time.deltaTime;

            Vector2 checkPosition = new Vector2();
            Enums.Direction nextDirection = Enums.Direction.Down;
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                if (delta.x < 0)
                {
                    checkPosition = new Vector2(-1, 0);
                    nextDirection = Enums.Direction.Left;
                }
                else if (delta.x > 0)
                {
                    checkPosition = new Vector2(1, 0);
                    nextDirection = Enums.Direction.Right;
                }
                else if (delta.x == 0)
                {

                }
            }
            else if (Mathf.Abs(delta.x) < Mathf.Abs(delta.y))
            {
                if (delta.y < 0)
                {
                    checkPosition = new Vector2(0, -1);
                    nextDirection = Enums.Direction.Down;
                }
                else if (delta.y > 0)
                {
                    checkPosition = new Vector2(0, 1);
                    nextDirection = Enums.Direction.Up;
                }
                else if (delta.y == 0)
                {

                }
            }
            else if (Mathf.Abs(delta.x) == Mathf.Abs(delta.y))
            {

            }

            if (checkPosition.x == 0 && checkPosition.y == 0)
            {

                resetMovementAnimation();
                return;
            }
            RaycastHit2D[] hits = Physics2D.RaycastAll(this.gameObject.transform.position, checkPosition, 1f);
            bool canMove = true;

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject == this.gameObject) continue;
                    GameObject detectedGameObject = hit.collider.gameObject;

                    if (hit.collider.isTrigger == false)
                    {
                        canMove = false;
                        Debug.Log("HIT SOMETHING???");
                    }
                    else
                    {
                        GameObject detectedCollisionObject = hit.collider.gameObject.transform.parent.gameObject;
                        SuperTiled2Unity.SuperCustomProperties properties = detectedCollisionObject.GetComponent<SuperTiled2Unity.SuperCustomProperties>();
                        if (properties != null)
                        {
                            CustomProperty p;
                            if (properties.TryGetCustomProperty("Surfable", out p) == true)
                            {
                                if (p.m_Value == "true")
                                {
                                    Debug.Log("Could surf here.");
                                    canMove = false;
                                }
                            }
                            if (properties.TryGetCustomProperty("WildPokemon", out p) == true)
                            {
                                if (p.m_Value == "true")
                                {
                                    Debug.Log("Wild Pokemon Here!");
                                    GameManager.Player.decrementStep();
                                    canMove = true;
                                    break;
                                }
                            }
                        }
                    }
                    this.facingDirection = nextDirection;


                    if (hit.collider.gameObject.GetComponent<Warp>() == null)
                    {
                        if (bumpSoundTimer.IsFinished)
                        {
                            GameInformation.GameManager.SoundManager.playSound(playerBumpSound, 0.75f);
                            bumpSoundTimer.restart();
                        }
                    }
                    playMovementAnimation(this.facingDirection, false);
                }
            }
            if (canMove)
            {
                //If no object detected!
                oldPosition = this.gameObject.transform.position;
                newPosition = this.gameObject.transform.position + (Vector3)checkPosition;
                this.facingDirection = nextDirection;
                playMovementAnimation(this.facingDirection, true);
            }
        }

        private void checkForCollisionInteractionRaycast()
        {


            Vector2 checkPosition = new Vector2();
            Enums.Direction nextDirection = this.facingDirection;
            if (nextDirection == Enums.Direction.Left)
            {
                checkPosition = new Vector2(-1, 0);
            }
            else if (nextDirection == Enums.Direction.Right)
            {
                checkPosition = new Vector2(1, 0);
            }
            else if (nextDirection == Enums.Direction.Up)
            {
                checkPosition = new Vector2(0, 1);
            }
            else if (nextDirection == Enums.Direction.Down)
            {
                checkPosition = new Vector2(0, -1);
            }


            RaycastHit2D[] hits = Physics2D.RaycastAll(this.gameObject.transform.position, checkPosition, 1f);

            foreach (RaycastHit2D hit in hits)
            {
                //RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, checkPosition, 1f);
                if (hit.collider != null)
                {

                    //Do logic!
                    if (hit.collider.gameObject == this.gameObject) continue;

                    Interactable i = hit.collider.gameObject.GetComponent<Interactables.Interactable>();
                    if (i != null)
                    {
                        if (hit.collider.gameObject.GetComponent<CharacterMovement>() != null)
                        {

                            hit.collider.gameObject.GetComponent<CharacterMovement>().faceDirection(Enums.GetOppositeDirection(nextDirection));
                        }

                        i.interact();
                        return;
                    }
                    try
                    {
                        GameObject detectedCollisionObject = hit.collider.gameObject.transform.parent.gameObject;
                        SuperTiled2Unity.SuperCustomProperties properties = detectedCollisionObject.GetComponent<SuperTiled2Unity.SuperCustomProperties>();
                        playMovementAnimation(this.facingDirection, false);
                        CustomProperty p;
                        if (properties.TryGetCustomProperty("Surfable", out p) == true)
                        {
                            if (p.m_Value == "true")
                            {
                                Debug.Log("Could surf here: Interaction");
                            }
                        }
                    }
                    catch (Exception err)
                    {

                    }
                }
                if (hit.collider == null)
                {
                    //Do nothing.
                }
            }
        }


        protected override void playMovementAnimation(Enums.Direction direction, bool hasMoved)
        {
            base.playMovementAnimation(direction, hasMoved);
            return;
        }


    }
}
