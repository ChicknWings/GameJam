using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public bool isRewinding = false;
    public TimeController timeController;

    private List<Vector3> positions;
    public bool hitSomething = false;
    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Vector3>();
        timeController = FindObjectOfType<TimeController>();
        timeController.RegisterBullet(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRewinding)
        {
            positions.Insert(0, transform.position); // record the position of bullet
        }
        if (!isRewinding)
        {
            if (!hitSomething)
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
        }
        else
        {
            if (positions.Count > 0)
            {
                transform.position = positions[0];
                positions.RemoveAt(0);
            }
            else
            {
                timeController.UnregisterBullet(this);
                Destroy(gameObject); // if the position list is empty, destroy bullet
            }
        }
    }

    void FixedUpdate()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            hitSomething = true;
        }
    }
    public void StartRewind()
    {
        isRewinding = true; // start rewind
    }

    public void StopRewind()
    {
        isRewinding = false; 
        positions.Clear(); 
    }
}
