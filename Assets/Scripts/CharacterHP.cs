using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterHP : MonoBehaviour
{
    [SerializeField]public int maxHP;
    public int currentHP;

    public TextMeshProUGUI playerHPText;

    private void Start()
    {
        currentHP = maxHP;
    }
    // Update is called once per frame
    void Update()
    {
        if(playerHPText != null)
        {
            playerHPText.text = currentHP.ToString();
        }
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
