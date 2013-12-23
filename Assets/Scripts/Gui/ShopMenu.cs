using UnityEngine;
using System.Collections;

public class ShopMenu : MonoBehaviour {

    public string item;
    public int amount;

    bool showGui = false;

    void OnGUI()
    {
        if (showGui)
        {
            GUI.Box(new Rect(0, 0, 80, 100), "Shop");
            GUI.Label(new Rect(0, 20, 80, 20), item + ": "+amount);
            if (GUI.Button(new Rect(0, 40, 80, 20), "Buy"))
            {
                amount--;
            }

            if (GUI.Button(new Rect(0, 60, 80, 20), "Sell"))
            {
                amount++;
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
