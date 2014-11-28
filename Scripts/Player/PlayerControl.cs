using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
		public bool pedalCreating = false;
		public bool grounded;
		public GameObject ironPedal;
		public GameObject woodPedal;
		public PedalControl.PedalTypes currentPedalType; //当前选择的踏板类型
		private PlayerAttribute pa;
		private Transform myTransform;
		private float moveSpeed = 30f; //玩家着陆后的横向移动速度
		private float onePedalWidth; //单个踏板的宽度
		private float onePedalHeight; //单个踏板的高度
		private Vector3 pedalStart;  //踏板创建的起始位置，也就是点击屏幕的位置
		private float pedalCheckOffset = 0.05f;  //踏板创建开始位置检测偏移，防止检测位置为自身
		private WeatherControll weatherControll; //天气
		private float playerWidth; //玩家模型宽度
		private float playerHeight; //玩家模型高度
		private Items items; //玩家拥有的道具
		private GameObject ground; //地面
		private 

		enum ClickPos
		{  
				Up,  
				Right,  
				Down,
				Left
		} 

		void Awake ()
		{
				pa = GetComponent<PlayerAttribute> ();
				myTransform = transform;
				onePedalWidth = ironPedal.renderer.bounds.size.x;
				onePedalHeight = ironPedal.renderer.bounds.size.y;
				playerWidth = myTransform.gameObject.renderer.bounds.size.x;
				playerHeight = myTransform.gameObject.renderer.bounds.size.y;
				weatherControll = GameObject.FindGameObjectWithTag (Tags.gameController).GetComponent<WeatherControll> ();
				items = GetComponent<Items> ();
				ground = GameObject.FindGameObjectWithTag (Tags.ground);
				grounded = true;
		}

		void Update ()
		{
				Vector3 clickPosition = InputMethod.GetClick ();

				if (clickPosition != InputMethod.nullPosition) {		

						if (!grounded && !pedalCreating) {
								pedalCreating = true;
								pedalStart = clickPosition;
						}

						JumpAndMove (clickPosition);		
						
				}

				Vector3 clickUpPosition = InputMethod.GetClickUp ();

				if (clickUpPosition != InputMethod.nullPosition) {		
						if (pedalCreating && pedalStart != InputMethod.nullPosition) {
								CreatePedal (pedalStart, clickUpPosition);
								pedalStart = InputMethod.nullPosition;
								pedalCreating = false;		
						}
				
				}
				
				WindMove ();
				BoundCheck ();
				FallDrag ();
		}
		
		void FallDrag ()
		{
				if (rigidbody2D.velocity.y < 0) {
						rigidbody2D.drag = 2.5f;
				} else {
						rigidbody2D.drag = 0;
				}
		}

		void CreatePedal (Vector3 start, Vector3 end)
		{
				int pedalCap = (int)items.itemList [currentPedalType];

				float pedalLength = Mathf.Abs (start.x - end.x);
				int createNum = Mathf.Max ((int)(pedalLength / onePedalWidth), 1);
				createNum = Mathf.Min ((int)(pedalLength / onePedalWidth), pedalCap);

				int dir = start.x > end.x ? -1 : 1;

				int createdNum = 0;

				GameObject greateType = null;

				if (currentPedalType == PedalControl.PedalTypes.Iron) {
						greateType = ironPedal;
				} else if (currentPedalType == PedalControl.PedalTypes.Wood) {
						greateType = woodPedal;
				}

				for (int i=1; i<=createNum; i++) {
						if (!Createable (new Vector2 (start.x + (i - 1) * dir * onePedalWidth + dir * pedalCheckOffset, start.y), dir)) {
								break;
						}	

						Instantiate (greateType, new Vector3 (start.x + (i - 1) * dir * onePedalWidth + dir * onePedalWidth / 2, start.y, -1), Quaternion.identity);
						
						createdNum++;
				}
				
				pedalCap -= createdNum;

				items.itemList.Remove (currentPedalType);
				items.itemList.Add (currentPedalType, pedalCap);
		}

		bool Createable (Vector2 checkStart, int dir)
		{
				if (!ClickOnGroundCheck (checkStart) && !SideCheck (new Vector2 (checkStart.x, checkStart.y + onePedalHeight / 2), dir) && !SideCheck (new Vector2 (checkStart.x, checkStart.y - onePedalHeight / 2), dir) && !CreateBoundCheck (new Vector2 (checkStart.x, checkStart.y), dir)) {
						return true;
				} else {
						return false;
				}
		}

		bool ClickOnGroundCheck (Vector2 checkStart)
		{
				//创建踏板的位置不能低于地表高度+一块踏板的高度
				float groundY = ground.renderer.bounds.size.y;
				if (checkStart.y < ground.transform.position.y + groundY / 2 + onePedalHeight) {
						return true;
				}
				return false;
		}

		bool CreateBoundCheck (Vector2 checkStart, int dir)
		{
				Vector3 boundLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
				Vector3 boundRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0));

				if (dir < 0) {
						if (Vector2.Distance (checkStart, new Vector2 (boundLeft.x, checkStart.y)) < (onePedalWidth / 2)) {
								return true;
						}
				} else {
						if (Vector2.Distance (checkStart, new Vector2 (boundRight.x, checkStart.y)) < (onePedalWidth / 2)) {
								return true;
						}
				}

				return false;
		}

		bool SideCheck (Vector2 rayStart, int dir)
		{

				RaycastHit2D hit = Physics2D.Raycast (rayStart, Vector3.right * dir);
				if (hit.transform != null) {
						if (hit.transform.tag == Tags.pedal || hit.transform.tag == Tags.player) {
								if (Vector2.Distance (rayStart, new Vector2 (hit.transform.position.x, hit.transform.position.y)) < (3f / 2f * onePedalWidth)) {
										return true;
								}
						}
				}

				return false;
		}

		void WindMove ()
		{
				if (weatherControll.windLevel != 0) {
						rigidbody2D.AddForce (Vector3.right * weatherControll.windLevel * weatherControll.windPower);
				}			
		}

		void JumpAndMove (Vector3 clickPosition)
		{
				Vector3 jumpDirection = GetJumpDirection (clickPosition);		
				if (grounded) {		
						if (jumpDirection.y > 0) {
								rigidbody2D.AddForce (Vector3.up * jumpDirection.y * pa.jumpAbility);
						}

						if (jumpDirection.y >= 0) {
								rigidbody2D.AddForce (Vector3.right * jumpDirection.x * moveSpeed);			
						}									
				}
		}

		/// <summary>
		/// Bounds the check. 角色触碰左右边界时，给予相反方向弹力
		/// </summary>
		void BoundCheck ()
		{
				Vector3 boundLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
				Vector3 boundRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0));
		
				if (transform.position.x - playerWidth / 2 < boundLeft.x) {
						rigidbody2D.AddForce (Vector3.right * moveSpeed * 0.5f);
				}
				if (transform.position.x + playerWidth / 2 > boundRight.x) {
						rigidbody2D.AddForce (Vector3.left * moveSpeed * 0.5f);
				}
		}

		Vector3 GetJumpDirection (Vector3 clickPosition)
		{
				float x = 0;
				float y = 0;
				if (clickPosition.x > myTransform.position.x + playerWidth / 2) {
						x = 1;
				} else if (clickPosition.x < myTransform.position.x - playerWidth / 2) {
						x = -1;
				}

				if (clickPosition.y > myTransform.position.y + 0.5f) {
						y = 1; //点击位置位于玩家上方某一位置之上，判断可以跳跃
				} else if (clickPosition.y < myTransform.position.y - playerHeight / 2) {
						y = -1;  //点击位置低于玩家位置，不可平移
				}

				return new Vector3 (x, y, 0);

		}
	
		public void ChangePedal (int pedalType)
		{
				currentPedalType = (PedalControl.PedalTypes)pedalType;
		}

}
