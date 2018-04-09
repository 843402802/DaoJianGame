using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIPanelJoystick : MonoBehaviour {

	public PlayerController playerCon;

	public GameObject attackIcon;
	public GameObject[] skillIcon;
	public UIJoystick joystick;

    private 
	// Use this for initialization
	void Start () 
	{
        playerCon = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();

        //技能
        skillIcon[0].GetComponent<UISKillCD>().Init(0, playerCon);
        skillIcon[1].GetComponent<UISKillCD>().Init(1, playerCon);
        skillIcon[2].GetComponent<UISKillCD>().Init(2, playerCon);
        //普通攻击 
        UIEventTrigger.Get(attackIcon).onPointerDown = OnDownAttack;
    }
	
	// Update is called once per frame
	void Update () {
        playerCon._MoveH = joystick.InputAxis.x;
        playerCon._MoveV = joystick.InputAxis.y;
    }

	public void OnDownAttack(PointerEventData data){
		playerCon.UseNormalAttack ();

	}
}
