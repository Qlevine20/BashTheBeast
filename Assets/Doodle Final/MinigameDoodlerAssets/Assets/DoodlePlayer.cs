using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class DoodlePlayer : MonoBehaviour {

    public float movementSpeed = 10f;
    public Text gameovertext;
    public bool dead = false;
    public int score = 0;
    private float maxheight = 0;
    public int heightchange = 5;
    public Text scoretext;
    public bool gamestart = false;
    public int divider = 1000;
    public AudioSource mainAudio;
    Rigidbody2D rb;

    float movement = 0f;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        movement = Input.GetAxis("Horizontal") * movementSpeed;
        if (transform.position.y - maxheight > heightchange)
        {
            score += 50;
            scoretext.text = "Score: " + score;
            maxheight = transform.position.y;
        }

        if(dead && Input.GetMouseButton(0))
        {
            SceneManager.LoadScene(3);
        }
    }

    void FixedUpdate()
    {
        if (dead == false && gamestart == true)
        {
            rb.gravityScale = 1;
            Vector2 velocity = rb.velocity;
            velocity.x = movement;
            rb.velocity = velocity;
        }
        else
        {
            rb.gravityScale = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Deathwall")
        {
            OnPlayerDeath();
        }
    }
   
    public void OnPlayerDeath()
    {
        rb.velocity = Vector2.zero;
        dead = true;
        gameovertext.gameObject.SetActive(true);
        Debug.Log("Player dead");
        GetComponent<SpriteRenderer>().enabled = false;
        if (PersistentInfo.instance)
        {
            PersistentInfo.instance.MoveForward = Mathf.RoundToInt(score / divider);
        }
        mainAudio.Stop();

    }


    
}