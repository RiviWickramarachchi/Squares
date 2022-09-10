using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Touch : MonoBehaviour
{
    private bool touched;
    private Collider2D coll;
    private SquareBehavior squareBehavior;
    void Start()
    {
        squareBehavior = GetComponent<SquareBehavior>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0) {
            for(int i = 0; i< Input.touchCount; i++) {
                UnityEngine.Touch touch = Input.GetTouch(i);
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                if(touch.phase == TouchPhase.Began) {
                    Collider2D touchedCol = Physics2D.OverlapPoint(touchPos);
                    if(coll == touchedCol) {
                        squareBehavior.SetMoveAllow(false);
                    }
                }
                if(touch.phase == TouchPhase.Ended) {
                    squareBehavior.SetMoveAllow(true);
                }
            }
        }
    }
}
