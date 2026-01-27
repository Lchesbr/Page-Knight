using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float moveAmount = 2.5f;
    public bool canMove;
    
    public float originalPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPos = GameObject.Find("Camera Mover").GetComponent<Rigidbody>().position.x;
        canMove = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (canMove == true)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        }

        if (transform.position.x > moveAmount + originalPos)
        {

            canMove = false;
            originalPos = transform.position.x;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canMove = true;

        }
    }
}
