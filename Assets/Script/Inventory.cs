using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public Text input;
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
        slotsAmount =88;
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

        AddItem(2);
        AddItem(2);

        RemoveItem(0);
    }

    public void RemoveItem(int id)
    {
        Item itemToRemove = database.FetchItemByID(id);
        if (itemToRemove.Stackable && CheckIfinInventory(itemToRemove))
        {
            for (int j = 0; j < items.Count; j++)
            {
                if (items[j].ID == id)
                {
                    ItemData data = slots[j].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount--;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    if (data.amount == 0)
                    {
                        Destroy(slots[j].transform.GetChild(0).gameObject);
                        items[j] = new Item();
                        break;
                    }
                    if (data.amount == 1)
                    {
                        slots[j].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "";
                        break;
                    }
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
                if (items[i].ID != -1 && items[i].ID == id)
                {
                    Destroy(slots[i].transform.GetChild(0).gameObject);
                    items[i] = new Item();
                    break;
                }
        }
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
                itemObj.GetComponent<ItemData>().amount = 1;
                itemObj.GetComponent<ItemData>().slot = i;
                itemObj.transform.SetParent(slots[i].transform);
                    itemObj.GetComponent<RectTransform>().offsetMin = new Vector2(0,0);
                    itemObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                itemObj.name = itemToAdd.Title;
                break;
            }
        }
}
    }

    public void Add()
    {
        AddItem(int.Parse(input.text));
    }

    public void Remove()
    {
       RemoveItem(int.Parse(input.text));
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
