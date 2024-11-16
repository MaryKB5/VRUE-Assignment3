using System;
using System.Linq;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomContinuousMoveProvider : ActionBasedContinuousMoveProvider
{
    public Vector2 input;

    // Start is called before the first frame update
    void Start()
    {
        input = Vector2.zero;

    }

    private bool collision = false;

    protected override Vector2 ReadInput()
    {
        if (this.collision) {
            return Vector2.down;
        }
        return input;
    }
    
    void OnCollisionEnter(Collision collision)
    {  
        string collidedObjectName = collision.collider.transform.name;
        Debug.Log("OnCollissionEnter " + collidedObjectName);
        if (collision.collider.CompareTag("Floor")) {
            Debug.Log("ignored collission with " + collidedObjectName);
            return;
        }

        
        this.collision = true;
        //moveSpeed = 10f;
        //input = Vector2.down;
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        Debug.Log("OnCollissionExit");
        this.collision = false;
    }

    
}
