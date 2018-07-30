using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 30f;
    public Text gameovertext;
    public int divider = 1000;
    public Blade playerscript;

    [SerializeField] Text countdownText;

	void Start ()
    {
        currentTime = startingTime;
	}
	
	void Update ()
    {
        if (playerscript.gamestart)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");
        }

        if(currentTime <= 0)
        {
            currentTime = 0;
            StartCoroutine(ondeathsequence(5));
        }
	}

    IEnumerator ondeathsequence(float time)
    {
        gameovertext.gameObject.SetActive(true);
        playerscript.dead = true;
        playerscript.mainAudio.Stop();
        if (PersistentInfo.instance)
        {
            PersistentInfo.instance.MoveForward = Mathf.RoundToInt(ShowScore.instance.score / divider); 
        }
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(3);
    }
}