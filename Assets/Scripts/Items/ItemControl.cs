using UnityEngine;
using System.Collections;

public class ItemControl : MonoBehaviour
{
		private ItemsCreate itemCreate;
		public ItemType currentItemType; //脚本所属的道具类型
		private Items items;
		public int capNum = 10; //踏板道具包含的踏板数量
		private float existTime = 20f; //道具存在时间
		private float existTimer = 0;
		public GameObject pickUpEffect;

		public enum ItemType
		{  
				Wood,  
				Iron,				
				SpringShoe
		}

		void Awake ()
		{
				itemCreate = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<ItemsCreate> ();
				items = GameObject.FindGameObjectWithTag (Tags.player).GetComponent<Items> ();
		}

		void Update ()
		{
				existTimer += Time.deltaTime;
				if (existTimer >= existTime) {
						Destroy (gameObject);		
						playPickUpEffect ();
				}
		}

		void playPickUpEffect ()
		{
				GameObject go = Instantiate (pickUpEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy (go, 1.5f);
		}

		void OnDestroy ()
		{
				itemCreate.itemCount--;
		}
		
		void OnTriggerEnter2D (Collider2D other)
		{
				if (other.tag == Tags.player) {

						if (currentItemType == ItemType.Iron || currentItemType == ItemType.Wood) {
								int pedalNum = (int)items.itemList [(PedalControl.PedalTypes)currentItemType];
								items.itemList.Remove ((PedalControl.PedalTypes)currentItemType);
								items.itemList.Add ((PedalControl.PedalTypes)currentItemType, pedalNum + capNum);

						} else if (currentItemType == ItemType.SpringShoe) {
								items.springShoe = true;
						}
				
						playPickUpEffect ();
						Destroy (gameObject);
				}
		}

}
