using UnityEngine;
using System.Collections;

using GooglePlayGames;
using UnityEngine.SocialPlatforms;


public class GameController : MonoBehaviour {
	
	public GameObject LobbyPopup;
	public GameObject EndPopup;

	public GameObject playing;
	public GameObject ready;

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

	public GameObject coinViewer;
	private bool bCoinViewerSend = false;

	private BoxCollider bCHitZone;

	private bool bEndScoreSend = false;
	private int bestScore;

	private bool bPlayingHudReset = false;

	private GameObject[] tmpParentCoin = new GameObject[]{};

	private CameraController tmpCamera;
	private CoinConstructor tmpCoinCounter;
	private BGController tmpBGController;
	private SoundManager tmpSoundManager;
	private HUDController tmpHudController;

	// Use this for initialization
	void Start () {
		state = GameState.LOBBY;

		tmpCamera = GameObject.Find("Main Camera").GetComponent<CameraController>();
		tmpHudController = GameObject.Find("HUD").GetComponent<HUDController>();

		tmpCoinCounter = HitZone.GetComponent<CoinConstructor>();
		bCHitZone = HitZone.GetComponent<BoxCollider>();

		tmpBGController = this.GetComponent<BGController>();
		tmpSoundManager = GetComponent<SoundManager>();

		//구글 플레이 초기화
		PlayGamesPlatform.Activate();
		PlayGamesPlatform.DebugLogEnabled = true;
		//구글 플레이 로그인
		Social.localUser.Authenticate((bool success)=>{

		});

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
		bCHitZone.enabled = false;
		LobbyPopup.SetActive(true);
		if (tmpSoundManager.bgmNo == 0){
			tmpSoundManager.BGMChange(1);
			tmpSoundManager.bgmNo = 1;
		}
	}

	void ReadyGame(){
		state = GameState.READY;

		bCHitZone.enabled = true;
		LobbyPopup.SetActive(false);

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

		PlayingHudReset();

	}

	void PlayingGame(){
		PlayingHudReset();

		ready.SetActive(false);
	}

	void EndGame(){
		if (bEndScoreSend == false){
			tmpHudController.gameObject.SendMessage("LastScore", coinCounter);
			bestScore = tmpHudController.GetComponent<HUDController>().nHighScore;
			tmpHudController.gameObject.SendMessage("BestScoreSend", bestScore);
			bEndScoreSend = true;
		}

		bCoinViewerSend = false;
		bPlayingHudReset = false;

		EndPopup.SetActive(true);
		playing.SetActive(false);

		bCHitZone.enabled = false;

		if (tmpSoundManager.bgmNo == 2){
			tmpSoundManager.BGMChange(3);
			tmpSoundManager.bgmNo = 1;
		}
		ready.SetActive(false);
	}

	public void Retry(){
		bEndScoreSend = false;

		state = GameState.RETRY;

		tmpCamera.gameObject.SendMessage("BaseCameraTransform");
		tmpCamera.gameObject.SendMessage("ResetCoinCounterCamera");
		tmpCoinCounter.gameObject.SendMessage("ResetCoinCounter");
		tmpCoinCounter.gameObject.SendMessage("ResetRandWind");
		tmpBGController.gameObject.SendMessage("ResetBGScroll");
		tmpCoinCounter.gameObject.SendMessage("ResetPrevRandCoinNo");

		EndPopup.SetActive(false);
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
		if (bPlayingHudReset == false){
			HitZone.GetComponent<CoinConstructor>().randCoinNo = 1;
			bPlayingHudReset = true;
		}
		if (bCoinViewerSend == false){
			coinViewer.gameObject.SendMessage("CoinViewerChange", 1);
			bCoinViewerSend = true;
		}
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

	public void ShowLeaderboard(){
		Social.ShowLeaderboardUI();
	}

	public void ShowAchievements(){
		Social.ShowAchievementsUI();
	}
}
