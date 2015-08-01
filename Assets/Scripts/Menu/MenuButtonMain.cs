using UnityEngine;
using System.Collections;

public class MenuButtonMain : MonoBehaviour
{

	public void Back ()
	{
		SoundManager.Select ();
		Application.LoadLevel ("Start");
	}

	public void Reload ()
	{
		SoundManager.Select ();
		Application.LoadLevel ("Main");
	}
}
