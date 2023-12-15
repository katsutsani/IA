using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterBrain : MonoBehaviour
{
    public GameObject brain;
    public Collider Weapon;
    public Collider Shield;

    private void Awake()
    {

        PlayerInputSystem brainInputSystem = brain.GetComponent<PlayerInputSystem>();
        if (brainInputSystem)
        {
            brainInputSystem.CharacterBody = GetComponent<Rigidbody>();
            brainInputSystem.animator = GetComponent<Animator>();
            brainInputSystem.Weapon = Weapon;
            brainInputSystem.Shield = Shield;
        }
    }
}
