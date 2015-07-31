using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuButtonGuide : MonoBehaviour
{
	
	private string[] guides = new string[]{"Guide1", "Guide2"};
	private int index = 0;
	private GameObject bg;
	
	void Start ()
	{
		bg = GameObject.FindGameObjectWithTag ("Bg");
	}
	
	public void toNext ()
	{			
		index++;

		if (index > 1) {
			Application.LoadLevel ("Main");
		} else {
			bg.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Images/Menu/" + guides [index]);
		}
		
	}
	
	public void toPre ()
	{
		index--;

		if (index < 0) {
			Application.LoadLevel ("Start");
		} else {
			bg.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Images/Menu/" + guides [index]);
		}				
	}
}
