﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Characters {

    public class PlayerMovement : CharacterMovement
    {

        

        

        // Start is called before the first frame update
        protected override void Start()
        {
            this.oldPosition = this.gameObject.transform.position;
            this.currentPosition = this.gameObject.transform.position;
            this.newPosition = this.gameObject.transform.position;

            
            this.setMovementPath(new List<Enums.Direction>()
            {
                Enums.Direction.Right,
                Enums.Direction.Right,
                Enums.Direction.Up,
                Enums.Direction.Right,
                Enums.Direction.Down,
            });
            
        }

        // Update is called once per frame
        void Update()
        { 
        }

        protected override void FixedUpdate()
        {
            //this.gameObject.transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime;
            getNextMovementPositionFromPath();
            checkForCollisionRaycast();
            moveLerp();
            Camera.main.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -10);
        }

        /// <summary>
        /// For determining input movement
        /// </summary>
        private void checkForCollisionRaycast()
        {
            if (CanMove==false) return;
            Vector2 delta=new Vector3(GameInput.InputControls.LeftJoystickHorizontal, GameInput.InputControls.LeftJoystickVertical, 0) * Time.deltaTime;

            Vector2 checkPosition = new Vector2();
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                if (delta.x < 0)
                {
                    checkPosition = new Vector2(-1, 0);
                }
                else if (delta.x > 0)
                {
                    checkPosition = new Vector2(1, 0);

                }
                else if (delta.x == 0)
                {

                }
            }
            else if(Mathf.Abs(delta.x) < Mathf.Abs(delta.y))
            {
                if (delta.y < 0)
                {
                    checkPosition = new Vector2(0, -1);
                }
                else if (delta.y > 0)
                {
                    checkPosition = new Vector2(0, 1);
                }
                else if (delta.y == 0)
                {

                }
            }
            else if(Mathf.Abs(delta.x) == Mathf.Abs(delta.y))
            {

            }

            if (checkPosition.x == 0 && checkPosition.y == 0) return;
            RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, checkPosition,1f);
            if (hit.collider != null)
            {
                GameObject detectedGameObject = hit.collider.gameObject;
                Debug.Log(detectedGameObject.name);
                if (detectedGameObject.GetComponent<Collider2D>() == null)
                {
                    //move
                    oldPosition = this.gameObject.transform.position;
                    newPosition = this.gameObject.transform.position + (Vector3)checkPosition;
                }
                else
                {
                    //Do logic!
                    Debug.Log("COLLISION AT: " + checkPosition);
                }
            }
            if(hit.collider == null)
            {
                oldPosition = this.gameObject.transform.position;
                newPosition=this.gameObject.transform.position + (Vector3)checkPosition;
            }
        }

        
    }
}
