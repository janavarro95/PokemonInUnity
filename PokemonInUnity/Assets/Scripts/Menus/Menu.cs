using Assets.Scripts.GameInformation;
using Assets.Scripts.GameInput;
using Assets.Scripts.Menus.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menus
{
    public class Menu : MonoBehaviour
    {

        public static List<Menu> MenuStack;


        public static Menu ActiveMenu
        {
            get
            {
                if (MenuStack == null) MenuStack = new List<Menu>();

                if (MenuStack.Count == 0) return null;
                return MenuStack[MenuStack.Count - 1];
            }
            set
            {
                if (MenuStack == null)
                {
                    MenuStack = new List<Menu>();
                }

                if (value != null)
                {
                    if (MenuStack.Contains(value)) return;
                    MenuStack.Add(value);
                }
                else
                {
                    MenuStack.RemoveAt(MenuStack.Count - 1);
                }
            }
        }

        public static bool IsMenuUp
        {
            get
            {
                return ActiveMenu != null;
            }
        }

        public static GameCursor GetCursorFromParentMenu()
        {
            if (MenuStack.Count < 2)
            {
                Debug.Log("No parent menu to get cursor from!");
                return null;
            }
            else
            {
                return MenuStack[MenuStack.Count - 2].menuCursor;
            }
        }

        public static Menu ParentMenu()
        {
            if (MenuStack.Count < 2)
            {
                Debug.Log("No parent menu!");
                return null;
            }
            else
            {
                return MenuStack[MenuStack.Count - 2];
            }
        }

        public static void exitMenusUntilThisOne(Menu m)
        {
            while (ActiveMenu != m)
            {
                ActiveMenu.exitMenu();
            }
        }


        public GameCursor menuCursor;
        public MenuComponent selectedComponent;
        
        public GameObject canvas;

        public virtual void Start()
        {
           
            
        }

        public virtual void Update()
        {

        }

        public void scaleMenuToSceen()
        {
            this.canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
        }

        public virtual void exitMenu()
        {
            Destroy(this.gameObject);
        }

        public virtual bool snapCompatible()
        {
            return false;
        }

        public virtual void setUpForSnapping()
        {

        }

        public static void Instantiate()
        {
            Instantiate<Menu>();
        }

        public static void Instantiate<T>()
        {
            if(typeof(T) == typeof(Menu))
            {
                Instantiate("Menu");
            }
            else if (typeof(T) == typeof(MainMenu))
            {
                Instantiate("MainMenu");
            }
            else if (typeof(T) == typeof(GameMenu))
            {
                Instantiate("GameMenu");
            }
            else if (typeof(T) == typeof(PokemonPartyMenu))
            {
                Instantiate("PokemonPartyMenu");
            }
            else if (typeof(T) == typeof(PartyMemberSelectMenu))
            {
                Instantiate("PartyMemberSelectMenu");
            }
            else if (typeof(T) == typeof(YesNoMenu))
            {
                Instantiate("YesNoMenu");
            }
            else
            {
                throw new Exception("Hmm trying to call on a type of menu that doesn't exist.");
            }
        }

        public static void Instantiate(string Name)
        {
            Menu.ActiveMenu = LoadMenuFromPrefab(Name).GetComponent<Menu>();
        }

        protected static GameObject LoadMenuFromPrefab(string ItemName)
        {
            string path = Path.Combine(Path.Combine("Prefabs", "Menus"), ItemName);
            GameObject menuObj=Instantiate((GameObject)Resources.Load(path, typeof(GameObject)));
            return menuObj;
        }
    }
}
