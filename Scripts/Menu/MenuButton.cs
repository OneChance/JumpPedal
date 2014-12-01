using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour
{

		public GameObject nextButton;
		public GameObject preButton;
		private float imageWidth;
		private Transform myCamera;
		private int current;
		
		void Awake ()
		{
				if (nextButton != null) {
						imageWidth = GameObject.Find ("Guide1").renderer.bounds.size.y;
						myCamera = Camera.main.transform;
						current = 0;
				}
		}

		public void StartGame ()
		{
				Application.LoadLevel ("Main");
		}

		public void StartGuide ()
		{
				Application.LoadLevel ("Guide");
		}

		public void toNext ()
		{
				
				if (current == 2) {
						Application.LoadLevel ("Main");
				} else {
						if (current == 0) {
								preButton.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Pre";
						} else if (current == 1) {
								nextButton.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Start";
						}					
						MoveCamera (1);
						current++;	
				}
						
		}

		public void toPre ()
		{
				if (current == 0) {
						Application.LoadLevel ("Start");
				} else {
						if (current == 1) {
								preButton.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Back";
						}		
						MoveCamera (-1);
						current--;	
				}				
		}
	
		void MoveCamera (int direction)
		{
				myCamera.position = new Vector3 (myCamera.position.x + imageWidth * direction, myCamera.position.y, myCamera.position.z);
		}

		public void ExitGame ()
		{
				Debug.Log ("exit");
				Application.Quit ();
		}
}
