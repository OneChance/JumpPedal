using UnityEngine;
using System.Collections;

public class PedalControl : MonoBehaviour
{
		public float pedalTime;
		public PedalTypes pedalType;
		public float SlipSpeed = 0.01f;
		public GameObject boomEffect;
		private WeatherControll weatherControll;
		private float goneSpeed;
		private float goneTimer;
		private float destroyBeforeTime = 0.1f;
		private float destroyBeforeTimer;

		public enum PedalTypes
		{  
				Wood,
				Iron
		} 
		
		void Awake ()
		{		
				weatherControll = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<WeatherControll> ();
				goneSpeed = 1f;
		}

		void Update ()
		{
				PedalTimeLimitCheck ();
				PedalWeatherEffect ();
		}
		
		/// <summary>
		/// 踏板生命周期检测，到期销毁
		/// </summary>
		void PedalTimeLimitCheck ()
		{
				goneTimer += Time.deltaTime * goneSpeed;
				if (goneTimer >= pedalTime) {
						PlayPedalDesEffect ();
						//销毁之前做一个向下移动，触发玩家的OnCollisonExit方法
						transform.position = InputMethod.nullPosition;
						destroyBeforeTimer += Time.deltaTime;
						if (destroyBeforeTimer >= destroyBeforeTime) {
								Destroy (gameObject);
						}	
				} 
		}

		/// <summary>
		/// 天气对踏板的影响
		/// </summary>
		void PedalWeatherEffect ()
		{
				//如果当前天气为HOT，木头踏板消失速度*2
				if (weatherControll.currentWeather == WeatherControll.Weather.Hot && pedalType == PedalTypes.Wood) {
						goneSpeed = 2f;
				} 
		}

		void PlayPedalDesEffect ()
		{
				GameObject go = Instantiate (boomEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy (go, 1.5f);
		}

		void OnCollisionStay2D (Collision2D other)
		{
				//如果当前天气为COLD，玩家站在钢踏板上时，会受到风力影响，出现平移 
				if (other.gameObject.tag == Tags.player && pedalType == PedalTypes.Iron && weatherControll.currentWeather == WeatherControll.Weather.Cold && weatherControll.windLevel != 0) {
						other.transform.Translate (Vector3.right * (int)weatherControll.windDirection * SlipSpeed);
				}
		}
}
