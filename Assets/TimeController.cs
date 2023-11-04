using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    //status
    public bool isRewinding = false;

    //about player
    public Transform player;
    private List<Vector3> positions;
    private List<Quaternion> rotations;
    public GameObject playerPrefab;

    //about bullets
    private List<BulletController> bullets = new List<BulletController>();

    //material
    public Material rewindMaterial;

    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Vector3>();
        rotations = new List<Quaternion>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartRewind();
        }
        /*
        if (Input.GetKeyUp(KeyCode.Z))
        {
            StopRewind();
        } 
        */
    }
    void FixedUpdate()
    {
        if (!isRewinding)
        {
            Record();
        }
    }
    void Record()
    {
        positions.Insert(0, player.position);
        rotations.Insert(0, player.rotation);
    }

    void StartRewind()
    {
        GameObject rewindPlayer = Instantiate(playerPrefab, player.position, player.rotation);
        rewindPlayer.GetComponent<Renderer>().material = rewindMaterial;
        // Disable any scripts that could affect the rewind player
        MonoBehaviour[] scripts = rewindPlayer.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
        // Disable any Rigidbody physics interactions
        if (rewindPlayer.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = true;
        }
        StartCoroutine(Rewind(rewindPlayer));
        foreach (var bullet in bullets)
        {
            bullet.StartRewind(); // each bullet start rewind
        }
    }
    IEnumerator Rewind(GameObject rewindPlayer)
    {
        isRewinding = true;
        int index = 0;
        while (index < positions.Count)
        {
            rewindPlayer.transform.position = positions[index];
            rewindPlayer.transform.rotation = rotations[index];
            index++;
            yield return new WaitForFixedUpdate();
        }

        isRewinding = false;
        positions.Clear();
        rotations.Clear();
        Destroy(rewindPlayer);
    }
    public void RegisterBullet(BulletController bullet)
    {
        bullets.Add(bullet);
    }

    // 当子弹被销毁时，将其从列表中移除
    public void UnregisterBullet(BulletController bullet)
    {
        bullets.Remove(bullet);
    }
}
