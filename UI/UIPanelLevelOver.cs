using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

/// <summary>
/// 关卡结束界面 
/// </summary>
public class UIPanelLevelOver : MonoBehaviour {

	//奖励物品 
	public List<Item> rewardItems = new List<Item>();
	//物品UI 模板
	public Transform itemTemp;
	public Transform itemGrid;

	//提示 
	public Transform tip;


	// Use this for initialization
	void Start () 
	{
		InitReward ();
	}

	//生成奖励 
	void InitReward(){
		//随机奖励
		var itemDatas = GameController.Instance.ItemDatas;
		int amount = Random.Range (1,6);
		for (int i = 0; i < amount; i++) {
			//Clone 
			var item = itemDatas [Random.Range (0, itemDatas.Count)].Clone();
			//数量随机
			item.Amount = Random.Range(1,item.AmountMax+1);

			rewardItems.Add (item);
		}

		//显示UI
		foreach (var item in rewardItems) {
			var ui = Instantiate (itemTemp);
			ui.SetParent (itemGrid);
			//替换图片
			ui.Find("Icon").GetComponent<Image>().sprite = 
				Resources.Load<Sprite>("Item/"+item.Icon);
			//数量 
			ui.Find("Amount").GetComponent<Text>().text = ""+item.Amount;
			//设置名称为id
			ui.name = ""+item.Id;
			//事件绑定
			UIEventTrigger.Get (ui.gameObject).onPointerDown += OnDownItem;
			UIEventTrigger.Get (ui.gameObject).onPointerUp += OnUpItem;

		}


		//加入玩家背包 

		GameController.Instance.playerItems.AddRange(rewardItems);

		//更新 任务数据 
		foreach (var reward in rewardItems) {
			GameController.Instance.UpdateQuestProgress (QuestType.Collection, reward.Id,reward.Amount);
		}

	}


	//按住，显示tip
	public void OnDownItem(PointerEventData data)
	{
		//显示 
		tip.gameObject.SetActive (true);
		//设置坐标 
		tip.position = data.pointerEnter.transform.position;
		//拿到id
		int id = int.Parse(data.pointerEnter.name);

		Item item = new Item();
		foreach (var t in rewardItems) {
			if (t.Id == id) {
				item = t;
				break;
			}
		}

		//显示UI
		//图标
		tip.Find("Icon").GetComponent<Image>().sprite = 
			Resources.Load<Sprite>("Item/"+item.Icon);
		//名称
		tip.Find("Name").GetComponent<Text>().text = ""+item.Name;
		//价格
		tip.Find("Value").GetComponent<Text>().text = "价值："+item.Value;
		//描述 
		tip.Find("Desc").GetComponent<Text>().text = ""+item.Desc;



	}

	//松开 关闭tip
	public void OnUpItem(PointerEventData data)
	{
		tip.gameObject.SetActive (false);
	}


	// 返回主城 
	public void ClickBackMainCity(){
		GameController.Instance.MainCityInit ();
		Destroy (gameObject);
	}
}
