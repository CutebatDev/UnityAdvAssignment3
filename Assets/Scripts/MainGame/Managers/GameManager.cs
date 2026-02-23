using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public PlayerCharacterController playerCharacterController;
    [SerializeField] private FireHazardScriptableObject[] fireHazardScriptableObjects;
    [SerializeField] private FireHazard[] fireHazards;

    private void Start()
    {
        int soCount = fireHazardScriptableObjects.Length;
        for (int i = 0; i < fireHazards.Length; i++)
        {
            fireHazards[i].fireHazardData = fireHazardScriptableObjects[Random.Range(0, soCount)];
            fireHazards[i].onCharacterEnteredAction += HandleCharacterEnteredFire;
        }
    }

    public void HandleCharacterEnteredFire(FireEnteredEventArgs args)
    {
        args.targetCharacterController.TakeDamage(args.damageDealt);
    }
    
}
