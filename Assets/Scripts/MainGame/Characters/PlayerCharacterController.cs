using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField] private Camera mainCam;

    public event UnityAction<int> onTakeDamageEventAction;

    [Header("Navigation")] 
    [SerializeField] private NavMeshAgent navMeshAgent;

    [SerializeField] private Transform[] pathWaypoints;
    
    [SerializeField] private Animator animator;

    private const string speedString = "Speed";
    private static readonly int speedHash = Animator.StringToHash(speedString);

    public int hp = 100;
    private int startingHp;
    
    public int currentWaypointIndex = 0;

    private bool isMoving = true;

    private bool hasBloodyBoots = true;

    public void ToggleMoving(bool shouldMove)
    {
        isMoving = shouldMove;
        if (navMeshAgent) navMeshAgent.enabled = shouldMove;
    }

    public void SetDestination(Transform targetTransformWaypoint)
    {
        if (navMeshAgent)
            navMeshAgent.SetDestination(targetTransformWaypoint.position);
    }

    public void SetDestination(int waypointIndex)
    {
        SetDestination(pathWaypoints[waypointIndex]);
    }

    public void TakeDamage(int damageAmount)
    {
        hp -= damageAmount;
        animator.SetLayerWeight(1, (1 - ((float)hp / startingHp)));
        onTakeDamageEventAction?.Invoke(hp);
    }

    private void Start()
    {
        startingHp = hp;
        SetMudAreaCost();
        ToggleMoving(true);
        SetDestination(pathWaypoints[0]);
    }

    private void SetMudAreaCost()
    {
        if (hasBloodyBoots)
        {
            navMeshAgent.SetAreaCost(3, 1);
        }
    }

    [ContextMenu("Take Damage Test")]
    private void TakeDamageTesting()
    {
        TakeDamage(10);
    }


    private void Update()
    {
        if (isMoving && !navMeshAgent.isStopped && navMeshAgent.remainingDistance <= 0.1f)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= pathWaypoints.Length)
                currentWaypointIndex = 0;

            SetDestination(pathWaypoints[currentWaypointIndex]);
        }

        if (animator)
            animator.SetFloat(speedHash, navMeshAgent.velocity.magnitude);
        
        if (mainCam)    
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                //We want to know what the mouse is hovering now

#if UNITY_EDITOR
                Debug.Log($"Hit: {hit.collider.name}");
#endif

            }
        }

    }
}