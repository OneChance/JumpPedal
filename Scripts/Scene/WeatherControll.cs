using UnityEngine;
using System.Collections;

public class WeatherControll : MonoBehaviour
{

		public Weather currentWeather;
		public WindDir windDirection;
		public float coolDown = 60f;
		public float timer = 0;
		public int windLevel;
		public float windPower = 0.5f;

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
