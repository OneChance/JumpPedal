using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
		public float moveSpeed = 20f;
		private GameObject player;
		private float relDistance;
		private PlayerControl playerControl;

		void Awake ()
		{
				player = GameObject.FindGameObjectWithTag (Tags.player);
				relDistance = player.transform.position.y - transform.position.y;
				playerControl = player.GetComponent<PlayerControl> ();
		}

		void Update ()
		{

				float curDistanceAdd = 0;
				float curSpeed = 0;


				if (playerControl.grounded) {
						curDistanceAdd = relDistance;
						curSpeed = moveSpeed / 10f;

				} else {

						Vector3 boundTop = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0));
						Vector3 boundBottom = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));

						curDistanceAdd = (boundTop.y - boundBottom.y) / 2 - player.renderer.bounds.size.y;
						curSpeed = moveSpeed / 10f;

						//curSpeed *= boundRight.y - player.transform.position.y;
				}
				Vector3 newPos = new Vector3 (transform.position.x, player.transform.position.y - curDistanceAdd, transform.position.z);

				transform.position = Vector3.Lerp (transform.position, newPos, Time.deltaTime * curSpeed);
		}

}
