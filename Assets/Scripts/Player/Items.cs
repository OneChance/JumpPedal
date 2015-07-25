using UnityEngine;
using System.Collections;

public class Items : MonoBehaviour
{
		public Hashtable itemList;
		public bool springShoe;
		public float itemTime = 8f;
		public float springShoeBounce = 2f;

		void Awake ()
		{
				itemList = new Hashtable ();			
				itemList.Add (PedalControl.PedalTypes.Iron, 50);
				itemList.Add (PedalControl.PedalTypes.Wood, 50);

				springShoe = false;
		}

}
