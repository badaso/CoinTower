using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public int coinCounter = 1;
	public float baseCameraHeight = 9f;
	public float baseCameraDepth = -200f;
	public float changeCameraHeight;
	public float changeValue = 1.5f;

	private GameObject tmpObject;
	private GameObject tmpGameController;
	private BGController tmpBGScroll;

	void Start(){
		tmpObject = GameObject.Find("HitZone");
		tmpBGScroll = GameObject.Find("GameController").GetComponent<BGController>();
	}

	void LateUpdate(){
		if (coinCounter == 1){
			BaseCameraTransform();
		}

		if (coinCounter < tmpObject.GetComponent<CoinConstructor>().coinCount){
			coinCounter = tmpObject.GetComponent<CoinConstructor>().coinCount;

			changeCameraHeight = coinCounter * changeValue + baseCameraHeight;

			transform.position = new Vector3(0,changeCameraHeight,baseCameraDepth);
			tmpBGScroll.gameObject.SendMessage("BGScroll");
			//print (coinCounter);
			//print (changeCameraHeight);
		}
	}

	public void BaseCameraTransform(){
			transform.position = new Vector3(0,baseCameraHeight,baseCameraDepth);
		}

	public void ResetCoinCounterCamera(){
		coinCounter = 1;
	}
}
