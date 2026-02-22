using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class FireHazard : MonoBehaviour
{
    public event UnityAction<FireEnteredEventArgs> onCharacterEnteredAction;
    
    [HideInInspector] public FireHazardScriptableObject fireHazardData;

    [SerializeField] private PlayerCharacterController playerScrRef;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root == playerScrRef.transform)
        {

#if UNITY_EDITOR
            Debug.Log("Player entered this hazard");
#endif

            FireEnteredEventArgs fireEnteredEventArgs = new FireEnteredEventArgs
            {
                damageDealt = fireHazardData.GetRandomFireDamage(),
                targetCharacterController = playerScrRef
            };
            onCharacterEnteredAction.Invoke(fireEnteredEventArgs);
        }
    }
}

public class FireEnteredEventArgs
{
    public int damageDealt;
    public PlayerCharacterController targetCharacterController;
}
