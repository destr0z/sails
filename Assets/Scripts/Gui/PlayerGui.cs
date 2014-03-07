using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerGui : MonoBehaviour {

	public Dictionary<string,int> inventory {get;set;}

	void Start() {
		if (inventory == null) {
			inventory = new Dictionary<string, int>();
		}
	}
	
	void OnGUI()
	{
		GUI.Box(new Rect(0, Screen.height - 100, Screen.width, 100), "Inventory");

		var height = 100;
		foreach (var item in inventory)
		{
			height -= 20;
			GUI.Label(new Rect(0, Screen.height-height, 100, 20), item.Key +": "+item.Value);
		}
	}
}
