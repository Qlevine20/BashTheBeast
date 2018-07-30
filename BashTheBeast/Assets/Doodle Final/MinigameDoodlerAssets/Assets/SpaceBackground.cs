using UnityEngine;
using System.Collections;

public class SpaceBackground : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        Application.Quit();
    }
}