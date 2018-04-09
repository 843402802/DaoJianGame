using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class UIPanelQuest : MonoBehaviour {

	//模版
	public Transform questTemp;
	public Transform grid;

	private List<Quest> playerQuests;

	// Use this for initialization
	void Start () 
	{
		playerQuests = GameController.Instance.playerQuests;

		InitQuestList ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	/// <summary>
	/// 创建任务列表的UI
	/// </summary>
	public void InitQuestList()
	{
		
		foreach (var quest in playerQuests) {
			CreateQuestUI (quest);
		}

	}

	//创建单条 任务
	void CreateQuestUI(Quest quest )
	{
		var ui = Instantiate (questTemp);
		ui.SetParent (grid);
		//改名字 
		ui.name = ""+quest.Id;
		//任务名称
		ui.Find("BG/Text_Name").GetComponent<Text>().text = quest.Name;
		//描述
		ui.Find("BG/Text_Content").GetComponent<Text>().text = quest.Desc;

		//Test
		//quest.CurrentProgress = quest.TotalProgress;

		//进度
		if (quest.CurrentProgress < quest.TotalProgress) {
			ui.Find ("BG/Text_Progress").GetComponent<Text> ().text = 
				quest.CurrentProgress + "/" + quest.TotalProgress;
		} else {
			ui.Find ("BG/ButtonFinish").gameObject.SetActive (true);
			UIEventTrigger.Get (ui.Find ("BG/ButtonFinish").gameObject).onPointerClick = OnClickFinishButton;

		}
	}


	//点击完成 
	public void OnClickFinishButton(PointerEventData eventData)
	{
		Transform ui = eventData.pointerEnter.transform.parent.parent;
		int id = int.Parse (ui.name);

		Quest quest = new Quest ();
		foreach (var q in playerQuests) {
			if (q.Id == id) {
				quest = q;
			}
		}
		UIPanelNotice.Instance.ShowMessage (quest.RewardMessage);
		Destroy (ui.gameObject);

		// 更新 player的任务列表  新增 新的任务条目
		playerQuests.Remove(quest);
		Quest newQuest = new Quest ();
		foreach (var q in GameController.Instance.QuestDatas) {
			if (q.PreId == quest.Id) {
				newQuest = q.Clone ();
				playerQuests.Add (newQuest);
				//增加UI
				CreateQuestUI(newQuest);
			}
		}


	}

		
	public void ClickClosePanel()
	{
		Destroy (gameObject);
	}
}
