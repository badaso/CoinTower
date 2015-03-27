using UnityEngine;
using System.Collections;

public class UICoinIconViewer : MonoBehaviour {
	public Material CoinIcon01;
	public Material CoinIcon02;
	public Material CoinIcon03;
	public Material CoinIcon04;
	public Material CoinIcon05;

	public void CoinIconChange(int i){
		if (i == 1)
			GetComponent<Renderer>().material = CoinIcon01;
		if (i == 2)
			GetComponent<Renderer>().material = CoinIcon02;
		if (i == 3)
			GetComponent<Renderer>().material = CoinIcon03;
		if (i == 4)
			GetComponent<Renderer>().material = CoinIcon04;
		if (i == 5)
			GetComponent<Renderer>().material = CoinIcon05;
	}
}
