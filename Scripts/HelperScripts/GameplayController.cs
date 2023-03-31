using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    public GameObject fruitPickUp;
    public GameObject bombPickUp;

    private float minX = -4.4f, maxX = 4.4f, minY = -2.35f, maxY = 2.35f;
    private float zPos = 5.8f;

    private Text scoreText;
    private int scoreCount = 0;

    private void Awake()
    {
        MakeInstance();
        Invoke("startSpawning", 0.5f);
    }

    private void Start()
    {
        scoreText = GameObject.Find("Score").GetComponent<Text>();
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void startSpawning()
    {
        StartCoroutine(spawnPickup());
    }

    public void cancelSpawning()
    {
        CancelInvoke("startSpawning");
    }

    IEnumerator spawnPickup()
    {
        yield return new WaitForSeconds(Random.Range(1f, 1.5f));
        if(Random.Range(0, 10f) >= 2)
        {
            Instantiate(fruitPickUp, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), zPos), Quaternion.identity);
        }
        else
        {
            Instantiate(bombPickUp, new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), zPos), Quaternion.identity);
        }
        Invoke("startSpawning", 0f);
    }

    public void increaseScore()
    {
        scoreCount++;
        scoreText.text = "Score: " + scoreCount;
    }
}
