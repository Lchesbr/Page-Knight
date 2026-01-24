using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Rigidbody moveCameraRb;
    private MoveCamera moveCamera;
    private Transform cameraMover;
    public float speed = 5.0f;
    public float jumpPower = 10.0f;
    public float pushback = 5.0f;
    public float wallFall;
    public bool isOnPlatform = true;
    public bool isSwamped = false;
    public bool isJumping = false;
    private float moveHorizontal;
    private float yVelocity;
    public float gravityMod;
    public float leftBound;
    public float topBound = 17.7f;
    public float playerPos;
    public bool waitingForCamera;
    public float cameraWaitTime = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        cameraMover = GameObject.Find("Camera Mover").GetComponent<Transform>();
        moveCameraRb = GameObject.Find("Camera Mover").GetComponent<Rigidbody>();
        moveCamera = GameObject.Find("Camera Mover").GetComponent<MoveCamera>();
        Physics.gravity *= gravityMod;
        playerPos = transform.position.x;
        waitingForCamera = false;
        leftBound = cameraMover.position.x - 54.5f;

    }

    // Update is called once per frame
    void Update()
    {
        
        leftBound = cameraMover.position.x - 54.5f;

        if (Input.GetKeyDown(KeyCode.Space) && isOnPlatform && !isJumping)
        {
            isJumping = true;
        } 

        if(waitingForCamera == true)
        {
            speed = 0;
            StartCoroutine(WaitForCamera());

        }

        if (waitingForCamera == false && isSwamped == false)
        {
            speed = 10.0f;
            
        }

        if (transform.position.x < leftBound)
        {
            transform.position = new Vector3(leftBound, transform.position.y, transform.position.z);
            
        }

        if (transform.position.y > topBound)
        {
            transform.position = new Vector3(transform.position.x, topBound, transform.position.z);
            playerRb.linearVelocity = new Vector3(GetComponent<Rigidbody>().linearVelocity.x, 0.0f, 0.0f);

        }

        if (isSwamped)
        {
            speed = 4.5f;
        }
        
        moveHorizontal = Input.GetAxis("Horizontal");
        

    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveHorizontal * speed, GetComponent<Rigidbody>().linearVelocity.y, 0.0f);

        playerRb.linearVelocity = movement;

        if (isJumping)
        {
            isOnPlatform = false;
            isSwamped = false;
            playerRb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJumping = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = true;
            isSwamped = false;
        }

        if (collision.gameObject.CompareTag("Boundary"))
        {
            playerRb.AddForce(Vector3.right * pushback, ForceMode.Impulse);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            isOnPlatform = false;
            isSwamped = false;
        }

        if (collision.gameObject.CompareTag("Swamp"))
        {
            isSwamped = true;
            isOnPlatform = true;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            playerRb.linearVelocity = new Vector3(GetComponent<Rigidbody>().linearVelocity.x, 0.0f, 0.0f);
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            playerRb.linearVelocity = new Vector3(GetComponent<Rigidbody>().linearVelocity.x, wallFall, 0.0f);
        }
    }

    IEnumerator WaitForCamera()
    {
        while(waitingForCamera == true)
        {
            yield return new WaitForSeconds(cameraWaitTime);
            waitingForCamera = false;
        }
    }
}
