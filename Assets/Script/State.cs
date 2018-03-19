using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour {
	public int maxHP = 1;
	public int HP = 1;
	public int power = 1;
	public int speed = 1;
	public int cost = 1;
	public string mobName;
	public int playerNum;
	public MobManager.MobType type = MobManager.MobType.none;
    public int x, y;



    //ここ！
    public Vector3 prevPos;
    //ここ！
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, prevPos, 3f * Time.deltaTime);
	}

    public void Death() {
        if(type == MobManager.MobType.king) {
            int winP = playerNum == 1 ? 0 : 1;
            Debug.Log("Win Player is " + winP);
        }
        Destroy(gameObject);
    }

	public void ChangeSprite(){
		gameObject.GetComponent<SpriteRenderer> ().flipY = playerNum == 1 ? true : false;
	}
	public void InitState(){
		if (type == MobManager.MobType.king) {
			King king = new King ();
			maxHP = king.maxHP;
            HP = king.maxHP;
			power = king.power;
			speed = king.speed;
			cost = king.cost;
		} else if (type == MobManager.MobType.soldier) {
			Soldier soldier = new Soldier ();
			maxHP = soldier.maxHP;
			power = soldier.power;
			speed = soldier.speed;
			cost = soldier.cost;
		}
	}
}
public class King {
	public int maxHP = 5;
	public int power = 2;
	public int speed = 2;
	public int cost = 0;
}

public class Soldier {
	public int maxHP = 2;
	public int power = 2;
	public int speed = 1;
	public int cost = 1;
}