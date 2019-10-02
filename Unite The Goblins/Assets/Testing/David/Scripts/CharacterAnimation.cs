using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;
    private CharacterMotor characterMotor;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        characterMotor = GetComponent<CharacterMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 speedVector = new Vector2(characterMotor.movement.velocity.x, characterMotor.movement.velocity.z);
        float speed = 0;
        if (characterMotor.grounded)
            speed = speedVector.magnitude;
        animator.SetFloat("speed", speed);

    }
}
