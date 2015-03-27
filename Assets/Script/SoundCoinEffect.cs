using UnityEngine;
using System.Collections;

public class SoundCoinEffect : MonoBehaviour {
	public AudioClip MovingSound;
	public AudioClip FixSound;

	public int effectSoundNo = 0;

	// Use this for initialization
	public void CoinEffectSoundChange(int i){
		if (i == 1){
			GetComponent<AudioSource>().clip = MovingSound;
			GetComponent<AudioSource>().Play();
		}
		if (i == 2){
			GetComponent<AudioSource>().clip = FixSound;
			GetComponent<AudioSource>().Play();
		}
	}
}
