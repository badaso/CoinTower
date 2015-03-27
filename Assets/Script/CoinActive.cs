using UnityEngine;
using System.Collections;

public class CoinActive : MonoBehaviour {

	private CoinController tmpCoinController;

	void Start(){
		tmpCoinController = GameObject.Find("CoinController").GetComponent<CoinController>();

	}

	void OnTriggerEnter(Collider c){
		string layerName = LayerMask.LayerToName(c.gameObject.layer);
		if (layerName == "Coin"){
			c.gameObject.SendMessage("StateCoercion", "DISABLE");	
		}
	}
}
