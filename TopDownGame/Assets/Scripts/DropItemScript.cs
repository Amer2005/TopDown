using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropItemScript : MonoBehaviour {

    private Transform target;
    private Image imageClicked;
    public SpriteRenderer dropSprite;

    public static bool isStartFinishedDrop = false;

    // Use this for initialization
    void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        imageClicked = GameObject.FindGameObjectWithTag("InventoryFollow").GetComponent<Image>();

        dropSprite.sprite = imageClicked.sprite;

        transform.position = new Vector2 (target.transform.position.x, target.transform.position.y);

        dropSprite = gameObject.GetComponentInChildren(typeof(SpriteRenderer)) as SpriteRenderer;

        isStartFinishedDrop = true;
    }
	
	// Update is called once per frame
	void Update () {

	}
}
