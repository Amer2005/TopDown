using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {

    public float speed;

    public float stopingDistance;

    private Transform target;

    int distanseX;
    int distanseY;

    Animator EnemyAnimator;
    int walking = 0;

    // Use this for initialization
    void Start () {
        EnemyAnimator = gameObject.GetComponentInChildren(typeof(Animator)) as Animator;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        distanseX = (int)Math.Abs(transform.position.x - target.transform.position.x);
        distanseY = (int)Math.Abs(transform.position.y - target.transform.position.y);
        if (Vector2.Distance(transform.position, target.position) < stopingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if (target.transform.position.y > transform.position.y && target.transform.position.x > transform.position.x)
            {
                //kvadrant 1
                if (distanseX >= distanseY)
                {
                    walking = 2;
                }
                else
                {
                    walking = 3;
                }
            }
            else if (target.transform.position.y > transform.position.y && target.transform.position.x < transform.position.x)
            {
                //kvadrant 2
                if (distanseX >= distanseY)
                {
                    walking = 4;
                }
                else
                {
                    walking = 3;
                }
            }
            else if (target.transform.position.y < transform.position.y && target.transform.position.x < transform.position.x)
            {
                //kvadrant 3
                if (distanseX >= distanseY)
                {
                    walking = 4;
                }
                else
                {
                    walking = 1;
                }
            }
            else if (target.transform.position.y < transform.position.y && target.transform.position.x > transform.position.x)
            {
                //kvadrant 4
                if (distanseX >= distanseY)
                {
                    walking = 2;
                }
                else
                {
                    walking = 1;
                }
            }
        }
        else
        {
            walking = 0;
        }

        EnemyAnimator.SetInteger("Walking", walking);
    }
}
