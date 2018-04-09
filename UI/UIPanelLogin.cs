using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AccountInfo // 账号信息
{
	public string Username;
	public string Password;
	public bool IsRemember;

	public AccountInfo(string user,string pass,bool isrem)
	{
		this.Username = user;
		this.Password = pass;
		this.IsRemember = isrem;
	}
}


//登陆界面的UI脚本 
public class UIPanelLogin : MonoBehaviour {

	public InputField usernameField, passwordField;

	public bool isRemember = false;
	// Use this for initialization
	void Start () {
		usernameField = transform.Find ("UserName/InputField").GetComponent<InputField>();
		passwordField = transform.Find ("Password/InputField").GetComponent<InputField>();
		Init ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Init(){
		string data = PlayerPrefs.GetString ("AccountInfo", "");
		if (data != "") {
			AccountInfo ainfo = JsonUtility.FromJson<AccountInfo> (data);
			//显示用户名
			usernameField.text = ainfo.Username;
			//显示密码 
			isRemember = ainfo.IsRemember;
			if (isRemember) {
				passwordField.text = ainfo.Password;
				transform.Find ("Remember").GetComponent<Toggle>().isOn = true;
			}
		}
	}


	public void ClickLogin()
	{
		//拿到 用户名，密码 
		print(usernameField.text);
		print(passwordField.text);
        //是否正确 
        //if (usernameField.text == "admin" && passwordField.text == "123456")
        if (true)
        {
			//登陆成功
			UIPanelNotice.Instance.ShowMessage("登陆成功！！");
			AccountInfo ainfo = new AccountInfo (usernameField.text, passwordField.text, isRemember);
			//将对象转换成json字符串
			string jsonAinfo = JsonUtility.ToJson (ainfo);
			PlayerPrefs.SetString ("AccountInfo", jsonAinfo);

            UIController.Instance.CreatePanel ("CreateRole");
			Destroy (gameObject);
		} else {
			//登陆失败 
			UIPanelNotice.Instance.ShowMessage("用户名或密码错误！");

		}
	}


	public void IsRememberMe(bool boo)
	{
		isRemember = boo;
	}
}
