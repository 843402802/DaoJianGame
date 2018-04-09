using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// UI 提示面板 
/// </summary>
public delegate void TipClickEventDelegate();

public class UIPanelTip : MonoBehaviour {
	
	private static UIPanelTip _instance;

	public static UIPanelTip Instance
	{
		get
		{ 
			if (_instance == null) 
			{
				// Resource
				GameObject panel = Resources.Load("UI/PanelTip") as GameObject;
				var tip = Instantiate (panel);
				tip.transform.SetParent (GameObject.Find ("Canvas").transform);
				RectTransform tempr = tip.GetComponent<RectTransform>();
				tempr.localScale = Vector3.one;
				tempr.localPosition = Vector3.zero;
				tempr.anchorMin = Vector2.zero;
				tempr.anchorMax = Vector2.one;
				tempr.sizeDelta = Vector2.zero; 

				_instance = tip.GetComponent<UIPanelTip> ();
			}
			return _instance;
		
		}
	}

	private Text txt;
	private TipClickEventDelegate funcOk;

	void Awake () 
	{
		_instance = this;
		txt = transform.Find ("Text").GetComponent<Text> ();
	}



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void ShowMessage(string mes = "")
	{
		txt.text = mes;
	}

	public void ShowConfirm(string mes = "",TipClickEventDelegate func = null)
	{
		txt.text = mes;

		funcOk = func;

		transform.Find ("Button_Ok").gameObject.SetActive (true);
		transform.Find ("Button_Canel").gameObject.SetActive (true);
	}

	public void ClickClose()
	{
		Destroy (gameObject);
	}

	public void ClickOk()
	{
		if (funcOk!=null) 
		{
			funcOk();
		}
		ClickClose ();
	}
}
