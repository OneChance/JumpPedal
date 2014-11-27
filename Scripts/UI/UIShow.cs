using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIShow : MonoBehaviour
{
		public GameObject heightValue;
		private Text heightText;
		private GameObject player;
		private float initOffset;//玩家初始高度矫正值
		private PlayerControl playerControl;
		public GameObject ironButton;
		public GameObject woodButton;

		void Awake ()
		{
				heightText = heightValue.GetComponent<Text> ();
				player = GameObject.FindGameObjectWithTag (Tags.player);
				initOffset = player.transform.position.y - 0;
				playerControl = player.GetComponent<PlayerControl> ();
		}

		void Update ()
		{
				HeightUI ();
				PedalChooseUI ();
		}

		void HeightUI ()
		{
				heightText.text = ((int)(player.transform.position.y - initOffset)) + "";
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
}
