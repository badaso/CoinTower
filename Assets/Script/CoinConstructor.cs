using UnityEngine;
using System.Collections;

public class CoinConstructor : MonoBehaviour {
	public GameObject parentCoin;

	public GameObject coin;

	public GameObject coin_01;
	public GameObject coin_02;
	public GameObject coin_03;
	public GameObject coin_04;
	public GameObject coin_05;

	public int coinCount = 1;

	public int randCoinNo = 1;

	public int randWind = 0;
	private bool randWindOn = true;
	private bool coinConstructorWork = true;

	void Update(){
		if (randCoinNo == 1)
			coin = coin_01;
		if (randCoinNo == 2)
			coin = coin_02;
		if (randCoinNo == 3)
			coin = coin_03;
		if (randCoinNo == 4)
			coin = coin_04;
		if (randCoinNo == 5)
			coin = coin_05;
	}

	void OnMouseDown(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (coinConstructorWork == true){
			if (Physics.Raycast(ray, out hit)){
				if (hit.collider.gameObject.name == "HitZone"){
					float mousePositionx = hit.point.x;
					float mousePositiony = this.transform.position.y;

					GameObject Child = Instantiate(coin,new Vector3(mousePositionx,mousePositiony,0),Quaternion.Euler(-90,0,0)) as GameObject;
					Child.transform.parent = parentCoin.transform;

					Child.GetComponent<CoinController>();
					Child.gameObject.SendMessage("StateCoercion", "MOVING");

					randCoinNo = Random.Range(1,6);
				}
			}
		}
	}

	public void ResetCoinCounter(){
		coinCount = 1;
	}

	public void CoinCountMounter(){
		coinCount++;
	}

	public void WorkOn(){
		coinConstructorWork = true;
	}

	public void WorkOff(){
		coinConstructorWork = false;
	}

	public void WindRandom(){
		if (coinCount <= 5){
			if (randWindOn == true){
				randWind = 0;
				randWindOn = false;
			}
		}
		if (coinCount <= 10){
			if (randWindOn == true){
				randWind = Random.Range(4,8)-6;
				randWindOn = false;
			}
		}


		if (randWindOn == true){
			randWind = Random.Range(1,12)-6;
			randWindOn = false;
		}
	}
	public void RandWindOn(){
		randWindOn = true;
	}

	public void ResetRandWind(){
		randWind = 0;
	}
}
