using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stores {

	public static Dictionary<string, StoreInventory> StoreList = new Dictionary<string, StoreInventory>();

	public class StoreInventory {
		public string StoreItem { get; set; }
		public int StoreItemAmount { get; set; }
	}
}
