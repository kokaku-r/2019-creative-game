using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController),typeof(Animator), typeof(CharacterStats))]
public class CharacterBase : MonoBehaviour, ICharacterable
{
    private CharacterController controller;
    private CharacterStats CharacterStats;
    private Animator animator;

    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        OnMove();
    }

    public void Initialize()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        CharacterStats = GetComponent<CharacterStats>();
    }

    public void OnMove()
    {
        var moveDirection = CharacterControl.GetMoveDirection(controller.isGrounded);
        controller.Move(moveDirection * Time.deltaTime);

        transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);

        animator.SetFloat("Speed", CharacterControl.speed);
    }

    public void OnAttack(GameObject attacker, Attack attack)
    {

    }


    public void OnDestruction(GameObject destroyer)
    {
        gameObject.SetActive(false);
    }
}
