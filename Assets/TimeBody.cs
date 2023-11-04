using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public bool isRewinding = false;
    List<Vector3> positions;
    public GameObject cube;
    void Start()
    {
        positions = new List<Vector3>();

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            StartRewind();
        if (Input.GetKeyUp(KeyCode.Z))
            StopRewind();
    }

    private void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
            
    }
    private void Rewind()
    {
        transform.position = positions[0];
        positions.RemoveAt(0);

    }
    void Record() 
    {
        positions.Insert(0, transform.position);
    }

    public void StartRewind() 
    {
        isRewinding = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
    }
}
