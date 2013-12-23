using UnityEngine;
using System.Collections;

public class ShopMenu : MonoBehaviour {

    public string item;
    public int amount;
	public string storename;

    bool showGui = false;

	void Start() 
	{
		if(!Stores.StoreList.ContainsKey(storename)) {
			Stores.StoreList.Add(storename, new Stores.StoreInventory() {StoreItem = item, StoreItemAmount=amount});
		}
	}

    void OnGUI()
    {
        if (showGui)
        {
            GUI.Box(new Rect(0, 0, 80, 100), "Shop");
			GUI.Label(new Rect(0, 20, 80, 20), item + ": "+Stores.StoreList[storename].StoreItemAmount);
            if (GUI.Button(new Rect(0, 40, 80, 20), "Buy"))
            {
				if(Stores.StoreList[storename].StoreItemAmount > 0) {
					Stores.StoreList[storename].StoreItemAmount--;
				}
            }

            if (GUI.Button(new Rect(0, 60, 80, 20), "Sell"))
            {
				if(Stores.StoreList[storename].StoreItemAmount > 0) {
					Stores.StoreList[storename].StoreItemAmount++;
				}
            }

            if (GUI.Button(new Rect(0, 80, 80, 20), "Close"))
            {
                showGui = false;
            }
        }
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            showGui = true;
        }
    }
}
