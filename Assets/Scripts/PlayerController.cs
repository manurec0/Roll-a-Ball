using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private float distToGround;
    public bool isMoving;
    public bool isGrounded;

    public AudioSource footsteps;
    public AudioSource pickUp;
    public AudioSource colSound;
    public AudioSource groundCol;

    private Rigidbody rb;
    private int count;

    public float movementX;
    public float movementY;

    public int startingPitch = 1;
    public int volumeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        rb = GetComponent<Rigidbody>();
        distToGround = 0.5f;
        isMoving = false;
        SetCountText ();
        footsteps = GetComponent<AudioSource>();
        footsteps.time = volumeSpeed;
        footsteps.pitch = startingPitch;

        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winTextObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        isGrounded = IsGrounded();
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        footsteps.volume = Mathf.Clamp01(rb.velocity.magnitude / volumeSpeed);
        footsteps.pitch = Mathf.Clamp(rb.velocity.magnitude / startingPitch, 1, 2);
        if (rb.angularVelocity.magnitude >= 1)
        {
            isMoving = true;
            if (!footsteps.isPlaying && isGrounded)
            {
                footsteps.Play();
            }
            //footsteps.Play();
            //print("entra");
        }
        else {
            isMoving = false;
            footsteps.Stop();
        }



        //if (isMoving == true)
        //{
        //    footsteps.Play();
        //}
        //else
        //    footsteps.Stop();
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
            // Set the text value of your 'winText'
            winTextObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            count += 1;
            pickUp.Play();
            other.gameObject.SetActive(false);

            // Run the 'SetCountText()' function (see below)
            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CollisionSound"))
        {
            colSound.Play();
        }
        if (collision.gameObject.CompareTag("GroundCol"))
        {
            groundCol.Play();
        }
    }

    //bool IsGrounded()
    //{
    //    return GetComponent<Rigidbody>().velocity.y == 0;
    //}
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

}
