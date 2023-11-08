using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    [SerializeField] Rigidbody playerRb;
    [SerializeField] float y = -10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tempPos = playerRb.transform.position;

        tempPos.y = tempPos.y - y;
        
        transform.position = tempPos;   
    }
}
