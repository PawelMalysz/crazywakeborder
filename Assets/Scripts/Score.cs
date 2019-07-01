using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text valueOfScore;
    private int score = 0;
    private int bonusScore = 0;
    private float timer = 0;

    void Start()
    {
        valueOfScore = GameObject.Find("Value").GetComponent<Text>();
    }
    
    void Update()
    {
        timer += Time.deltaTime;

        
            score = (int)(timer % 60);
        

        valueOfScore.text = " " + (score + bonusScore);
    }

    public void AddScore(int value)
    {
        bonusScore += value;
    }

    public int GetScore()
    {
        return score + bonusScore;
    }
}
