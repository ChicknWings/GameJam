using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletSpowner : MonoBehaviour
{
    public TextMeshProUGUI ammoCountText;
    int ammoCountTextDisplay;

    public static int ammoCount = 0;
    [SerializeField] int maxAmmo = 4;
    public GameObject bulletPrefab;

    

    // Update is called once per frame
    void Update()
    {
        
        ammoCountTextDisplay = maxAmmo - ammoCount;
        if (ammoCountText != null)
            ammoCountText.text = ammoCountTextDisplay.ToString();
        


        if (Input.GetMouseButtonDown(0) && ammoCount < maxAmmo) // mouse left click
        {
            ammoCount++;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

            //Debug.Log(4 - ammoCount);

        }

    }
}
