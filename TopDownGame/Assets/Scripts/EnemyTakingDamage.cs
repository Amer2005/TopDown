using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakingDamage : MonoBehaviour {

    int playerLooking;
    bool canBeHitDown = false;
    bool canBeHitLeft = false;
    bool canBeHitUp = false;
    bool canBeHitRight = false;

    // Use this for initialization
    void Start () {
        playerLooking = Player_Script.looking;
    }
	
	// Update is called once per frame
	void Update () {
        playerLooking = Player_Script.looking;
        /*
         looking  
         1 = s
         2 = d
         3 = w
         4 = a
         */
        if (playerLooking == 1 && canBeHitDown && Input.GetMouseButtonDown(0))
        {
            damageEnemy();
        }
        if (playerLooking == 2 && canBeHitRight && Input.GetMouseButtonDown(0))
        {
            damageEnemy();
        }
        if (playerLooking == 3 && canBeHitUp && Input.GetMouseButtonDown(0))
        {
            damageEnemy();
        }
        if (playerLooking == 4 && canBeHitLeft && Input.GetMouseButtonDown(0))
        {
            damageEnemy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DownSword")
        {
            canBeHitDown = true;
        }
        if (collision.gameObject.tag == "LeftSword")
        {
            canBeHitLeft = true;
        }
        if (collision.gameObject.tag == "UpSword")
        {
            canBeHitUp = true;
        }
        if (collision.gameObject.tag == "RightSword")
        {
            canBeHitRight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DownSword")
        {
            canBeHitDown = false;
        }
        if (collision.gameObject.tag == "LeftSword")
        {
            canBeHitLeft = false;
        }
        if (collision.gameObject.tag == "UpSword")
        {
            canBeHitUp = false;
        }
        if (collision.gameObject.tag == "RightSword")
        {
            canBeHitRight = false;
        }
    }

    void damageEnemy()
    {
        Debug.Log("EnemyHit");
    }
}
