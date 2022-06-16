using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ClimbingAction : PlayerActions
{
    [SerializeField] private float climbingSpeed;
    [SerializeField] private float jumpLenght;
    
    [SerializeField] private Transform rayStartPos;
    [SerializeField] private Vector3 offset;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float rayLenght;

    [SerializeField] private Vector3 moveAmount;
    [SerializeField] private float lerpSpeed;
    

    private CharacterController playerController;
    private PlayerInputHandle playerInputHandle;
    private AnimatorManager animatorManager;
    private PlayerManager playerManager;
    private Vector3 moveVector;
    
    private void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    private void Awake()
    {
        playerInputHandle = GetComponent<PlayerInputHandle>();
        animatorManager = GetComponent<AnimatorManager>();
        playerManager = GetComponent<PlayerManager>();
    }

    private void CheckIfOutOfLadder()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayStartPos.position - offset,
                rayStartPos.forward, out hit,
                rayLenght, layer,
                QueryTriggerInteraction.Collide))
        {
            if (hit.transform.gameObject.CompareTag("EndClimb"))
            {
                Debug.Log("out of view");
                animatorManager.ModifyBoolParams("isClimbing", false);
                animatorManager.EnableOverrideLayer();
                StartCoroutine(LerpPosition(transform.position + moveAmount, lerpSpeed));
                playerManager.SwitchStates(PlayerManager.PlayerStates.Active);
                return;
            }
            else if (hit.transform.gameObject.CompareTag("Ladder"))
            {
                animatorManager.ModifyBoolParams("isClimbing", true);
            }
        }
    }

    private void ClimbUp()
    {
        moveVector = transform.up * playerInputHandle.vertical;
        moveVector.x = 0;
        moveVector.z = 0;
        if (playerInputHandle.vertical >= 0.5f)
        {
            moveVector *= climbingSpeed;
        }

        playerController.Move(moveVector * Time.deltaTime);
    }

    private void CheckForMidCancel()
    {
        if (playerInputHandle.jumpInput)
        {
            transform.Rotate(transform.up, 180);
            animatorManager.ModifyBoolParams("isClimbing", false);
            animatorManager.EnableOverrideLayer();
            animatorManager.ModifyBoolParams("isJumping", true);
            moveVector.y += Mathf.Sqrt(-2f * -9.8f * jumpLenght);
            moveVector.z += Mathf.Sqrt(-5f * -9.8f * jumpLenght);
            playerController.Move(moveVector * Time.deltaTime);
            playerManager.SwitchStates(PlayerManager.PlayerStates.Active);
        }
    }
    
    private IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
    
    public override void Action()
    {
        CheckIfOutOfLadder();
        ClimbUp();
        CheckForMidCancel();
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(rayStartPos.position - offset, rayStartPos.forward * rayLenght);
        Gizmos.color = Color.cyan;
    }
    
}
