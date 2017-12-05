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

        Debug.Log(FetchItemByID(2).Description);

    }

    public Item FetchItemByID(int id)
    {
        for(int i=0;i<DataBase.Count;i++)
        {
            if (DataBase[i].ID == id)
                return DataBase[i];    
        }
        return null;
    }

    void ConstructItemDatabase()
    {
        for(int i=0;i<itemData.Count;++i)
        {
    DataBase.Add(new Item((int)itemData[i]["id"],
                               itemData[i]["title"].ToString(),
                          (int)itemData[i]["value"],
                          (int)itemData[i]["stats"]["damage"], 
                          (int)itemData[i]["stats"]["level"],
                          (int)itemData[i]["stats"]["magic"],
                               itemData[i]["description"].ToString(),
                               itemData[i]["slug"].ToString(),
                         (bool)itemData[i]["usable"],
                         (bool)itemData[i]["stackable"]));
        }
    }
}

public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public int Damage { get; set; }
    public int Level { get; set; }
    public int Magic { get; set; }
    public string Description { get; set; }
    public string Slug { get; set; }
    public bool Usable { get; set; }
    public bool Stackable { get; set; }
    public Sprite Sprite { get; set; }

    public Item(int id, string title, int value,int damage,int level,int magic,string description,string slug, bool usable,bool stackable)
    {
        this.ID = id;
        this.Title = title;
        this.Value = value;
        this.Damage = damage;
        this.Level = level;
        this.Magic = magic;
        this.Description = description;
        this.Slug = slug;
        this.Usable = usable;
        this.Stackable = stackable;
        this.Sprite = Resources.Load<Sprite>("Item/" + slug);
    }
    public Item()
    {
         this.ID = -1;
    }

}