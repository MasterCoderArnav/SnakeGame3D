using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerController controller;
    private int horizontal, vertical;
    private enum Axis
    {
        Horizontal,
        Vertical
    }

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        horizontal = 0;
        vertical = 0;
        getKeyboardInput();
        setMovement();
    }

    private void getKeyboardInput()
    {
        horizontal = GetAxis(Axis.Horizontal);
        vertical = GetAxis(Axis.Vertical);
        if (horizontal != 0)
        {
            vertical = 0;
        }
    }

    private void setMovement()
    {
        if(horizontal != 0)
        {
            controller.setInputDirection(horizontal == 1 ? PlayerDirection.RIGHT : PlayerDirection.LEFT);
        }
        else if(vertical != 0)
        {
            controller.setInputDirection(vertical==1?PlayerDirection.UP: PlayerDirection.DOWN);
        }
    }

    private int GetAxis(Axis axis)
    {
        if (axis == Axis.Horizontal) {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                return -1;
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                return 1;
            }
        }
        else if(axis == Axis.Vertical)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                return 1;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                return -1;
            }
        }
        return 0;
    }
}
