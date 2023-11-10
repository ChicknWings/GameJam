using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public Rigidbody rb;

    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        rb.freezeRotation = true;
        Vector3 move = new Vector3(h, 0f, v) * moveSpeed * Time.deltaTime;
        transform.position += move;

        rb.freezeRotation = false;
    }

    private void Turn()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.transform.position.y - transform.position.y));
        Vector3 targetDirection = mousePosition - transform.position;
        targetDirection.y = 0; 

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 1000f);
    }

    private void OnTriggerEnter(Collider ammoBall)
    {
        if (ammoBall.gameObject.tag == "AmmoCollectible")
        {
            BulletSpowner.ammoCount = 0;
            Destroy(ammoBall.gameObject);
        }
    }
}
