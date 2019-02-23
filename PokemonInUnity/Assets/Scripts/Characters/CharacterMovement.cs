using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Characters
{
    public class CharacterMovement:MonoBehaviour
    {

        [SerializeField]
        protected float movementLerp;
        public float movementSpeed = 1f;

        protected Vector2 oldPosition;
        protected Vector2 newPosition;
        protected Vector2 currentPosition;

        List<Enums.Direction> directionsToMove=new List<Enums.Direction>();

        protected bool IsMoving
        {
            get
            {
                return movementLerp < 1f;
            }
        }

        /// <summary>
        /// Determines if the player can automatically move along the path
        /// </summary>
        protected bool CanAutoMove
        {
            get
            {
                return (movementLerp == 0f);
            }
        }

        /// <summary>
        /// Determines if the player can move via input
        /// </summary>
        protected bool CanMove
        {
            get
            {
                return (CanAutoMove && this.directionsToMove.Count==0);
            }
        }



        // Start is called before the first frame update
        protected virtual void Start()
        {
            this.oldPosition = this.gameObject.transform.position;
            this.currentPosition = this.gameObject.transform.position;
            this.newPosition = this.gameObject.transform.position;
        }

        // Update is called once per frame
        void Update()
        {

        }

        protected virtual void FixedUpdate()
        {
            //this.gameObject.transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime;
            //checkForCollisionRaycast();
            getNextMovementPositionFromPath();
            moveLerp();
        }

        protected void getNextMovementPositionFromPath()
        {

            if (CanAutoMove)
            {
                if (this.directionsToMove.Count == 0) return;


                //Same as dequeue
                Enums.Direction direction = this.directionsToMove[0];
                this.directionsToMove.RemoveAt(0);

                if (direction == Enums.Direction.Down)
                {
                    this.moveDown();
                }
                else if (direction == Enums.Direction.Left)
                {
                    this.moveLeft();
                }
                else if (direction == Enums.Direction.Right)
                {
                    this.moveRight();
                }
                else if (direction == Enums.Direction.Up)
                {
                    this.moveUp();
                }
            }
        }

        protected virtual void moveLerp()
        {
            
            if (newPosition == (Vector2)this.gameObject.transform.position)
            {
                this.movementLerp = 1f;
                if (movementLerp == 1f)
                {
                    this.oldPosition = this.gameObject.transform.position;
                    this.currentPosition = this.gameObject.transform.position;
                    this.newPosition = this.gameObject.transform.position;
                    movementLerp = 0;
                    Debug.Log("NANI??");
                }

                return;
            }
            
            
            movementLerp += getMovementSpeed();
            currentPosition = Vector2.Lerp(oldPosition, newPosition, movementLerp);
            this.gameObject.transform.position = currentPosition;


        }

        protected virtual float getMovementSpeed()
        {
            return movementSpeed * Time.deltaTime;
        }

        /// <summary>
        /// For tile direction
        /// </summary>
        /// <param name="direction"></param>
        protected virtual void checkForCollisionRaycast(Vector2 direction)
        {
            if (CanAutoMove == false) return;
            Vector2 delta = direction;

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
            else if (Mathf.Abs(delta.x) < Mathf.Abs(delta.y))
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
            else if (Mathf.Abs(delta.x) == Mathf.Abs(delta.y))
            {

            }

            if (checkPosition.x == 0 && checkPosition.y == 0) return;
            RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, checkPosition, 1f);
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
                    
                }
            }
            if (hit.collider == null)
            {
                
                oldPosition = this.gameObject.transform.position;
                newPosition = this.gameObject.transform.position + (Vector3)checkPosition;
            }
        }


        protected virtual void moveLeft()
        {
            checkForCollisionRaycast(new Vector2(-1, 0));
        }
        protected virtual void moveRight()
        {
            checkForCollisionRaycast(new Vector2(1, 0));
        }
        protected virtual void moveDown()
        {
            checkForCollisionRaycast(new Vector2(0, -1));
        }
        protected virtual void moveUp()
        {
            checkForCollisionRaycast(new Vector2(0, 1));
        }


        /// <summary>
        /// Sets a forced movement path for the player to follow.
        /// </summary>
        /// <param name="movementPath"></param>
        public virtual void setMovementPath(List<Enums.Direction> movementPath)
        {
            this.directionsToMove = movementPath;
        }

        /*
         * move up/down/left/right
         * 
         */
    }
}
