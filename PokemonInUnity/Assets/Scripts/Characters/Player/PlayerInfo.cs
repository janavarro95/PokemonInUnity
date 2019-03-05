using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Characters.Player
{
    [Serializable,SerializeField]
    public class PlayerInfo
    {


        public string playerName;

        public Enums.Direction facingDirection
        {
            get
            {
                return playerMovement.facingDirection;
            }
        }

        [JsonIgnore]
        private GameObject _gameObject;
        [JsonIgnore]
        public GameObject gameObject
        {
            get
            {
                if (_gameObject == null)
                {
                    _gameObject = GameObject.FindWithTag("Player");
                    GameObject.DontDestroyOnLoad(_gameObject);
                    return _gameObject;
                }
                else
                {
                    return _gameObject;
                }
            }
            set
            {
                if (value.tag == "Player")
                {
                    _gameObject = value;
                    renderer = _gameObject.GetComponent<SpriteRenderer>();
                    _playerMovement = _gameObject.GetComponent<PlayerMovement>();
                }
            }
        }
        public Vector3 position
        {
            get
            {
                return this.gameObject.transform.position;
            }
            set
            {
                this.gameObject.transform.position = value;
            }
        }

        private PlayerMovement _playerMovement;
        public PlayerMovement playerMovement
        {
            get
            {
                if (_playerMovement == null)
                {
                    _playerMovement=this.gameObject.GetComponent<PlayerMovement>();
                }
                return _playerMovement;
            }
        }


        private SpriteRenderer renderer;
        public SpriteRenderer Renderer
        {
            get
            {
                if (renderer == null)
                {
                    renderer = this.gameObject.GetComponent<SpriteRenderer>();
                    return renderer;
                }
                else
                {
                    return renderer;
                }

            }
        }


        public PlayerInfo()
        {

        }

        public void setSpriteVisibility(Enums.Visibility visibility)
        {
            if (visibility == Enums.Visibility.Invisible)
            {
                Renderer.enabled = false;
            }
            else
            {
                Renderer.enabled = true;
            }
        }


    }
}
