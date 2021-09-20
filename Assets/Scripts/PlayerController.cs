using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public struct InputIcons
{
    [System.Serializable]
    public struct ButtonIcons
    {
        [Tooltip("Name of Button")]
        public string Button;

        [Tooltip("Icon For Button")]
        public Sprite Icon;
    }

    [Tooltip("Name of Control Scheme")]
    public string ControlScheme;
    
    [Tooltip("List of Icons for Each Control Scheme")]
    public List<ButtonIcons> Buttons;
}

public class PlayerController : MonoBehaviour
{
    public List<InputIcons> Inputs;
    public PlayerInput PlayerInput;

    // Components
    private SpriteRenderer Sprite;
    private Rigidbody2D Rig;
    private Animator Animator;

    [Header("Movement")]
    [Tooltip("Movement Speed of the Player")]
    public float moveSpeed = 3.5f;
    //The Movement from the Input
    private Vector3 movement;

    void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
        Sprite = GetComponent<SpriteRenderer>();
        Rig = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();        
    }

    // Physics Update
    void FixedUpdate()
    {
        transform.position += movement * moveSpeed * Time.deltaTime;
        //Rig.MovePosition(Rig.position + movement * moveSpeed * Time.fixedDeltaTime);   

        // Movement Animation
        bool running = movement != Vector3.zero;
        Animator.SetBool("Run", running); 

        //Flip Sprite (Only Flip if Running and Movement is Character is moving Horizontally)
        if(running && movement.x != 0)
            gameObject.transform.localScale = new Vector3(Mathf.Sign(movement.x), 1, 1);
    }

    // Horizontal Movement from Input
    public void OnMoveHorizontal(InputAction.CallbackContext context)
    {  
        movement.x = context.ReadValue<float>();
    }

    // Vertical Movement From Input
    public void OnMoveVertical(InputAction.CallbackContext context)
    {  
        movement.y = context.ReadValue<float>();
    }

    // Returns the Correct Indicator Sprite based on the Input
    public Sprite ChangeIndicator(string action)
    {
        // Set Icon
        foreach(InputIcons input in Inputs)
        {
            if(input.ControlScheme == PlayerInput.currentControlScheme)
            {
                foreach(InputIcons.ButtonIcons button in input.Buttons)
                {
                    if(button.Button == action)
                    {
                        return button.Icon;
                    }
                }
            }
        }

        return null;
    }
}
