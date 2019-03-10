using Assets.Scripts.Menus;
using Assets.Scripts.Menus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyMemberSelectMenu : Menu
{

    GameObject background;

    public MenuComponent selectSnap;
    public MenuComponent statsSnap;
    public MenuComponent switchSnap;
    public MenuComponent closeSnap;

    public override void Start()
    {
        this.canvas = this.transform.Find("Canvas").gameObject;
        background = this.transform.Find("Canvas").Find("Image").gameObject;

        selectSnap = new MenuComponent(background.transform.Find("Select").Find("SnapComponent").gameObject.GetComponent<Image>());
        statsSnap = new MenuComponent(background.transform.Find("Stats").Find("SnapComponent").gameObject.GetComponent<Image>());
        switchSnap = new MenuComponent(background.transform.Find("Switch").Find("SnapComponent").gameObject.GetComponent<Image>());
        closeSnap = new MenuComponent(background.transform.Find("Close").Find("SnapComponent").gameObject.GetComponent<Image>());

        this.menuCursor = Menu.GetCursorFromParentMenu();
        this.menuCursor.gameObject.transform.parent = canvas.transform;
        //this.menuCursor = canvas.transform.Find("GameCursor").gameObject.GetComponent<Assets.Scripts.GameInput.GameCursor>();
        setUpForSnapping();
    }

    public override void exitMenu()
    {
        this.menuCursor.gameObject.transform.parent = Menu.ParentMenu().canvas.transform;

        base.exitMenu();
        GameMenu.ActiveMenu = null;
        GameMenu.ActiveMenu.menuCursor.snapToCurrentMenuComponent();
    }

    public override void setUpForSnapping()
    {
        selectSnap.setNeighbors(null, null, null, statsSnap);
        statsSnap.setNeighbors(null, null, selectSnap, switchSnap);
        switchSnap.setNeighbors(null, null, statsSnap, closeSnap);
        closeSnap.setNeighbors(null, null, switchSnap, null);

        this.selectedComponent = selectSnap;
        this.selectedComponent.snapToThisComponent();
    }

    public override bool snapCompatible()
    {
        return true;
    }

    public override void Update()
    {
       
    }
}
