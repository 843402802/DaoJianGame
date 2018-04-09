using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class UIPanelMain : MonoBehaviour {

	public Transform buttonGrid;

	// Use this for initialization
	void Start () 
	{
		Transform misc = GameObject.FindGameObjectWithTag ("Misc").transform;

		for (int i = 0; i < buttonGrid.childCount; i++) 
		{
			var btn = buttonGrid.GetChild (i);
			UIEventTrigger.Get (btn.gameObject).onPointerClick += OnClickOpenPanel;
			var follow = btn.gameObject.AddComponent<UIFollow> ();
			follow.worldPoint = misc.Find (btn.name);
		}
	}

	public void OnClickOpenPanel(PointerEventData data)
	{
		string name = data.pointerEnter.name;
		print (name);
        UIController.Instance.CreatePanel (name);
	}
	
}
