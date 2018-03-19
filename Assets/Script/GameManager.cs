using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public bool isMoveMode = false;
    public bool isAtkMode = false;
    public int nowPlayerNum = 0;
    public GameObject selectedUnit;
    public Text playerInfo;

    public void ChangeTurn() {
        nowPlayerNum = nowPlayerNum == 0 ? 1 : 0;
        Debug.Log("change turn");
        playerInfo.text = "Player" + nowPlayerNum;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.A) && !isMoveMode)
		{
			isAtkMode = false;
			ChangeTurn();
		}
    }
}
