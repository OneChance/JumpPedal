using UnityEngine;
using System.Collections;

public class InputMethod : MonoBehaviour
{

		private static bool isMP = false;
		public static Vector3 nullPosition = new Vector3 (9999, 9999, 9999);

		public static Vector3 GetClick ()
		{

				Vector3 clickPos = nullPosition;

				if (isMP) {
						if (Input.GetTouch (0).phase == TouchPhase.Began) {
								clickPos = Input.GetTouch (0).position;
						}
				} else {
						if (Input.GetMouseButtonDown (0)) {
								clickPos = Input.mousePosition;
						}
				}
					
				if (clickPos != nullPosition) {
						return toWorldPos (clickPos);
				}

				return nullPosition;
		}

		public static Vector3 GetClickUp ()
		{
		
				Vector3 clickPos = nullPosition;
		
				if (isMP) {
						if (Input.GetTouch (0).phase == TouchPhase.Ended) {
								clickPos = Input.GetTouch (0).position;
						}
				} else {
						if (Input.GetMouseButtonUp (0)) {
								clickPos = Input.mousePosition;
						}
				}
		
				if (clickPos != nullPosition) {
						return toWorldPos (clickPos);
				}
		
				return nullPosition;
		}

		static Vector3 toWorldPos (Vector3 clickPos)
		{
				RaycastHit hit;	
				Ray ray = Camera.main.ScreenPointToRay (clickPos);
				if (Physics.Raycast (ray, out hit)) {
						return hit.point;
				}
				return clickPos;
		}
}
