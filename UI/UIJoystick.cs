using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIJoystick : MonoBehaviour {

	public Transform joystickLeft;

	public float offsetRange;
	public Vector2 InputAxis;

	public bool IsUse = false;

	// Use this for initialization
	void Start () 
	{
		//设置按压事件
		UIEventTrigger.Get (joystickLeft.gameObject).onPointerDown += OnDownJoystick;

	}
	/// <summary>
	/// 重置数据
	/// </summary>
	void OnDisable()
	{
		Debug.Log("UIJoystick OnDisable");
		StopAllCoroutines ();
		InputAxis = Vector2.zero;
		IsUse = false;
		joystickLeft.Find ("Header").localPosition = Vector3.zero;
	}


	//方向摇杆
	public void OnDownJoystick(PointerEventData eventData)
	{
		if (Application.platform == RuntimePlatform.WindowsEditor) {
			StartCoroutine (JoystickMov (eventData.pointerEnter.transform.Find ("Header")));
		}else if (Application.platform == RuntimePlatform.Android) {
			for (int i = 0; i < Input.touchCount; i++) 
			{
				var t = Input.GetTouch (i);
				if (t.phase == TouchPhase.Began) 
				{
					if (!IsUse) 
					{
						StartCoroutine (JoystickMov (eventData.pointerEnter.transform.Find ("Header"),t));
					}

					break;
				}
			}
		}

	}

	// 正确的使用touch 在移动端可用 

	IEnumerator JoystickMov(Transform header,Touch touch)
	{
		IsUse = true;

		//使用
		Vector3 offset = Vector3.zero;
		while (touch.phase != TouchPhase.Ended)
		{
			header.position = new Vector3(touch.position.x,touch.position.y,0);

			offset = header.position - header.parent.position;

			if (offset.magnitude > offsetRange) 
			{
				header.localPosition = offset.normalized * offsetRange;
			}

			InputAxis = offset.normalized;
		
			var finger = touch.fingerId;

			yield return null;
			for (int i = 0; i < Input.touchCount; i++)
			{
				var newt = Input.GetTouch(i);
				if (newt.fingerId == finger)
				{
					touch = newt;
				}
			}

		}

		header.localPosition = Vector3.zero;
		InputAxis = Vector2.zero;

		IsUse = false;


	}

	// pc端 控制虚拟摇杆的方法，在移动端上，不可用！！！


	IEnumerator JoystickMov(Transform header)
	{
		IsUse = true;
		Vector3 offset = Vector3.zero;

		while (Input.GetMouseButton(0))
		{
			header.position = Input.mousePosition;
			offset = header.position - header.parent.position;

			if (offset.magnitude > offsetRange) 
			{
				header.localPosition = offset.normalized * offsetRange;
			}
			InputAxis = offset.normalized;
			yield return null;
		}
		header.localPosition = Vector3.zero;
		InputAxis = Vector2.zero;
		IsUse = false;
	}

}
