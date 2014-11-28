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
						curDistanceAdd = 0f;
						curSpeed = moveSpeed / 10f;

				} else {
						curDistanceAdd = 3f;
						curSpeed = moveSpeed / 10f;
				}

				Vector3 newPos = new Vector3 (transform.position.x, player.transform.position.y - relDistance - curDistanceAdd, transform.position.z);
				transform.position = Vector3.Lerp (transform.position, newPos, Time.deltaTime * curSpeed);
		}

}
