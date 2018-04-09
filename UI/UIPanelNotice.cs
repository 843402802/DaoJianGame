using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPanelNotice : MonoBehaviour {
	
	private static UIPanelNotice _instance;

	public static UIPanelNotice Instance
	{
		get
		{ 
			if (_instance == null) 
			{
				// Resource
				GameObject panel = Resources.Load("UI/PanelNotice") as GameObject;
				var tip = Instantiate (panel);
				tip.transform.SetParent (GameObject.Find ("Canvas").transform);
				RectTransform tempr = tip.GetComponent<RectTransform>();
				tempr.localScale = Vector3.one;
				tempr.localPosition = Vector3.zero;
				tempr.anchorMin = Vector2.zero;
				tempr.anchorMax = Vector2.one;
				tempr.sizeDelta = Vector2.zero; 

				_instance = tip.GetComponent<UIPanelNotice> ();
			}
			return _instance;

		}
	}

	private Text txt;

	private float resetTime = 1f;
	private float _time;

	void Awake () 
	{
		_instance = this;
		txt = transform.Find ("Text").GetComponent<Text> ();
	}

	void Update()
	{
		_time += Time.deltaTime;
		if (_time>resetTime) {
			Destroy (gameObject);
		}
	}

	public void ShowMessage(string mes = "")
	{
		txt.text = mes;
		_time = 0;
	}
		
}
