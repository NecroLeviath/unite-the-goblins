using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private int _currentHealth;
    public int CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = value;
            HealthEvent(_currentHealth);
        }
    }

    public float immuneTimeInSec = 2.0f;

    private bool isImmune;
    private float timer;

    public delegate void HealthDel(int hp);
    public event HealthDel HealthEvent;

    void Start()
    {
        _currentHealth = 3;
        isImmune = false;
        timer = 0.0f;
    }

    void Update()
    {
        if (isImmune)
        {
            timer += Time.deltaTime;

            if (timer >= immuneTimeInSec)
            {
                isImmune = false;
                timer = 0;
            }
        }
    }

    public void TakeDamage(int dmg = 1)
    {
        if (isImmune) return;

        CurrentHealth -= dmg;

        if (CurrentHealth <= 0)
        {
            GameObject.Find("Character Manager").GetComponent<CharacterManager>().Die();
        }
        else
        {
            isImmune = true;
        }
    }
}
