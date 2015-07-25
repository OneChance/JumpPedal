using UnityEngine;
using System.Collections;

public class HashIDs : MonoBehaviour
{
		public int jumpTypeInt;
		public int fallQuickBool;
		public int moveTypeInt;

		void Awake ()
		{
				jumpTypeInt = Animator.StringToHash ("jumpType");
				fallQuickBool = Animator.StringToHash ("fallQuick");
				moveTypeInt = Animator.StringToHash ("moveType");
		}
}
