using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	private static SoundManager instance;

	public AudioClip BGMTitle;
	public AudioClip BGMGamePlaying;
	public AudioClip BGMGameEnd;
	public int bgmNo=0;

	public void BGMChange(int i){
		if (i == 1){
			GetComponent<AudioSource>().clip = BGMTitle;
			GetComponent<AudioSource>().loop = true;
			GetComponent<AudioSource>().Play();
		}
		if (i == 2){
			GetComponent<AudioSource>().clip = BGMGamePlaying;
			GetComponent<AudioSource>().loop = true;
			GetComponent<AudioSource>().Play();
		}
		if (i == 3){
			GetComponent<AudioSource>().clip = BGMGameEnd;
			GetComponent<AudioSource>().loop = false;
			GetComponent<AudioSource>().Play();
		}
	}
} 
