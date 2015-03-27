using UnityEngine;
using System.Collections;

public class SoundButtonEffect : MonoBehaviour {
	public AudioClip ButtonEffectSound;

	// Use this for initialization
	public void ButtonEffect(){
		GetComponent<AudioSource>().clip = ButtonEffectSound;
		GetComponent<AudioSource>().Play();
	}
}
