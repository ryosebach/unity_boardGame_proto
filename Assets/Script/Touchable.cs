using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchable : MonoBehaviour
{
    public Color normalColor;
    public Color selectedColor;
    public Color highlightColor;
    public GameObject board;

    MobManager mobManager;
    GameManager gameManager;
    GridManager gridManager;
    public GameObject manager;

    State state;


    void Awake()
    {
        board = GameObject.FindWithTag("Board");
        manager = GameObject.FindWithTag("Manager");
        mobManager = manager.GetComponent<MobManager>();
        state = GetComponent<State>();
        gameManager = manager.GetComponent<GameManager>();
        gridManager = manager.GetComponent<GridManager>();
    }

    void OnMouseDown()
    {
        if (!gameManager.isMoveMode && !gameManager.isAtkMode)
        {
            if (state == null)
                return;
            if (state.playerNum != gameManager.nowPlayerNum)
                return;

            if (gameObject.CompareTag("Mob"))
            {
                board.GetComponent<MeshRenderer>().material.color = selectedColor;
                DrawMoveableArea();
                gameManager.isMoveMode = true;
                gameManager.selectedUnit = gameObject;
            }

        } else if (gameManager.isMoveMode && gameObject.CompareTag("Target"))
        {
            Debug.Log("select");
            State tempState = gameManager.selectedUnit.GetComponent<State>();
            mobManager.mobs[tempState.x, tempState.y] = null;
            Grid grid = gameObject.GetComponent<Grid>();
            if (!grid.isHighlight)
                return;
            mobManager.mobs[grid.x, grid.y] = gameManager.selectedUnit;
            //gameManager.selectedUnit.transform.position = transform.position + new Vector3(0, 0, -0.1f);
            tempState.prevPos = transform.position + new Vector3(0, 0, -0.1f);
            tempState.x = grid.x;
            tempState.y = grid.y;
            gridManager.ClearHighlight();
            gameManager.isMoveMode = false;
            gameManager.isAtkMode = true;
        } else if (gameManager.isAtkMode && gameObject.CompareTag("Mob")) {
            var playerState = gameManager.selectedUnit.GetComponent<State>();
            if (state.playerNum == gameManager.nowPlayerNum)
                return;
            int difX = state.x - playerState.x;
            int difY = state.y - playerState.y;
            if(difX + difY == 1) {
                state.HP -= playerState.power;
                if (state.HP < 0)
                    state.Death();
            }
            gameManager.isAtkMode = false;
            gameManager.ChangeTurn();
        }
    }

    void DrawMoveableArea()
    {
        gridManager.ClearHighlight();
        int speed = state.speed;
        Debug.Log(speed);
        for (int i = -speed; i <= speed; i++)
        {
            for (int j = -speed; j <= speed; j++)
            {
                if (state.x + i < mobManager.mobs.GetLength(0) && state.x + i >= 0
                    && state.y + j < mobManager.mobs.GetLength(1) && state.y + j >= 0)
                {
                    if (Mathf.Abs(i) + Mathf.Abs(j) <= speed)
                    {
                        if (mobManager.mobs[state.x + i, state.y + j] != null)
                            continue;

                        mobManager.mobsTarget[state.x + i, state.y + j].GetComponent<SpriteRenderer>().color = highlightColor;
                        gridManager.spriteRenderes.Add(mobManager.mobsTarget[state.x + i, state.y + j].GetComponent<SpriteRenderer>());
                        mobManager.mobsTarget[state.x + i, state.y + j].GetComponent<Grid>().isHighlight = true;
                    }
                }
            }
        }
    }
}
