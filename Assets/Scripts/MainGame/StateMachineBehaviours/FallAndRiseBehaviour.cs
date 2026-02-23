using UnityEngine;
using UnityEngine.Animations;

public class FallAndRiseBehaviour : StateMachineBehaviour
{
    [SerializeField] private PlayerCharacterController playerCharacterController;

    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        playerCharacterController.ToggleMoving(false);
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        playerCharacterController.ToggleMoving(true);
    }
}
