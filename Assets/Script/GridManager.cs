using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    //配列の強い奴
    public List<SpriteRenderer> spriteRenderes = new List<SpriteRenderer>();


    public void ClearHighlight() {
        foreach (var spriteRendere in spriteRenderes)
        {
            spriteRendere.color = Color.clear;
            spriteRendere.gameObject.GetComponent<Grid>().isHighlight = false;
        }
        spriteRenderes.Clear();
    }
}
