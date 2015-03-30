using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDController : MonoBehaviour {
	public GameObject GameController;
	public GameObject CoinIconViewer;

	public TextMesh UICoinCounter;
	public TextMesh UICoinType;
	public TextMesh UIWindValue;
	public GameObject UIHighScore;

	private int coinType;
	private string coinTypeName;
	private int coinCount;
	private int windValue;
	private string sWind;
	private int nHighScore;

	// Use this for initialization
	void Start () {
		//tmpGameController = GameObject.Find("GameController");
		nHighScore = PlayerPrefs.GetInt("High Score");
		UIHighScore.GetComponent<Text>().text = "high : " + nHighScore;

	}
	
	// Update is called once per frame
	void Update () {
		if (GameController.GetComponent<GameController>().playingHudActive == true){
			//바람 노출
			windValue = GameController.GetComponent<GameController>().randWindValue;
			convertWind();
			UIWindValue.text = sWind;

			//코인 카운트 노출
			coinCount = GameController.GetComponent<GameController>().coinCounter;
			UICoinCounter.text = coinCount.ToString("N0");

			//highScore check
			checkUpdate (coinCount);

			//다음 코인 노출
			coinType = GameController.GetComponent<GameController>().coinTypeGenerater;
			//coinTypeName = coinType.name.ToString();
			//UICoinType.text = coinTypeName;

			CoinIconViewer.GetComponent<UICoinIconViewer>().CoinIconChange(coinType);
		}
	}

	void checkUpdate(int nScore)
	{
		if (nHighScore < nScore) 
		{
			nHighScore = nScore;
			PlayerPrefs.SetInt("High Score", nHighScore);
			UIHighScore.GetComponent<Text>().text = "high : " + nHighScore;
		}
	}

	void convertWind(){
		if (windValue == -5){
			sWind = "◀◀◀◀◀";}
		if (windValue == -4){
			sWind = "◀◀◀◀";}
		if (windValue == -3){
			sWind = "◀◀◀";}
		if (windValue == -2){
			sWind = "◀◀";}
		if (windValue == -1){
			sWind = "◀";}
		if (windValue == 0){
			sWind = "-";}
		if (windValue == 1){
			sWind = "▶";}
		if (windValue == 2){
			sWind = "▶▶";}
		if (windValue == 3){
			sWind = "▶▶▶";}
		if (windValue == 4){
			sWind = "▶▶▶▶";}
		if (windValue == 5){
			sWind = "▶▶▶▶▶";}
	}
}
