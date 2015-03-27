using UnityEngine;
using System.Collections;

public class BGController : MonoBehaviour {

	public GameObject bg01;
	public GameObject bg02;

	public float bgScrollY = 0.5f;

	private float bg01BaseX;
	private float bg01BaseY = 80;
	private float bg01BaseZ;

	private float bg02BaseX;
	private float bg02BaseY = 280;
	private float bg02BaseZ;

	private GameObject tmpCamera;
	private float tmpCameraChangeValue = 1f;
	private float totalScrollValue01 = 1f;
	private float totalScrollValue02 = 1f;

	// Use this for initialization
	void Start () {
		tmpCamera = GameObject.Find("Main Camera");
		tmpCameraChangeValue = tmpCamera.GetComponent<CameraController>().changeValue;

		bg01BaseX = bg01.transform.position.x;
		bg01BaseZ = bg01.transform.position.z;

		bg02BaseX = bg02.transform.position.x;
		bg02BaseZ = bg02.transform.position.z;
	}
	
	// Update is called once per frame
	public void BGScroll () {
		bg01BaseY = bg01.transform.position.y;
		totalScrollValue01 = bg01BaseY - tmpCameraChangeValue * bgScrollY;
		bg01.GetComponent<Transform>().position = new Vector3 (bg01BaseX, totalScrollValue01, bg01BaseZ);

		bg02BaseY = bg02.transform.position.y;
		totalScrollValue02 = bg02BaseY - tmpCameraChangeValue * bgScrollY;
		bg02.GetComponent<Transform>().position = new Vector3 (bg02BaseX, totalScrollValue02, bg02BaseZ);
	}

	public void ResetBGScroll(){
		bg01BaseY = 80;
		bg01.transform.position = new Vector3 (bg01BaseX, bg01BaseY, bg01BaseZ);
		bg02BaseY = 280;
		bg02.transform.position = new Vector3 (bg02BaseX, bg02BaseY, bg02BaseZ);
	}
}
