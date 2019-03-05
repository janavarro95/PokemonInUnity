using Assets.Scripts.GameInformation;
using Assets.Scripts.Utilities.Timers;
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

        [SerializeField]
        protected Animator characterAnimator;

        [SerializeField]
        protected Enums.Direction facingDirection;

        
        public Sprite leftSprite;
        public Sprite rightSprite;
        public Sprite upSprite;
        public Sprite downSprite;

        protected SpriteRenderer spriteRenderer;

        private enum MovementType
        {
            Stationary,
            Random
        }

        [SerializeField]
        private MovementType movementType;
        [SerializeField]
        private DeltaTimer randomMoveTimer;

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
                return (CanAutoMove && this.directionsToMove.Count==0 && GameManager.Manager.isObjectActiveInteractable(this.gameObject)==false && GameInformation.GameManager.Manager.dialogueManager.isDialogueUp==false);
            }
        }



        // Start is called before the first frame update
        protected virtual void Start()
        {
            this.characterAnimator = this.gameObject.GetComponent<Animator>();
            this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
            this.oldPosition = this.gameObject.transform.position;
            this.currentPosition = this.gameObject.transform.position;
            this.newPosition = this.gameObject.transform.position;

            if(this.movementType== MovementType.Random)
            {
                float time = UnityEngine.Random.Range(0.0f, 10.0f);
                this.randomMoveTimer = new DeltaTimer(time, Enums.TimerType.CountDown, false, new Utilities.Delegates.VoidDelegate(randomMove));
                this.randomMoveTimer.start();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (this.randomMoveTimer != null && this.CanMove) this.randomMoveTimer.Update();
        }

        protected virtual void FixedUpdate()
        {
            //this.gameObject.transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * Time.deltaTime;
            //checkForCollisionRaycast();
            getNextMovementPositionFromPath();
            moveLerp();
            if (this.directionsToMove.Count == 0 && CanMove)
            {
                resetMovementAnimation();
            }
        }

        protected void getNextMovementPositionFromPath()
        {

            if (CanAutoMove)
            {
                if (this.directionsToMove.Count == 0)
                {
                    //resetMovementAnimation();
                    return;
                }

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
        protected virtual bool checkForCollisionRaycast(Vector2 direction)
        {
            if (CanAutoMove == false) return false;
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

            if (checkPosition.x == 0 && checkPosition.y == 0) return false;
            RaycastHit2D[] hits = Physics2D.RaycastAll(this.gameObject.transform.position, checkPosition, 1f);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    GameObject detectedGameObject = hit.collider.gameObject;
                    if (detectedGameObject.GetComponent<Collider2D>() == null)
                    {
                        //move
                        oldPosition = this.gameObject.transform.position;
                        newPosition = this.gameObject.transform.position + (Vector3)checkPosition;
                        return true;
                    }
                    else
                    {
                        //Do logic!
                        return false;
                    }
                }
                if (hit.collider == null)
                {

                    oldPosition = this.gameObject.transform.position;
                    newPosition = this.gameObject.transform.position + (Vector3)checkPosition;
                    return true;
                }
            }
            return false;
        }


        protected virtual void moveLeft()
        {
            bool animate=checkForCollisionRaycast(new Vector2(-1, 0));
            if (animate == true)
            {
                characterAnimator.Play("WalkingLeft");
            }
            else
            {
                characterAnimator.Play("StandingIdleLeft");
            }
            this.facingDirection = Enums.Direction.Left;
        }
        protected virtual void moveRight()
        {
            bool animate = checkForCollisionRaycast(new Vector2(1, 0));
            if (animate == true)
            {
                characterAnimator.Play("WalkingRight");
            }
            else
            {
                characterAnimator.Play("StandingIdleRight");
            }
            this.facingDirection = Enums.Direction.Right;
        }
        protected virtual void moveDown()
        {
            bool animate = checkForCollisionRaycast(new Vector2(0, -1));
            if (animate == true)
            {
                characterAnimator.Play("WalkingDown");
            }
            else
            {
                characterAnimator.Play("StandingIdleDown");
            }
            this.facingDirection = Enums.Direction.Down;
        }
        protected virtual void moveUp()
        {
            bool animate = checkForCollisionRaycast(new Vector2(0, 1));
            if (animate == true)
            {
                characterAnimator.Play("WalkingUp");
            }
            else
            {
                characterAnimator.Play("StandingIdleUp");
            }
            this.facingDirection = Enums.Direction.Up;
        }


        public virtual void faceDirection(Enums.Direction Dir)
        {
            if(Dir== Enums.Direction.Down)
            {
                faceDown();
            }
            else if(Dir== Enums.Direction.Left)
            {
                faceLeft();
            }
            else if(Dir== Enums.Direction.Right)
            {
                faceRight();
            }
            else if(Dir== Enums.Direction.Up)
            {
                faceUp();
            }
        }

        public virtual void faceLeft()
        {
            this.facingDirection = Enums.Direction.Left;
            this.resetMovementAnimation();
        }

        public virtual void faceRight()
        {
            this.facingDirection = Enums.Direction.Right;
            this.resetMovementAnimation();
        }

        public virtual void faceDown()
        {
            this.facingDirection = Enums.Direction.Down;
            this.resetMovementAnimation();
        }

        public virtual void faceUp()
        {
            this.facingDirection = Enums.Direction.Up;
            this.resetMovementAnimation();
        }

        /// <summary>
        /// Sets a forced movement path for the player to follow.
        /// </summary>
        /// <param name="movementPath"></param>
        public virtual void setMovementPath(List<Enums.Direction> movementPath)
        {
            this.directionsToMove = movementPath;
        }

        protected virtual void playMovementAnimation(Enums.Direction direction, bool hasMoved)
        {
            if (direction == Enums.Direction.Down)
            {
                if (hasMoved)
                {
                    this.characterAnimator.Play("WalkingDown");
                }
                else
                {
                    this.spriteRenderer.sprite = downSprite;
                    this.characterAnimator.Play("StandingIdleDown");
                }
            }
            if (direction == Enums.Direction.Left)
            {
                if (hasMoved)
                {
                    this.characterAnimator.Play("WalkingLeft");
                }
                else
                {
                    this.spriteRenderer.sprite = leftSprite;
                    this.characterAnimator.Play("StandingIdleLeft");
                }
            }
            if (direction == Enums.Direction.Up)
            {
                if (hasMoved)
                {
                    this.characterAnimator.Play("WalkingUp");
                }
                else
                {
                    this.spriteRenderer.sprite = upSprite;
                    this.characterAnimator.Play("StandingIdleUp");
                }
            }
            if (direction == Enums.Direction.Right)
            {
                if (hasMoved)
                {
                    this.characterAnimator.Play("WalkingRight");
                }
                else
                {
                    this.spriteRenderer.sprite = rightSprite;
                    this.characterAnimator.Play("StandingIdleRight");
                }
            }
        }

        protected virtual void resetMovementAnimation()
        {
            if (CanMove == true)
            {
                playMovementAnimation(this.facingDirection, false);
            }
        }

        private void randomMove()
        {
            Debug.Log("RESTART?");
            Enums.Direction dir =(Enums.Direction)UnityEngine.Random.Range(0, 4);
            this.directionsToMove.Add(dir);
            float time = UnityEngine.Random.Range(0.0f, 10.0f);
            this.randomMoveTimer = new DeltaTimer(time, Enums.TimerType.CountDown, false, randomMove);
            this.randomMoveTimer.start();
        }

        /*
         * move up/down/left/right
         * 
         */
    }
}
