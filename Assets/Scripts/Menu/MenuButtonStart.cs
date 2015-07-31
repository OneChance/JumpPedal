using UnityEngine;
using System.Collections;

public class MenuButtonStart : MonoBehaviour
{

	private GameObject sounds;

	void Start(){
		sounds = GameObject.FindGameObjectWithTag ("Sounds");
	}

	public void StartGame ()
	{

		//Application.LoadLevel ("Main");
	}

	public void Guide ()
	{
		sounds.SendMessage ("Select");
		Application.LoadLevel ("Guide");
	}

	public void ExitGame ()
	{
		Application.Quit ();
	}
}
