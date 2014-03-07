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
			var store = Stores.StoreList[storename];
			var player = GameObject.FindWithTag ("Player");
			var playerGui = player.GetComponent<PlayerGui>();

			var playerInventory = playerGui.inventory;

            GUI.Box(new Rect(0, 0, 80, 100), "Shop");
			GUI.Label(new Rect(0, 20, 80, 20), item + ": "+store.StoreItemAmount);
            if (GUI.Button(new Rect(0, 40, 80, 20), "Buy"))
            {
				if(store.StoreItemAmount > 0) {
					store.StoreItemAmount--;
					if(playerInventory.ContainsKey(store.StoreItem)) {
						playerInventory[store.StoreItem]++;
					} else {
						playerInventory.Add(store.StoreItem, 1);
					}
				}

				networkView.RPC("UpdateStore", RPCMode.OthersBuffered, "buy");
            }

			if(playerInventory.ContainsKey(store.StoreItem) && playerInventory[store.StoreItem] > 0 ) {
				if (GUI.Button(new Rect(0, 60, 80, 20), "Sell"))
				{
					store.StoreItemAmount++;
					playerInventory[store.StoreItem]--;
					
					networkView.RPC("UpdateStore", RPCMode.OthersBuffered, "sell");
				}
			}
			
			if (GUI.Button(new Rect(0, 80, 80, 20), "Close"))
            {
                showGui = false;
            }
        }
    }


	[RPC]
	void UpdateStore(string message)
	{
		var store = Stores.StoreList[storename];
		if (message == "buy") {
			if(store.StoreItemAmount > 0) {
				store.StoreItemAmount--;
			}
		}

		if (message == "sell") {
			store.StoreItemAmount++;
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
