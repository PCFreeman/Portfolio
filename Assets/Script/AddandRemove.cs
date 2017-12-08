using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AddandRemove : MonoBehaviour {

    public Text input;
    public static Inventory inv;
	// Use this for initialization
	void Start () {
		
	}

    public void Add()
    {
        inv.AddItem(int.Parse(input.text));
    }

    public void Remove()
    {
        inv.RemoveItem(int.Parse(input.text));
    }
    // Update is called once per frame
    void Update () {
		
	}
}
