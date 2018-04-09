using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UISKillCD : MonoBehaviour {

	public Image skillImage;
	public Image skillMask;
	public Text cdText;

	public float _SkillCD;
	public float _CDTime;

	public PlayerController _PlayerContro;
	public int _SkillIndex;
	// Use this for initialization
	void Start () {
		skillImage = GetComponent<Image> ();
		skillMask = transform.Find("Temp").GetComponent<Image> ();
		cdText =  transform.Find("Temp/Text").GetComponent<Text> ();
        _PlayerContro = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        UIEventTrigger.Get (gameObject).onPointerDown += OnDownSkill;

	}
		
	public void Init(int skillindex,PlayerController player){
		_SkillCD = player.Skills [skillindex].SkillCD;
	}

	public void OnDownSkill(PointerEventData data){
		bool cdOver = _PlayerContro.UseSkill (_SkillIndex);
		if (cdOver) {
			StartCoroutine (SkillCDMov());
		}

	}

	IEnumerator SkillCDMov()
	{
		_CDTime = _SkillCD;
		skillImage.raycastTarget = false;
		skillMask.gameObject.SetActive (true);
		while (_CDTime>0) {
			yield return null;
			_CDTime -= Time.deltaTime;
			cdText.text = _CDTime.ToString("F1");
			skillMask.fillAmount = _CDTime / _SkillCD;
		}
		_CDTime = 0;
		cdText.text = "";
		skillImage.raycastTarget = true;
		skillMask.gameObject.SetActive (false);

	}

}
