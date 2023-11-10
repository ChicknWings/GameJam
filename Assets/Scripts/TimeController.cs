using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    //status
    public static bool isRewinding = false;

    //about player
    public Transform player;
    private List<Vector3> positions;
    private List<Quaternion> rotations;
    public GameObject playerPrefab;
    //public float slowdownFactor = 0.3f;
    //public float slowdownLength = 1.5f;

    //about bullets
    private List<BulletController> bullets = new List<BulletController>();

    //material
    public Material rewindMaterial;

    //ammo collectible 
    [SerializeField] GameObject ammoCollectible;

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
            //startSloMo();
            //new WaitForSecondsRealtime(1);
            //endSloMo();
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

    /*void startSloMo()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.2f;
        
    }

    void endSloMo()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.timeScale * 1f;
    }
    */
    void Record()
    {
        positions.Insert(0, player.position);
        rotations.Insert(0, player.rotation);
    }

    void StartRewind()
    {
        GameObject rewindPlayer = Instantiate(playerPrefab, player.position, player.rotation);
        //rewindPlayer.GetComponent<Renderer>().material = rewindMaterial;
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
        //positions.Clear();
        //rotations.Clear();

        //Replace rewind player with an ammo collectible
        Instantiate(ammoCollectible, rewindPlayer.transform.position, Quaternion.identity);
        Destroy(rewindPlayer);
        
        positions.Clear();
        rotations.Clear();
    }
    public void RegisterBullet(BulletController bullet)
    {
        bullets.Add(bullet);
    }

    // When a bullet is destroyed, remove it from the list
    public void UnregisterBullet(BulletController bullet)
    {
        bullets.Remove(bullet);
    }
}
