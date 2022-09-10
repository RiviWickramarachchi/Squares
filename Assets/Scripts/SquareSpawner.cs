using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SquareSpawner : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public GenState genState;
    private IEnumerator routine;
    [SerializeField] private float spawnTime = 3f;
    [SerializeField] private GameObject square;
    void Awake() {
        Assert.IsNotNull(square);
        GameManager.SendSignalToSpawner += ChangeGenState;
    }
    void Start()
    {
        routine = SquareGenerator();
        StartGame();
    }
    void Update() {
        if(genState == GenState.Generate) {
            StartCoroutine(routine);
            genState = GenState.Generating;
        }
        if(genState == GenState.Stop) {
            StopCoroutine(routine);
            genState = GenState.Stopped;
        }
    }

    private float GetRandomPositionX() {
        float randX = Random.Range(minX,maxX);
        return randX;
    }
     private float GetRandomPositionY() {
        float randY = Random.Range(minY,maxY);
        return randY;
    }

    public void ChangeGenState() {
        genState = GenState.Stop;
    }

    public void StartGame() {
        for(int i=0; i<3; i++) {
            Instantiate(square, new Vector3(GetRandomPositionX(),GetRandomPositionY(),0), Quaternion.identity);
        }
        genState = GenState.Generate;
    }
    IEnumerator SquareGenerator() {
        Debug.Log(genState);
        while(true) {
            yield return new WaitForSeconds(spawnTime);
            //generate squares
            Instantiate(square, new Vector3(GetRandomPositionX(),GetRandomPositionY(),0), Quaternion.identity);
        }
    }

    void OnDisable() {
        GameManager.SendSignalToSpawner -= ChangeGenState;
    }
}


public enum GenState {
    Generate,
    Generating,
    Stop,
    Stopped
}
