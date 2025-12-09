using System.Collections;
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
    public bool isOnPlatform = true;
    private float moveHorizontal;
    public float gravityMod;
    public float leftBound;
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
        leftBound = cameraMover.position.x - 52.0f;

    }

    // Update is called once per frame
    void Update()
    {

        leftBound = cameraMover.position.x - 52.0f;

        if (Input.GetKeyDown(KeyCode.Space) && isOnPlatform)
        {
            isOnPlatform = false;
            playerRb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        } 

        if(waitingForCamera == true)
        {
            speed = 0;
            StartCoroutine(WaitForCamera());

        }

        if (waitingForCamera == false)
        {
            speed = 10.0f;
            
        }

        if (transform.position.x < leftBound)
        {
            transform.position = new Vector3( leftBound, transform.position.y, transform.position.z);
            
        }

        
        moveHorizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * moveHorizontal * speed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = true;
        }

        if (collision.gameObject.CompareTag("Boundary"))
        {
            playerRb.AddForce(Vector3.right * pushback, ForceMode.Impulse);
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
