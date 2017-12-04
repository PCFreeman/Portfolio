﻿using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    GameObject inventoryPanel;
    GameObject slotPanel;
    ItemDataBase database;
    public GameObject InventorySlot;
    public GameObject inventoryItem;

    int slotsAmount;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    private void Start()
    {
        database = GetComponent<ItemDataBase>();
        slotsAmount = 16;
        inventoryPanel = GameObject.Find("Inventory Panel");
        slotPanel = inventoryPanel.transform.Find("Slot Panel").gameObject;
        for(int i=0;i< slotsAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(InventorySlot));
            slots[i].GetComponent<Slot>().id = i;
            slots[i].transform.SetParent(slotPanel.transform);
        }

        AddItem(0);

        AddItem(1);
        AddItem(1);
        AddItem(1);
        AddItem(1);



    }

    public void AddItem(int id)
    {
        Item itemToAdd = database.FetchItemByID(id);
        if(itemToAdd.Stackable&&CheckIfinInventory(itemToAdd))
        {
            for(int i=0;i<items.Count;i++)
            {
                if (items[i].ID==id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;
                }
            }
        }
        else
        {   
        for(int i=0;i<items.Count;i++)
        {
            if(items[i].ID==-1)
            {
                items[i] = itemToAdd;
                GameObject itemObj = Instantiate(inventoryItem);
                itemObj.GetComponent<ItemData>().item = itemToAdd;
                itemObj.GetComponent<ItemData>().slot = i;
                itemObj.transform.SetParent(slots[i].transform);
                itemObj.transform.position = Vector2.zero;
                itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                itemObj.name = itemToAdd.Title;
                break;
            }
        }
}
    }

    bool CheckIfinInventory(Item item)
    {
        for(int i=0;i<items.Count;i++)
        {
            if(items[i].ID==item.ID)
            {
                return true;
            }
        }
            return false;
    }
}