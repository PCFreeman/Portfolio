using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTips : MonoBehaviour {

    private Item item;
    private string data;
    private GameObject tooltip;


    void Start()
    {
        tooltip = GameObject.Find("ToolTips");
        tooltip.SetActive(false);
    }

    void Update()
    {
        if(tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition;
        }
    }
    public void Activate(Item item)
    {
        this.item = item;
        ConstructDataString();
        tooltip.SetActive(true);
    }


    public void Deactivate()
    {
        tooltip.SetActive(false);
    }
    
    public void ConstructDataString()
    {
        data = "<color=#0473f0><b>"+item.Title
            +"</b></color>\n\nValue"+item.Value
            + "\nID:" + item.ID
            + "\nDamage:"+item.Damage
            +"\nMagic Damage:"+item.Magic
            +"\nLevel:"+item.Level
            +"\nDescription:"+item.Description;
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;

    }
}
