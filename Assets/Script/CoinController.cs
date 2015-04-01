using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {
	public enum CoinState{
		IDLE,
		MOVING,
		FIXED,
		DISABLE,
	}
	public CoinState state = CoinState.IDLE;

	private GameController tmpGameController;
	private CoinConstructor tmpCoinConstructor;

	private int windRandInt = 0;

	private SoundCoinEffect tmpSoundCoinEffect;

	void Start () {
		tmpGameController = GameObject.Find("GameController").GetComponent<GameController>();
		tmpCoinConstructor = GameObject.Find("HitZone").GetComponent<CoinConstructor>();
		tmpSoundCoinEffect = GetComponent<SoundCoinEffect>();
	}

	void Update () {
		switch(state){
		case CoinState.IDLE:
			//print ("idle");
			StateIdle();
			break;
		case CoinState.MOVING:
			print ("Coin Moving!!!!!!!!!!!");
			StateMoving();
			break;
			
		case CoinState.FIXED:
			//print ("Coin Fixed");
			StateFixed();
			break;	
		case CoinState.DISABLE:
			StateDisable();
			break;
		}
	}

	void OnCollisionEnter(Collision c){
		string layerName = LayerMask.LayerToName(c.gameObject.layer);

		if (state == CoinState.MOVING){
			if (layerName == "Coin"){
				StateCoercion("FIXED");
			}
		}
	}

	public void StateCoercion(string SC){
		if (SC == "IDLE"){
			state = CoinState.IDLE;
		}
		if (SC == "MOVING"){	
			state = CoinState.MOVING;
		}
		if (SC == "FIXED"){
			state = CoinState.FIXED;
		}
		if (SC == "DISABLE"){
			state = CoinState.DISABLE;
		}
	}

	void StateIdle(){
		tmpCoinConstructor.gameObject.SendMessage("WorkOn");
		tmpCoinConstructor.gameObject.SendMessage("WindRandom");
	}

	void StateMoving(){
		tmpCoinConstructor.gameObject.SendMessage("WorkOff");
		windRandInt = tmpCoinConstructor.randWind;
		float nPosY = this.gameObject.transform.position.y;
		float cPosX = this.gameObject.transform.position.x + Time.deltaTime * windRandInt;
		this.gameObject.transform.position = new Vector3 (cPosX,nPosY,0);

		if (tmpSoundCoinEffect.effectSoundNo == 0){
			tmpSoundCoinEffect.CoinEffectSoundChange(1);
			tmpSoundCoinEffect.effectSoundNo = 1;
		}
	}

	void StateFixed(){
		tmpGameController.gameObject.SendMessage("CoinCounterMounterCall");
		tmpCoinConstructor.gameObject.SendMessage("RandWindOn");
		state = CoinState.IDLE;

		if (tmpSoundCoinEffect.effectSoundNo == 1){
			tmpSoundCoinEffect.CoinEffectSoundChange(2);
			tmpSoundCoinEffect.effectSoundNo = 2;
		}
	}

	void StateDisable(){
		Rigidbody tmp = GetComponent<Rigidbody>();
		Destroy(tmp);
		this.GetComponent<Renderer>().enabled = false;
		state = CoinState.IDLE;
	}
}
