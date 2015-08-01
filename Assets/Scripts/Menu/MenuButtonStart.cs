using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuButtonStart : MonoBehaviour
{

	private static GameObject sounds;
	public Toggle music;
	public Toggle sound;

	void Start ()
	{
		if (sounds == null) {
			sounds = Instantiate (Resources.Load<GameObject> ("Prefabs/Sounds"));
			DontDestroyOnLoad (sounds);
		}

		music.isOn = SoundManager.music;
		sound.isOn = SoundManager.sound;
	}

	public void StartGame ()
	{
		SoundManager.Select ();
		Application.LoadLevel ("Main");
	}

	public void Guide ()
	{
		SoundManager.Select ();
		Application.LoadLevel ("Guide");
	}

	public void ExitGame ()
	{
		SoundManager.Select ();
		Application.Quit ();
	}

	public void Music ()
	{
		if (music.isOn) {
			SoundManager.music = true;
		} else {
			SoundManager.music = false;
		}

		SoundManager.PlayBg ();
	}

	public void Sound ()
	{
		if (sound.isOn) {
			SoundManager.sound = true;
		} else {
			SoundManager.sound = false;
		}
	}
}
