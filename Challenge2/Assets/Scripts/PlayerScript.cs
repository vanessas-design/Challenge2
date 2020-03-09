using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerScript : MonoBehaviour
{
    public Text countText;
    public Text winText;
    public Text livesText;
    public Text restartText;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;


    public float speed;

    private Rigidbody2D rd2d;
    private int count;
    private int lives;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        count = 0;
        lives = 3;
        SetCountText();
        SetLivesText();
        winText.text = "";
        restartText.text = "";
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (Input.GetKey("r"))
        {
            SceneManager.LoadScene(0);
        }
        countText.text = "Count:" + count.ToString(); 
        if (count >= 0)
        {
            musicSource.clip = musicClipOne;
            musicSource.Play();
        }
        if (count ==8)
        {
            musicSource.clip = musicClipTwo;
            musicSource.Play(); 
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            count = count + 1;
            SetCountText();
            Destroy(collision.collider.gameObject);
        }
        else if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            lives = lives - 1;
            SetLivesText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count:" + count.ToString();
        if (count == 8)
        {
            winText.text = "You Win! Game created by Vanessa Seymour!";
        }
        if (count == 4)
        {
            transform.position = new Vector2(61.1f, .1f);
            lives = 3;
            livesText.text = "Lives:" + lives.ToString();
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives:" + lives.ToString();
        if (lives == 0)
        {
            winText.text = "Game Over!";
            DestroyScriptInstance();
            restartText.text = "Press 'R' to try again";
        }

        void DestroyScriptInstance()
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }

        }
    }
    
}