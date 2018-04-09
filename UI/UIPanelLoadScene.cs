using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPanelLoadScene : MonoBehaviour {

	public Slider slider;

	void Start () {
		slider.value = 0;
	}
	//value 0-1
	public void ChangeProgress(float value)
	{
		slider.value = value;
	}
}
