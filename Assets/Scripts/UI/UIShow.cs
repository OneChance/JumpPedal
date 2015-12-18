using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIShow : MonoBehaviour
{
	public Text heightText;
	public Text maxHeightText;
	private GameObject player;
	private float initOffset;
	//玩家初始高度矫正值
	private PlayerControl playerControl;
	public GameObject ironButton;
	public GameObject woodButton;
	public GameObject ironNumComp;
	public GameObject woodNumComp;
	public GameObject weatherComp;
	private WeatherControll weatherControl;
	private Items items;
	public GameObject windComp;
	public Sprite weatherNormal;
	public Sprite weatherHot;
	public Sprite weatherCold;
	public Sprite none;
	public Sprite windLeft;
	public Sprite windRight;
	private int max = 0;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag (Tags.player);
		initOffset = player.transform.position.y - 0;
		playerControl = player.GetComponent<PlayerControl> ();
		items = player.GetComponent<Items> ();
		weatherControl = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<WeatherControll> ();

		//加载本地存储的分数
	}

	void Update ()
	{
		HeightUI ();
		PedalChooseUI ();
		PedalNumUI ();
		WeatherUI ();
	}

	void HeightUI ()
	{
		int currentHeight = (int)(player.transform.position.y - initOffset);
		heightText.text = currentHeight + "";
		max = Mathf.Max (max, currentHeight);	
		maxHeightText.text = max + "";
	}

	void PedalChooseUI ()
	{				
		if (playerControl.currentPedalType == PedalControl.PedalTypes.Iron) {
			ironButton.GetComponent<RectTransform> ().localScale = new Vector3 (1.2f, 1.2f, 1f);
		} else {
			ironButton.GetComponent<RectTransform> ().localScale = new Vector3 (1f, 1f, 1f);
		}
			
		if (playerControl.currentPedalType == PedalControl.PedalTypes.Wood) {
			woodButton.GetComponent<RectTransform> ().localScale = new Vector3 (1.2f, 1.2f, 1f);
		} else {
			woodButton.GetComponent<RectTransform> ().localScale = new Vector3 (1f, 1f, 1f);
		}	
	}

	void PedalNumUI ()
	{
		ironNumComp.GetComponent<Text> ().text = items.itemList [PedalControl.PedalTypes.Iron].ToString ();
		woodNumComp.GetComponent<Text> ().text = items.itemList [PedalControl.PedalTypes.Wood].ToString ();
	}

	void WeatherUI ()
	{

		Sprite weatherImage = null;

		if (weatherControl.currentWeather == WeatherControll.Weather.Cold) {
			weatherImage = weatherCold;
		} else if (weatherControl.currentWeather == WeatherControll.Weather.Hot) {
			weatherImage = weatherHot;
		} else if (weatherControl.currentWeather == WeatherControll.Weather.Normal) {
			weatherImage = weatherNormal;
		}

		weatherComp.GetComponent<Image> ().sprite = weatherImage;

		//风向
		Sprite windImage = null;
		if (weatherControl.windDirection == WeatherControll.WindDir.Left) {
			windImage = windLeft;
		} else if (weatherControl.windDirection == WeatherControll.WindDir.Right) {
			windImage = windRight;
		} else {
			windImage = none;
		}	
		windComp.GetComponent<Image> ().sprite = windImage;
	}
}
