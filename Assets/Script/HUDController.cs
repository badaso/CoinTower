using UnityEngine;
using System.Collections;

public class HUDController : MonoBehaviour {
	public GameObject GameController;
	public GameObject CoinIconViewer;

	public TextMesh UICoinCounter;
	public TextMesh UICoinType;
	public TextMesh UIWindValue;

	private int coinType;
	private string coinTypeName;
	private int coinCount;
	private int windValue;
	private string sWind;

	// Use this for initialization
	void Start () {
		//tmpGameController = GameObject.Find("GameController");
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

			//다음 코인 노출
			coinType = GameController.GetComponent<GameController>().coinTypeGenerater;
			//coinTypeName = coinType.name.ToString();
			//UICoinType.text = coinTypeName;

			CoinIconViewer.GetComponent<UICoinIconViewer>().CoinIconChange(coinType);
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
