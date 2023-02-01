using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CartMovement))]
public class BasicController : MonoBehaviour
{
    CartMovement cartMovement;
    private PlayerInputControls PlayerControls;
    
    private void Awake()
    {
        PlayerControls = new PlayerInputControls();
        cartMovement = GetComponent<CartMovement>();
    }

    private void OnEnable()
    {
        PlayerControls.Enable();
    }

    private void OnDisable()
    {
        PlayerControls.Disable();
    }

    void Update()
    {
        SetPlayerInputForMovement();
    }

    private void SetPlayerInputForMovement()
    {
       SetXInput();
       SetYInput();
       if(PlayerControls.Player.Fire.triggered) TriggerFire();
    }

    private void SetYInput()
    {
        if (PlayerControls.Player.Move.ReadValue<Vector2>() != Vector2.zero)
        {
            var input = PlayerControls.Player.Move.ReadValue<Vector2>(); 
            cartMovement.RawAccelerationInput = input.y;
        }
    }

    private void SetXInput()
    { 
        if (PlayerControls.Player.Look.ReadValue<Vector2>() != Vector2.zero)
        {
            var input = PlayerControls.Player.Look.ReadValue<Vector2>(); 
            cartMovement.RawSteeringInput = input.x;
        }
    }
    
    private void TriggerFire()
    {
        Debug.Log("Trigger Fired");
    }
}
