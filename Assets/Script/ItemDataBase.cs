using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
public class ItemDataBase : MonoBehaviour {
    private List<Item> DataBase = new List<Item>();
    private JsonData itemData;

    void Start()
    {
        //Item item = new Item(0, "Ball", 5);
        //DataBase.Add(item);
        //Debug.Log(DataBase[0]);


        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        ConstructItemDatabase();

        Debug.Log(DataBase[1].Title);
    }

    void ConstructItemDatabase()
    {
        for(int i=0;i<itemData.Count;++i)
        {
            DataBase.Add(new Item((int)itemData[i]["id"],itemData[i]["title"].ToString(),(int)itemData[i]["value"]));
        }
    }
}

public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }

    public Item(int id,string title,int value)
    {
        this.ID = id;
        this.Title = title;
        this.Value = value;
    }
}