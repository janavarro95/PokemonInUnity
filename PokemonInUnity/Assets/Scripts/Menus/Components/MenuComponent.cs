﻿using Assets.Scripts.GameInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Menus.Components
{
    public class MenuComponent
    {
        public MenuComponent leftNeighbor;
        public MenuComponent rightNeighbor;
        public MenuComponent topNeighbor;
        public MenuComponent bottomNeighbor;

        public MonoBehaviour unityObject;

        public bool Selected
        {
            get
            {
                if(EventSystem.current != null)
                {
                    if (EventSystem.current.currentSelectedGameObject == unityObject.gameObject)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public MenuComponent(MonoBehaviour UnityObject)
        {
            this.unityObject = UnityObject;
        }

        public virtual void setNeighbors(MenuComponent Left=null, MenuComponent Right=null, MenuComponent Top=null, MenuComponent Bottom = null)
        {
            leftNeighbor = Left;
            rightNeighbor = Right;
            topNeighbor = Top;
            bottomNeighbor = Bottom;
        }

        public void snapToThisComponent()
        {
            if (Menu.ActiveMenu.menuCursor != null)
            {
                if (Menu.ActiveMenu.selectedComponent != null)
                {
                    Debug.Log("SNAP TO COMPONENT");
                    Menu.ActiveMenu.menuCursor.gameObject.GetComponent<RectTransform>().position = this.unityObject.transform.position;
                }
            }
        }

        /// <summary>
        /// Snaps to the neighbor of the current component as given by the direction,
        /// </summary>
        /// <param name="NextDirection"></param>
        /// <returns></returns>
        public virtual MenuComponent snapToNextComponent(Enums.Direction NextDirection)
        {
            if (Menu.ActiveMenu.menuCursor != null)
            {
                if(NextDirection== Enums.Direction.Up && topNeighbor!=null)
                {
                    Menu.ActiveMenu.menuCursor.gameObject.GetComponent<RectTransform>().position = topNeighbor.unityObject.transform.position;
                    Menu.ActiveMenu.selectedComponent = topNeighbor;
                    return topNeighbor;
                }
                if (NextDirection == Enums.Direction.Down && bottomNeighbor != null)
                {
                    Menu.ActiveMenu.menuCursor.gameObject.GetComponent<RectTransform>().position = bottomNeighbor.unityObject.transform.position;
                    Menu.ActiveMenu.selectedComponent = bottomNeighbor;
                    return bottomNeighbor;
                }
                if (NextDirection == Enums.Direction.Left && leftNeighbor != null)
                {
                    Menu.ActiveMenu.menuCursor.gameObject.GetComponent<RectTransform>().position = leftNeighbor.unityObject.transform.position;
                    Menu.ActiveMenu.selectedComponent = leftNeighbor;
                    return leftNeighbor;
                }
                if (NextDirection == Enums.Direction.Right && rightNeighbor != null)
                {
                    Menu.ActiveMenu.menuCursor.gameObject.GetComponent<RectTransform>().position = rightNeighbor.unityObject.transform.position;
                    Menu.ActiveMenu.selectedComponent = rightNeighbor;
                    return rightNeighbor;
                }

                return Menu.ActiveMenu.selectedComponent;
            }
            return null; //No snapping here!
        }

        public virtual void select()
        {

        }

    }
}
