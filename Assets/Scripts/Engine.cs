using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Engine : MonoBehaviour
{
    public float maxX;
    public float maxZ;
    public Transform boardTransform;
    public float waterLevel;
    public float immersionLevel;
    public GameObject woodLog;
    public GameObject speedUpPill;
    public GameObject coin;
    public GameObject armor;
    public GameObject starUp;
    public List<GameObject> bonuses;
    public Score score;

    private int difficultLevel;
    private float interval;
    private float currentInterval;
    System.Random rnd = new System.Random();


    void Start()
    {
        currentInterval = interval;
        boardTransform = GameObject.FindGameObjectWithTag("Board").GetComponent<Transform>();
        score = FindObjectOfType<Score>();
        maxX = 8;
        maxZ = boardTransform.localScale.z;
        waterLevel = -1;
        immersionLevel = 2f;
        interval = 1;
        difficultLevel = 5;
        PlayerPrefs.SetInt("score", 0);
    }

    void Update()
    {

        if (currentInterval > 0)
        {
            currentInterval -= Time.deltaTime;
        }
        else if (currentInterval <= 0)
        {
            if (ResultOfTheDraw( 40 + difficultLevel * (int)(Time.time / 10)) )
            {
                SpawnObject(woodLog);
            }
            if(ResultOfTheDraw(5))
            {
                SpawnObject(speedUpPill);
            }
            if(ResultOfTheDraw( (int)(25 + difficultLevel * (int)(Time.time / 10)) ))
            {
                SpawnObject(coin);
            }
            if (ResultOfTheDraw(5))
            {
                SpawnObject(armor);
            }
            if (ResultOfTheDraw(5))
            {
                SpawnObject(starUp);
            }


            currentInterval = interval; 
        }

        foreach(GameObject go in bonuses)
        {
            if (go != null)
            {
                if (go.GetComponent<Coin>())
                {
                    go.GetComponent<Coin>().UpdateActions(go);
                }
                else if (go.GetComponent<SpeedUpPill>())
                {
                    go.GetComponent<SpeedUpPill>().UpdateActions(go);
                }
            }
        }
    }

    public Vector3 RandomVector3()
    {
        System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());
        Vector3 v = new Vector3(rnd.Next((int)-maxX,(int)maxX),0,maxZ * 0.8f);

        return v;
    }

    public void SpawnObject(GameObject ob)
    {
        GameObject.Instantiate(ob, RandomVector3(), Quaternion.identity * Quaternion.Euler(0,rnd.Next(-180,180),0));
    }

    public bool ResultOfTheDraw(int probability)
    {
        System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());

        if (rnd.Next(0, 100) <= probability)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GameOver()
    {
        Debug.Log(score.GetScore());
        PlayerPrefs.SetInt("score", score.GetScore());
        Time.timeScale = 0f;
        SceneManager.LoadScene("GameOver Menu");
    }
}
