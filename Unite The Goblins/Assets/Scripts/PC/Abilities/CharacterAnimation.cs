using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;
    private CharacterMotor characterMotor;
    private Scream scream;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        characterMotor = GetComponent<CharacterMotor>();
        scream = GetComponent<Scream>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 speedVector = new Vector2(characterMotor.movement.velocity.x, characterMotor.movement.velocity.z);
        float speed = 0;
        if (characterMotor.grounded)
            speed = speedVector.magnitude;
        animator.SetFloat("speed", speed);
        if (scream)
        {
            if (scream.Active)
            {
                animator.SetLayerWeight(1, 1);
            }
            else
            {
                animator.SetLayerWeight(1, 0);
            }
        }

    }
}
