using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    private int lastHP;
    
    [SerializeField] private PlayerCharacterController goofyGoober;

    public void RefreshHPText(int newHP)
    {
        if (lastHP == newHP)
            return;

        hpText.text = newHP.ToString();
        lastHP = newHP;
    }

    private void Awake()
    {
        goofyGoober.onTakeDamageEventAction += RefreshHPText;
    }
}
