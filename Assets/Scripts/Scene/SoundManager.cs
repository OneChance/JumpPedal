using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

	private static AudioSource bgMusic;
	private static AudioSource soundsEffect;
	private static AudioSource bgEffect;
	public static bool music = true;
	public static bool sound = true;

	void Start ()
	{
		bgMusic = transform.FindChild ("BgMusic").GetComponent<AudioSource> ();
		soundsEffect = transform.FindChild ("ActEffect").GetComponent<AudioSource> ();
		bgEffect = transform.FindChild ("BgEffect").GetComponent<AudioSource> ();


	}

	public static void PlayBg ()
	{
		if (music) {
			bgMusic.clip = Resources.Load<AudioClip> ("Sounds/Music/BgMusic");
			bgMusic.Play ();
		} else {
			bgMusic.Stop ();
		}
	}

	public static void Select ()
	{
		if (sound) {
			soundsEffect.clip = Resources.Load<AudioClip> ("Sounds/Effect/Select");
			soundsEffect.Play ();
		}
	}

	public static void PickUp ()
	{
		if (sound) {
			soundsEffect.clip = Resources.Load<AudioClip> ("Sounds/Effect/PickUp");
			soundsEffect.Play ();
		}
	}

	public static void Jump ()
	{
		soundsEffect.clip = Resources.Load<AudioClip> ("Sounds/Effect/Jump");
		soundsEffect.Play ();
	}

	public static void RocketJump ()
	{
		if (sound) {
			soundsEffect.clip = Resources.Load<AudioClip> ("Sounds/Effect/RocketJump");
			soundsEffect.Play ();
		}
	}

	public static void Fall (PedalControl.PedalTypes p)
	{
		if (sound) {
			soundsEffect.clip = Resources.Load<AudioClip> ("Sounds/Effect/" + p + "Fall");
			soundsEffect.Play ();
		}
	}

	public static void Walk (PedalControl.PedalTypes p)
	{
		if (sound) {
			soundsEffect.clip = Resources.Load<AudioClip> ("Sounds/Effect/" + p + "Walk");
			soundsEffect.Play ();
		}
	}

	public static void Wind (int i)
	{
		if (sound) {
			bgEffect.clip = Resources.Load<AudioClip> ("Sounds/Effect/Wind" + (i));
			bgEffect.Play ();
		}
	}

	public static void StopBgEffect ()
	{
		if (sound) {
			bgEffect.Stop ();
		}
	}
}
