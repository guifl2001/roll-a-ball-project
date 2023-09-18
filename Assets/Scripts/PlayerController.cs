using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public TextMeshProUGUI countText;
    public float timeLeft = 60.0f;
    public TextMeshProUGUI countdownText;
    public Transform respawnPoint;
    public MenuController menuController;

    private Rigidbody rb;
    private int count;
    private AudioSource pop;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        pop = GetComponent<AudioSource>();

        SetCountText();
        SetCountdownText();
    }

    private void Update()
    {
        if (transform.position.y < -10)
        {
            Respawn();
        }

        timeLeft -= Time.deltaTime;
        SetCountdownText();
        
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            menuController.WinGame();
            gameObject.SetActive(false);
        }
    }

    void SetCountdownText()
    {
        countdownText.text = "Time Left: " + timeLeft.ToString("0.00");
        if (timeLeft <= 0)
        {
            countdownText.text = "Time's Up!";
            menuController.LoseGame();
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            pop.Play();
            count++;
            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            menuController.LoseGame();
            gameObject.SetActive(false);
        }
    }

    void Respawn()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.Sleep();
        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;
        timeLeft = 60.0f;
    }
}
