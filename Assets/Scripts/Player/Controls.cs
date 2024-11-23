using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    private CharacterController controller;
    private PlayerInput playerInputs;

    private float playerSpeed = 6f;



    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInputs = GetComponent<PlayerInput>();
        
    }

    void Update()
    {
        Vector2 ýnput = playerInputs.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(ýnput.x, 0, ýnput.y);
        controller.Move(move*Time.deltaTime*playerSpeed);
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("isRunning", true);
        }
        else
        {
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("isRunning", false);
        }

        if (playerInputs.actions["Touch"].triggered)
        {

        }

    }
}
