using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class UIPanelSelectLevel : MonoBehaviour {

	public Transform grid;
	public ScrollRect scrollRect;
	public Transform ButtonLevel;


	public Toggle[] toggleArray;

	float scrollPosX = 0f;

	bool isDrag = false;

	private float targetHorizontalPosition=0;

	public int levelIndex;

	// Use this for initialization
	void Start () {
		//创建24个关卡 
		for (int i = 1; i <= 24; i++) {
			var btn = Instantiate (ButtonLevel);
			btn.SetParent (grid);
			//为关卡信息赋值
			if (i <= GameController.Instance.UnLockedLevel) {
				btn.Find ("Lock").gameObject.SetActive (false);
				btn.Find ("Text").GetComponent<Text> ().text = i.ToString ();
			}

			UIEventTrigger.Get (btn.gameObject).onPointerClick += ClickLevelButton;

		}
	

		//事件的绑定 
		UIEventTrigger.Get (scrollRect.gameObject).onBeginDrag += OnBeginDrag;
		UIEventTrigger.Get (scrollRect.gameObject).onEndDrag += OnEndDrag;


	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isDrag == false) {
			//scrollRect.horizontalNormalizedPosition = targetHorizontalPosition;

			scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition,
				targetHorizontalPosition, Time.deltaTime*10);
		}
	}




	//点击关卡按钮 
	public void ClickLevelButton(PointerEventData eventData)
	{
		if (eventData.pointerEnter.transform.Find("Lock").gameObject.activeSelf) {
			UIPanelNotice.Instance.ShowMessage ("关卡未解锁！！！");
			return;
		}

		string info = eventData.pointerPress.transform.Find ("Text").GetComponent<Text> ().text;
		print (info);
		levelIndex = int.Parse (info);
		UIPanelTip.Instance.ShowConfirm ("是否进入关卡" + info + "?",ClickLevelButtonOk);
	}

	public void ClickLevelButtonOk()
	{
		GameController.Instance.InitLevel (levelIndex);
		Destroy (gameObject);
	}


	public void OnBeginDrag(PointerEventData eventData)
	{
		
		isDrag = true;
	}



	public void OnEndDrag(PointerEventData eventData)
	{
		isDrag = false;

		scrollPosX = scrollRect.horizontalNormalizedPosition;
		int index = 0;
		if (scrollPosX < 0.25f) {
			index = 0;
			targetHorizontalPosition = 0;
		} else if (scrollPosX < 0.75f) {
			index = 1;
			targetHorizontalPosition = 0.5f;
		} else {
			index = 2;
			targetHorizontalPosition = 1;
		}
		toggleArray [index].isOn = true;
		print (index);
	}

	public void ClickClosePanel(){
		Destroy (gameObject);
	}



	public void ClickToggle1(bool ison)
	{
		if (ison) 
		{
			targetHorizontalPosition = 0;
		}
	}
	public void ClickToggle2(bool ison)
	{
		if (ison) {
			targetHorizontalPosition = 0.5f;
		}
	}
	public void ClickToggle3(bool ison)
	{
		if (ison) {
			targetHorizontalPosition = 1;
		}
	}
}
