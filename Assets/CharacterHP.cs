using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHP : MonoBehaviour
{
    [SerializeField]public int maxHP;
    public int currentHP;

    private void Start()
    {
        currentHP = maxHP;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damageAmount)
    {
        currentHP -= damageAmount;
        //Debug.Log(currentHP);
        if (currentHP <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
