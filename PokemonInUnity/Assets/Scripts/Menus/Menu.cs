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

        /// <summary>
        /// Gets the top most "Active" Menu from the menu stack.
        /// </summary>
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
        
        /// <summary>
        /// Checks if there is atleast one menu on the menu stack.
        /// </summary>
        public static bool IsMenuUp
        {
            get
            {
                return ActiveMenu != null;
            }
        }

        /// <summary>
        /// Gets the game cursor from whatever menu came before this one.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the menu that came before this one on the menu stack.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Exits all menus until the passed in menu is reached.
        /// </summary>
        /// <param name="m"></param>
        public static void exitMenusUntilThisOne(Menu m)
        {
            while (ActiveMenu != m)
            {
                ActiveMenu.exitMenu();
            }
        }

        /// <summary>
        /// Closes all menus.
        /// </summary>
        public static void ExitAllMenus()
        {
            foreach(Menu m in MenuStack)
            {
                m.exitMenu();
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
            else if (typeof(T) == typeof(PokemonStatusMenu))
            {
                Instantiate("PokemonStatusMenu");
            }
            else if (typeof(T) == typeof(PokemonStatusMovesMenu))
            {
                Instantiate("PokemonStatusMovesMenu");
            }
            else if (typeof(T) == typeof(PokemonBattleMenu))
            {
                Instantiate("PokemonBattleMenu");
            }
            else if (typeof(T) == typeof(Battle.V1.BattleManagerV1))
            {
                Instantiate("BattleManagerV1");
            }
            else if(typeof(T)== typeof(Battle.V1.BattleActionSelectionMenu))
            {
                Instantiate("BattleActionSelectionMenu");
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
