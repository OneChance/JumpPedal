using UnityEngine;
using System.Collections;

public class PedalControl : MonoBehaviour
{
		public float pedalTime = 8f;
		
		void Awake ()
		{
				Destroy (gameObject, pedalTime);
		}

		void OnDestroy ()
		{

		}
}
