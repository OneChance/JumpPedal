using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
		public float moveSpeed = 20f;
		private GameObject player;
		private float relDistance;
		private PlayerControl playerControl;
		private Vector3 playerOriPos;

		void Awake ()
		{
				player = GameObject.FindGameObjectWithTag (Tags.player);
				relDistance = player.transform.position.y - transform.position.y;
				playerControl = player.GetComponent<PlayerControl> ();
				playerOriPos = player.transform.position;
		}

		void Update ()
		{
				if (player.transform.position.y > 0 || player.transform.position.y < playerOriPos.y) {
						float curDistanceAdd = relDistance;
						float lerpSpeed = Time.deltaTime * 5f;
			
						if (!playerControl.grounded) {
								Vector3 boundTop = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0));
								Vector3 boundBottom = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
								curDistanceAdd = (boundTop.y - boundBottom.y) / 2 - playerControl.playerHeight;
								lerpSpeed = Time.deltaTime * 10f;						
						} 
						Vector3 newPos = new Vector3 (transform.position.x, player.transform.position.y - curDistanceAdd, transform.position.z);
			
						transform.position = Vector3.Lerp (transform.position, newPos, lerpSpeed);
				}
			
		}

}
