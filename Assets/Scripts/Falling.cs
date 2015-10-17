using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using System;

public class Falling : MonoBehaviour
{

    //private CharacterController m_CharacterController;

    private enum AnimationState
    {
        NotAnimated,
        Falling,
        LayingDown,
        GettingUp
    }

    private AnimationState animationState;

    void Start()
    {
        animationState = AnimationState.NotAnimated;
    }


    void Update()
    {
        if (Input.GetButton("Fire3"))
        {
            animationState = AnimationState.Falling;
        }
        switch (animationState)
        {
            case AnimationState.NotAnimated:
                break;
            case AnimationState.Falling:
                FallDown();
                break;
            case AnimationState.LayingDown:
                float vertical = CrossPlatformInputManager.GetAxis("Vertical");
                if (vertical != 0)
                {
                    animationState = AnimationState.GettingUp;
                }
                break;
            case AnimationState.GettingUp:
                GetUp();
                break;
            default:
                break;
        }
    }


    private void FallDown()
    {
        Debug.Log("Falling");
        /* if (!m_CharacterController.isGrounded)
         {
             //m_MoveDir = Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;

             //m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);
         }*/
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(270, transform.rotation.eulerAngles.y, 0)),
            5 * Time.deltaTime);
        if (Math.Abs(transform.rotation.eulerAngles.x - 270) < 0.1f)
        {
            animationState = AnimationState.LayingDown;
            // PlayLandingSound();
        }
    }

    private void GetUp()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0)),
            5 * Time.deltaTime);
        Debug.Log(Math.Cos(transform.rotation.eulerAngles.x));
        Debug.Log(transform.rotation.eulerAngles.x);
        if (Math.Abs(Math.Cos(Mathf.Deg2Rad *transform.rotation.eulerAngles.x) - Math.Cos(Mathf.Deg2Rad * 0)) < 0.001f)
        {
            Debug.Log(Math.Cos(Mathf.Deg2Rad * 0));
            Debug.Log("IN"+Math.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.x));
            Debug.Log("IN"+transform.rotation.eulerAngles.x);
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
            animationState = AnimationState.NotAnimated;
            this.SendMessage("GiveBackControl");
            //PlayLandingSound();
        }
    }

    public void StartFalling()
    {
        animationState = AnimationState.Falling;
    }

}
