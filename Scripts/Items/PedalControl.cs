using UnityEngine;
using System.Collections;

public class PedalControl : MonoBehaviour
{
		public float pedalTime = 8f;
		public PedalTypes pedalType;
		public float SlipSpeed = 0.01f;
		private WeatherControll weatherControll;


		public enum PedalTypes
		{  
				Iron,
				Wood
		} 
		
		void Awake ()
		{
				Destroy (gameObject, pedalTime);
				weatherControll = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<WeatherControll> ();
		}

		void OnDestroy ()
		{

		}

		void OnCollisionStay2D (Collision2D other)
		{
				if (other.gameObject.tag == Tags.player && pedalType == PedalTypes.Iron && weatherControll.currentWeather == WeatherControll.Weather.Cold && weatherControll.windLevel != 0) {
						other.transform.Translate (Vector3.right * (int)weatherControll.windDirection * SlipSpeed);
				}
		}
}
