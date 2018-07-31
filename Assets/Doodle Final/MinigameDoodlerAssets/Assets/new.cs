using UnityEngine;
using System.Collections;

public class EndOflevel : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        Application.Quit();
    }
}