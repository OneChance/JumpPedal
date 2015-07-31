using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

	private AudioSource bgMusic;
	private AudioSource soundsEffect;

	void Start ()
	{
		bgMusic = transform.FindChild ("BgMusic").GetComponent<AudioSource> ();
		soundsEffect = transform.FindChild ("Effect").GetComponent<AudioSource> ();
	}

	public void Select ()
	{
		soundsEffect.clip = Resources.Load<AudioClip> ("Sounds/Effect/pickup");
		soundsEffect.Play ();
	}
}
