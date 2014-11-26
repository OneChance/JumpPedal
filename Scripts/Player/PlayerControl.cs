using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
		public bool pedalCreating = false;
		public bool grounded;
		public GameObject pedal;
		public int pedalCap = 10;
		private PlayerAttribute pa;
		private Transform myTransform;
		private float moveSpeed = 30f;
		private static float pedalCreateIncre = 0.1f;
		private float onePedalWidth;
		private float onePedalHeight;
		private Vector3 pedalStart;
		private float pedalCheckOffset = 0.05f;

		enum ClickPos
		{  
				Up,  
				Right,  
				Down,
				Left
		} 

		void Awake ()
		{
				pa = GetComponent<PlayerAttribute> ();
				myTransform = transform;
				onePedalWidth = pedal.renderer.bounds.size.x;
				onePedalHeight = pedal.renderer.bounds.size.y;
		}

		void Update ()
		{
				Vector3 clickPosition = InputMethod.GetClick ();

				if (clickPosition != InputMethod.nullPosition) {		

						if (!grounded && !pedalCreating) {
								pedalCreating = true;
								pedalStart = clickPosition;
						}

						JumpAndMove (clickPosition);		
						
				}

				Vector3 clickUpPosition = InputMethod.GetClickUp ();

				if (clickUpPosition != InputMethod.nullPosition) {		
						if (pedalCreating && pedalStart != InputMethod.nullPosition) {
								CreatePedal (pedalStart, clickUpPosition);
								pedalStart = InputMethod.nullPosition;
								pedalCreating = false;		
						}
				
				}
				
				BoundCheck ();
		}

		void CreatePedal (Vector3 start, Vector3 end)
		{
				float pedalLength = Mathf.Abs (start.x - end.x);
				int createNum = Mathf.Max ((int)(pedalLength / onePedalWidth), 1);
				createNum = Mathf.Min ((int)(pedalLength / onePedalWidth), pedalCap);

				int dir = start.x > end.x ? -1 : 1;

				int createdNum = 0;

				for (int i=1; i<=createNum; i++) {

						if (!Createable (new Vector2 (start.x + (i - 1) * dir * onePedalWidth + dir * pedalCheckOffset, start.y), dir)) {
								break;
						}	

						GameObject one = Instantiate (pedal, new Vector3 (start.x + (i - 1) * dir * onePedalWidth + dir * onePedalWidth / 2, start.y, -1), Quaternion.identity) as GameObject;
						createdNum++;
				}
				
				pedalCap -= createdNum;
		}

		bool Createable (Vector2 checkStart, int dir)
		{
				if (!SideCheck (new Vector2 (checkStart.x, checkStart.y + onePedalHeight / 2), dir) && !SideCheck (new Vector2 (checkStart.x, checkStart.y - onePedalHeight / 2), dir) && !CreateBoundCheck (new Vector2 (checkStart.x, checkStart.y), dir)) {
						return true;
				} else {
						return false;
				}
		}

		bool CreateBoundCheck (Vector2 checkStart, int dir)
		{
				Vector3 boundLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
				Vector3 boundRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0));

				if (dir < 0) {
						if (Vector2.Distance (checkStart, new Vector2 (boundLeft.x, checkStart.y)) < (onePedalWidth/2)) {
								return true;
						}
				} else {
						if (Vector2.Distance (checkStart, new Vector2 (boundRight.x, checkStart.y)) < (onePedalWidth/2)) {
								return true;
						}
				}

				return false;
		}

		bool SideCheck (Vector2 rayStart, int dir)
		{

				RaycastHit2D hit = Physics2D.Raycast (rayStart, Vector3.right * dir);
				if (hit != null && hit.transform != null) {
						if (hit.transform.tag == Tags.pedal) {
								if (Vector2.Distance (rayStart, new Vector2 (hit.transform.position.x, hit.transform.position.y)) < (3f / 2f * onePedalWidth)) {
										return true;
								}
						}
				}

				return false;
		}

		void JumpAndMove (Vector3 clickPosition)
		{
				Vector3 jumpDirection = GetJumpDirection (clickPosition);		
				if (grounded) {		
						if (jumpDirection.y > 0) {
								rigidbody2D.AddForce (Vector3.up * jumpDirection.y * pa.jumpAbility);
								grounded = false;
						}

						rigidbody2D.AddForce (Vector3.right * jumpDirection.x * moveSpeed);
				}
				
		}

		void BoundCheck ()
		{
				Vector3 boundLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
				Vector3 boundRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0));
		
				if (transform.position.x < boundLeft.x) {
						rigidbody2D.AddForce (Vector3.right * moveSpeed * 1.1f);
				}
				if (transform.position.x > boundRight.x) {
						rigidbody2D.AddForce (Vector3.left * moveSpeed * 1.1f);
				}
		}

		Vector3 GetJumpDirection (Vector3 clickPosition)
		{
				float x = 0;
				float y = 0;
				if (clickPosition.x > myTransform.position.x) {
						x = 1;
				} else if (clickPosition.x < myTransform.position.x) {
						x = -1;
				}

				if (clickPosition.y > myTransform.position.y + 0.5f) {
						y = 1;
				}

				return new Vector3 (x, y, 0);

		}

}
