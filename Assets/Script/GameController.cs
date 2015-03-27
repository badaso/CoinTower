using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject LobbyPopup;
	public GameObject EndPopup;

	public GameObject Lobby;
	public GameObject playing;
	public GameObject ready;
	public GameObject gameOver;

	public GameObject HitZone;
	public int coinTypeGenerater;
	public GameObject baseCoin;

	public int coinCounter;
	public int randWindValue;

	public bool playingHudActive = false;

	public enum GameState{
		LOBBY,
		READY,
		PLAYING,
		GAMEEND,
		RETRY,
	}
	public GameState state;

	private BoxCollider bCHitZone;

	private GameObject[] tmpParentCoin = new GameObject[]{};

	private CameraController tmpCamera;
	private CoinConstructor tmpCoinCounter;
	private BGController tmpBGController;
	private SoundManager tmpSoundManager;

	// Use this for initialization
	void Start () {
		state = GameState.LOBBY;

		tmpCamera = GameObject.Find("Main Camera").GetComponent<CameraController>();

		tmpCoinCounter = HitZone.GetComponent<CoinConstructor>();
		bCHitZone = HitZone.GetComponent<BoxCollider>();

		tmpBGController = this.GetComponent<BGController>();
		tmpSoundManager = GetComponent<SoundManager>();

	}
	
	// Update is called once per frame
	void Update () {
		switch(state){
		case GameState.LOBBY:
			LobbyMenu();
			break;

		case GameState.READY:
			ReadyGame();
			break;

		case GameState.PLAYING:
			PlayingGame();
		break;

		case GameState.GAMEEND:
			EndGame();
		break;

		case GameState.RETRY:
			Retry();
		break;
		}
	}

	void OnTriggerEnter(Collider c)	{
		if (c.transform.tag == "Coin"){
			state = GameState.GAMEEND;
		}
	}

	void LobbyMenu(){
		Lobby.SetActive(true);
		bCHitZone.enabled = false;
		LobbyPopup.SetActive(true);
		if (tmpSoundManager.bgmNo == 0){
			tmpSoundManager.BGMChange(1);
			tmpSoundManager.bgmNo = 1;
		}
	}

	void ReadyGame(){
		state = GameState.READY;

		Lobby.SetActive(false);
		bCHitZone.enabled = true;
		LobbyPopup.SetActive(false);

		PlayingHudReset();

		ready.SetActive(true);
		baseCoin.SetActive(true);
		playing.SetActive(true);
		playingHudActive = true;
		bCHitZone.enabled = true;

		if (HitZone.GetComponent<CoinConstructor>().coinCount >= 2){
			state = GameState.PLAYING;
		}

		if (tmpSoundManager.bgmNo == 1){
			tmpSoundManager.BGMChange(2);
			tmpSoundManager.bgmNo = 2;
		}
	}

	void PlayingGame(){
		PlayingHudReset();
		ready.SetActive(false);
	}

	void EndGame(){
		EndPopup.SetActive(true);
		gameOver.SetActive(true);
		playing.SetActive(false);

		bCHitZone.enabled = false;

		if (tmpSoundManager.bgmNo == 2){
			tmpSoundManager.BGMChange(3);
			tmpSoundManager.bgmNo = 1;
		}
	}

	public void Retry(){
		state = GameState.RETRY;

		tmpCamera.gameObject.SendMessage("BaseCameraTransform");
		tmpCamera.gameObject.SendMessage("ResetCoinCounterCamera");
		tmpCoinCounter.gameObject.SendMessage("ResetCoinCounter");
		tmpCoinCounter.gameObject.SendMessage("ResetRandWind");
		tmpBGController.gameObject.SendMessage("ResetBGScroll");

		EndPopup.SetActive(false);
		gameOver.SetActive(false);
		playing.SetActive(true);
		bCHitZone.enabled = true;

		tmpParentCoin = GameObject.FindGameObjectsWithTag("Coin");
		for(int i=0;i<tmpParentCoin.Length;i++)
		{
			Destroy(tmpParentCoin[i]);
		}

		state = GameState.PLAYING;

		if (tmpSoundManager.bgmNo == 1){
			tmpSoundManager.BGMChange(2);
			tmpSoundManager.bgmNo = 2;
		}
	}

	public void CoinCounterMounterCall(){
		tmpCoinCounter.gameObject.SendMessage("CoinCountMounter");
	}

	void PlayingHudReset(){
		coinCounter = HitZone.GetComponent<CoinConstructor>().coinCount;
		randWindValue = HitZone.GetComponent<CoinConstructor>().randWind;
		coinTypeGenerater = HitZone.GetComponent<CoinConstructor>().randCoinNo;
	}

	public void StateCoercion(string SC){
		if (SC == "LOBBY"){
			state = GameState.LOBBY;
		}
		if (SC == "PLAYING"){	
			state = GameState.PLAYING;
		}
		if (SC == "READY"){
			state = GameState.READY;
		}
		if (SC == "RETRY"){
			state = GameState.RETRY;
		}
		if (SC == "GAMEEND"){
			state = GameState.GAMEEND;
		}
	}
}
