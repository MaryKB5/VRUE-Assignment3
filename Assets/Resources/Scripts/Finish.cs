using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Finish : MonoBehaviour
{

    public GameObject win;
    public GameObject lose;

    private bool finished = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) {
        string collidedObjectName = collision.collider.transform.name;
        if (collidedObjectName.Equals("Floor")) {
            return;
        }

        if (!finished) {
            finished = true;
            
            Debug.Log("FinishLine OnCollissionEnter " + collidedObjectName);

            Instantiate(win, new Vector3(158.2f, 1.25f, -12.14f), Quaternion.Euler(0, 90, 0));
        }
        
    }
}


