using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{

    public int score = 0;
    public static ShowScore instance;

    void Start()
    {
        instance = this;
        GetComponent<Text>().text = "" + this.score;
    }
    private void Update()
    {
        GetComponent<Text>().text = "" + this.score;
    }
    public int getScore()
    {
        return this.score;
    }
}
