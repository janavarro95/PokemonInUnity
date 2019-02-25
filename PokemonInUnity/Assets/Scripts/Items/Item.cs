using Assets.Scripts.Content.PokeDatabase;
using Assets.Scripts.GameInformation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Item {

    /*Name
     * Description
     * Sprite
     * CanBeHeld
     * CanBeTossed
     * 
     */


    public string itemName;

    public string Name
    {
        get
        {
            return itemName;
        }
        set
        {
            this.itemName = value;
        }
    }

    public string description;
    public bool canBeHeld;

    public int stack;

    public Texture2D sprite;

    public Assets.Scripts.Enums.ItemType itemType;

    public int id;

    public Item()
    {

    }

    public Item(string Name, string Description,bool CanBeHeld,int Amount=1)
    {
        this.itemName = Name;
        this.description = Description;
        this.canBeHeld = CanBeHeld;
        stack = Amount;
    }

    public void addToStack(int amount)
    {
        this.stack += amount;
    }

    public void removeFromStack(int amount)
    {
        this.stack -= amount;
    }


    public virtual Item clone()
    {
        return new Item(this.Name,this.description,this.canBeHeld,this.stack);
    }

    public virtual Item clone(int StackSize=1)
    {
        return new Item(this.Name, this.description, this.canBeHeld, StackSize);
    }


    protected virtual void loadSpriteFromDisk()
    {
       
    }

    protected virtual void onUse()
    {

    }
}
