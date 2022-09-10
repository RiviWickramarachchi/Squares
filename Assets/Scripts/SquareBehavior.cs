using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SquareBehavior : MonoBehaviour
{
    public static event Action OnSquareDestroyed;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float moveSpeed;
    private Vector2 targetPosition;
    private bool moveAllowed;
    private float destroyInTime;

    void Awake() {
        GameManager.DestroySquaresOnGameEnd += DestroyOnGameEnd;
    }
    void Start() {
        targetPosition = GetRandomPosition();
        moveAllowed = true;
    }

    // Update is called once per frame
    void Update() {
        if(moveAllowed) {
            destroyInTime = 0;
            if((Vector2)transform.position != targetPosition) {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition,moveSpeed * Time.deltaTime);
        }
        else {
            targetPosition = GetRandomPosition();
        }
        }
        else {
            destroyInTime += 1 * Time.deltaTime;
            DestroySquare();
        }
    }

    private Vector2 GetRandomPosition() {
        float randX = UnityEngine.Random.Range(minX,maxX);
        float randY = UnityEngine.Random.Range(minY,maxY);
        return new Vector2(randX,randY);
    }

    private void DestroySquare() {
        if(destroyInTime > 3f) {
            OnSquareDestroyed?.Invoke();
            Destroy(this.gameObject);
        }
    }

    public void DestroyOnGameEnd() {
        Destroy(this.gameObject);
    }


    public void SetMoveAllow(bool allowVal) {
        moveAllowed = allowVal;
    }

    void OnDisable() {
         GameManager.DestroySquaresOnGameEnd -= DestroyOnGameEnd;
    }

}
