using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImage : MonoBehaviour {

    public SpriteRenderer EnemySprite;

    GameObject Player;

    public GameObject EnemyLogPosition;
    

    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(Player.transform.position.y + " " + EnemyLogPosition.transform.position.y);
        if (Player.transform.position.y < EnemyLogPosition.transform.position.y)
        {
            EnemySprite.sortingLayerName = "HigherEnemy";
        }
        else if (Player.transform.position.y > EnemyLogPosition.transform.position.y)
        {
            EnemySprite.sortingLayerName = "LowerEnemy";
        }
    }
}
