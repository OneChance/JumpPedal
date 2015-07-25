using UnityEngine;
using System.Collections;

public class WeatherControll : MonoBehaviour
{

		public Weather currentWeather;//当前天气
		public WindDir windDirection; //风向
		public float coolDown = 60f; //天气转变间隔
		public float timer = 0;
		public int windLevel; //风力级别
		public float windPower = 0.5f; //风力
 
		public enum Weather
		{  
				Normal,
				Hot,  
				Cold
		}

		public enum WindDir
		{  
				Normal = 0,
				Left = -1,  
				Right = 1
		} 

		void Awake ()
		{
				currentWeather = Weather.Normal;
				windDirection = 0;
				windLevel = 0;
		}

		void Update ()
		{
				timer += Time.deltaTime;
				if (timer >= coolDown) {
						ChangeWeather ();
						timer = 0;
				}
		}

		void ChangeWeather ()
		{
				currentWeather = (Weather)(int)((Random.value * 100) % 3);
				windDirection = (WindDir)(int)((Random.value * 100) % 3 - 1);
				if (windDirection != WindDir.Normal) {
						windLevel = (int)((Random.value * 100) % 3) + 1;
				} else {
						windLevel = 0;
				}
		}

}
