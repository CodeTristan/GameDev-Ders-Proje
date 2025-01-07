using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string Name;
    public string Description;
    public Sprite Sprite;
    public int Amount;
    public int MaxAmount;

    public Item(string name, string description, Sprite sprite, int amount, int maxAmount)
    {
        Name = name;
        Description = description;
        Sprite = sprite;
        Amount = amount;
        MaxAmount = maxAmount;
    }
}
public class AllItems
{
    public List<Item> Items;

    public AllItems()
    {
        Items = new List<Item>();

        //Item Template
        Items.Add(new Item
            (
            "Item Name", "Item Description", SpriteManager.instance.GetItemSprite("Item Sprite Name").sprite,1,10
            ));
    }
}
