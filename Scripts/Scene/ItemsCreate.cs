using UnityEngine;
using System.Collections;

public class ItemsCreate : MonoBehaviour
{
		private Vector3 boundLeft;
		private Vector3 boundRight;
		private GameObject player;
		public GameObject wood;
		public GameObject iron;
		public GameObject springShoe;
		public int itemCount = 0;
		private int createDirection;
		private float destroyTime ;
		public GameObject currentItem;
		private float cameraHeight;
		private float itemWidth;

		void Awake ()
		{
				player = GameObject.FindGameObjectWithTag (Tags.player);
				boundLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
				boundRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0));
				cameraHeight = boundRight.y - boundLeft.y;
				itemWidth = player.renderer.bounds.size.x;
		}

		void Update ()
		{
				StateCheck ();	

				if (itemCount == 0) {
						CreateItem ();
				}
		}

		void StateCheck ()
		{
				if (player.rigidbody2D.velocity.y < -3.9f) {
						createDirection = -1;	
						//如果当前是急速下落状态，立刻销毁玩家上方的道具，以准备在下方生成补偿
						if (currentItem != null && currentItem.transform.position.y > player.transform.position.y + cameraHeight) {
								Destroy (currentItem);
						}
				} else {
						createDirection = 1;
				}
		}

		void CreateItem ()
		{
				boundLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
				boundRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0));

				

				int itemType = (int)(Random.value * 100 % 3);

				GameObject createPrefab = null;

				switch (itemType) {
				case (int)ItemControl.ItemType.Wood:
						createPrefab = wood;
						break;
				case (int)ItemControl.ItemType.Iron:
						createPrefab = iron;
						break;
				case (int)ItemControl.ItemType.SpringShoe:
						createPrefab = springShoe;
						break;
				}

				Vector3 createPos;		
				//检测创建位置是否有效
				
				do {					
						createPos = GeneratePos ();
						Debug.Log ("re create " + createPos);
				} while(!PosCheck (createPos));

				currentItem = Instantiate (createPrefab, createPos, Quaternion.identity) as GameObject;
				itemCount++;
		}

		Vector3 GeneratePos ()
		{
				float posX = Random.Range (boundLeft.x + itemWidth / 2, boundRight.x - itemWidth / 2);	
				float offSetY = Random.Range (3f, 5f);	
				float posY = boundRight.y + offSetY * createDirection;
				posY = Mathf.Max (posY, 0);
				return new Vector3 (posX, posY, -1);
		}

		bool PosCheck (Vector3 createPos)
		{
				return true;
		}
}
