using UnityEngine;
using System.Collections;

public class PlayerAttribute : MonoBehaviour
{

		public float health = 100f;
		public float jumpAbility;
		private Items items;
		public float JumpAbilityNormal;
		public float itemsTimer;

		void Awake ()
		{
				jumpAbility = JumpAbilityNormal;
				items = GetComponent<Items> ();
		}

		void Update ()
		{
				if (items.springShoe) {
						if (jumpAbility == JumpAbilityNormal) {
								jumpAbility *= items.springShoeBounce;			
								itemsTimer = 0f;
						}
						itemsTimer += Time.deltaTime;
				}

				if (itemsTimer >= items.itemTime) {
						ResetAttributes ();
				}
		}

		void ResetAttributes ()
		{
				items.springShoe = false;
				jumpAbility = JumpAbilityNormal;
		}
}
