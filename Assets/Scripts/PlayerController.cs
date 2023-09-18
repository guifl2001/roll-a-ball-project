using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public TextMeshProUGUI countText;
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
    }

    private void Update()
    {
        if (transform.position.y < -10)
        {
            Respawn();
        }
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
    }
}
