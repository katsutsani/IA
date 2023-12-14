using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBrain : MonoBehaviour
{
    public GameObject brain;

    private void Awake()
    {

        PlayerInputSystem brainInputSystem = brain.GetComponent<PlayerInputSystem>();
        if (brainInputSystem)
        {
            brainInputSystem.CharacterBody = GetComponent<Rigidbody>();
            brainInputSystem.animator = GetComponent<Animator>();
        }
    }
}
