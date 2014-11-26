using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
		public float moveSpeed = 40f;
		private GameObject player;
		private float relDistance;

		void Awake ()
		{
				player = GameObject.FindGameObjectWithTag (Tags.player);
				relDistance = player.transform.position.y - transform.position.y;
		}

		void Update ()
		{
				Vector3 newPos = new Vector3 (transform.position.x, player.transform.position.y - relDistance, transform.position.z);
				transform.position = Vector3.Lerp (transform.position, newPos, Time.deltaTime * moveSpeed);
		}
}
