using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class GameInput : MonoBehaviour
{
    /// <summary>
    /// 交互发生时候的事件
    /// </summary>
    public event EventHandler OnInteractAction;
    /// <summary>
    /// 交互替代发生时候的事件
    /// </summary>
    public event EventHandler OnInteractAlternateAction;
    /// <summary>
    /// 投掷物体发生时候的事件
    /// </summary>
    public event EventHandler OnThrowAction;

    private PlayerInuptAction playerInuptAction;
    private void Awake()
    {
        playerInuptAction = new PlayerInuptAction();
        playerInuptAction.Player.Enable();
        playerInuptAction.Player.Interact.performed += Interact_performed;
        playerInuptAction.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInuptAction.Player.Throw.performed += Throw_performed;
    }

    public Vector3 GetInputVectorNomalized()
    {
        Vector2 inputVector = playerInuptAction.Player.Move.ReadValue<Vector2>();

        //if (Input.GetKey(KeyCode.W))
        //{
        //    inputVector.y = 1;
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    inputVector.x = -1;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    inputVector.y = -1;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    inputVector.x = 1;
        //}

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    public void Throw_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) 
    {
        OnThrowAction?.Invoke(this, EventArgs.Empty);
    }
}
