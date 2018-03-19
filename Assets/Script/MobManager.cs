using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    public GameObject[,] mobsTarget = new GameObject[11, 11];
    public GameObject[,] mobs = new GameObject[11, 11];
    public GameObject[] mobUnit;
    // Use this for initialization
    void Start()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("Target"))
        {
            string[] nums = obj.name.Split('-');
            mobsTarget[int.Parse(nums[0]), int.Parse(nums[1])] = obj;
			obj.GetComponent<Grid>().x = int.Parse(nums[0]);
			obj.GetComponent<Grid>().y = int.Parse(nums[1]);
        }

        mobs[5, 0] = Instantiate(mobUnit[0], mobsTarget[5, 0].transform.position, Quaternion.identity);
        mobs[5, 0].transform.position += new Vector3(0, 0, -0.1f);
        var state = mobs[5, 0].GetComponent<State>();
		state.prevPos = mobsTarget[5, 0].transform.position + new Vector3(0, 0, -0.1f);
		state.playerNum = 0;
        state.x = 5;
        state.y = 0;

		mobs[4, 0] = Instantiate(mobUnit[1], mobsTarget[4, 0].transform.position, Quaternion.identity);
		mobs[4, 0].transform.position += new Vector3(0, 0, -0.1f);
		state = mobs[4, 0].GetComponent<State>();
		state.prevPos = mobsTarget[4, 0].transform.position + new Vector3(0, 0, -0.1f);
		state.playerNum = 0;
		state.x = 4;
		state.y = 0;

        mobs[5, 10] = Instantiate(mobUnit[0], mobsTarget[5, 10].transform.position, Quaternion.identity);
        mobs[5, 10].transform.position += new Vector3(0, 0, -0.1f);
        state = mobs[5, 10].GetComponent<State>();
        state.prevPos = mobsTarget[5, 10].transform.position + new Vector3(0, 0, -0.1f);
		state.playerNum = 1;
        state.x = 5;
        state.y = 10;

        for (int i = 0; i < 11; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                if (mobs[i, j] != null)
                {
                    state = mobs[i, j].GetComponent<State>();
                    if (state != null)
                    {
                        state.ChangeSprite();
                        state.InitState();
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public enum MobType
    {
        king,
        soldier,
        none,
    }
}
