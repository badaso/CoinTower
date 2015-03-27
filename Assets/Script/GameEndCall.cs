using UnityEngine;
using System.Collections;

public class GameEndCall : MonoBehaviour {

	private GameController tmpGameController;


	// Use this for initialization
	void Start () {
		tmpGameController = GameObject.Find("GameController").GetComponent<GameController>();
	}

	void OnTriggerEnter(Collider c)	{
		if (c.transform.tag == "Coin"){
			int d = (int)c.GetComponent<CoinController>().state;
			print(d);
			if (d == 0){
				tmpGameController.gameObject.SendMessage("StateCoercion", "GAMEEND");
			}
		}
	}
}
