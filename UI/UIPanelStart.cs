using UnityEngine;
using System.Collections;

public class UIPanelStart : MonoBehaviour {

	public void StartGame()
	{
        UIController.Instance.CreatePanel ("Login");
		Destroy (gameObject);
	}
}
