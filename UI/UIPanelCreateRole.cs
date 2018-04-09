using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIPanelCreateRole : MonoBehaviour {

	public InputField inputField;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ClickRandomName()
	{
		string[] strs = { "小王", "小明", "小张", "小李", "小强" };
		string name = strs [Random.Range (0, strs.Length)];
		inputField.text = name;
	}

	public void ClickCreate()
	{
		if (inputField.text == "") 
		{
			UIPanelNotice.Instance.ShowMessage ("不能为空");

		}
		else 
		{
			UIPanelNotice.Instance.ShowMessage ("创建成功");
			Destroy (gameObject);

			GameController.Instance.MainCityInit (inputField.text);


		}
	}
}
