using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BgScroll : MonoBehaviour
{
		private List<Transform> bgs;
		private float skyHeight;
		private float spliceOffset = 0.2f;

		void Awake ()
		{
				bgs = new List<Transform> ();
		
				for (int i=0; i<transform.childCount; i++) {
			
						Transform t = transform.GetChild (i);

						skyHeight = t.gameObject.renderer.bounds.size.y;
			
						if (t.renderer != null) {
								bgs.Add (transform.GetChild (i));	
						}
				}
		
				bgs = bgs.OrderBy (one => one.position.y).ToList ();	


		}

		void Update ()
		{
				ScrollCheck ();
		}

		void ScrollCheck ()
		{
				Transform low = bgs.First ();
				Transform high = bgs.Last ();
				
				Vector3 boundBottom = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
				Vector3 boundTop = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0));	

				if (Mathf.Abs (low.position.y - skyHeight / 2 - boundBottom.y) < 1f) {
						bgs.Remove (high);
						high.position = new Vector3 (high.position.x, low.position.y - skyHeight + spliceOffset, high.position.z);
						bgs.Insert (0, high);
				}
			
				if (Mathf.Abs (high.position.y + skyHeight / 2 - boundTop.y) < 1f) {
						bgs.Remove (low);
						low.position = new Vector3 (low.position.x, high.position.y + skyHeight - spliceOffset, low.position.z);
						bgs.Add (low);
				}	
		}
}
