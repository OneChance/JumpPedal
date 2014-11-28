using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour
{

		private GameObject player;
		private PlayerControl pc;

		void Awake ()
		{
				player = GameObject.FindGameObjectWithTag (Tags.player);
				pc = player.GetComponent<PlayerControl> ();
		}

		void Update ()
		{

		}

		void OnCollisionStay2D (Collision2D other)
		{
				if (other.gameObject.tag == Tags.player) {
						pc.grounded = true;
				}
		}

		void OnCollisionExit2D (Collision2D other)
		{
				if (other.gameObject.tag == Tags.player) {
						pc.grounded = false;
				}
		}
}
