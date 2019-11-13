using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public float upwardForce=500f;
    private Rigidbody rb;
    private int count;
    public int maxCount;
    public Text message;
    public Text message2;
    public float timeLeft = 30f;
    private Text timeText;
    private bool start;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        start = false;
        count = 0;
        maxCount = 20;
        SetText();
    }


    void FixedUpdate ()
    {
        if (start == false)
        {
            message2.text = "Press S to start...";
            if (Input.GetKeyDown(KeyCode.S))
                start = true;
        }

        if (start == true)
        {
            message2.text = "Press Q to Quit...";
            if (Input.GetKeyDown(KeyCode.S))
            {
                start = false;
                QuitGame();
            }
        
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.AddForce(movement * speed);
       
            if (Input.GetKeyDown("space") && rb.transform.position.y <= 1.0f)
            {
                Vector3 jump = new Vector3(0.0f, 200.0f, 0.0f);
                rb.AddForce(jump * speed);
            }

            if(count==maxCount)
            {
                message.text = "Level Completed!!! ";
                resetGame();
            }

            if(rb.transform.position.y<=30f)
            {
                message.text = "You have fallen!!!";
                resetGame();
            }

            timeLeft -= Time.deltaTime;

            if (timeLeft < 0)
            {
                message.text = "Time Out!!!";
                resetGame();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
            count=count+1;
            timeLeft += 10f;
            SetText();
        }
        if(other.gameObject.CompareTag("NonCollectible"))
        {
            message.text = "Oops!!! Try Again ";
            resetGame();
        }
    }

    void SetText()
    {
        countText.text ="Count: " + count.ToString();
        timeText.text = "Time Remaining: " + timeLeft.ToString();
    }
    public void QuitGame()
    {
        message.text = "Game over. Press E to exit. ";
        if (Input.GetKeyDown(KeyCode.E))
            Application.Quit();
    }
    public void resetGame()
    {
            start = false;
            count = 0;
    }

}
