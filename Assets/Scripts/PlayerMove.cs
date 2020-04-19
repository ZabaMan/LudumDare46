using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public string[] controls;



    public void SetupControls(string[] controls)
    {
        this.controls = controls;
    }


    // Update is called once per frame
    void Update()
    {
        var horizontalMovement = 0;
        if (Input.GetKey(controls[0])) 
        {
            horizontalMovement--;
        }
        if (Input.GetKey(controls[1]))
        {
            horizontalMovement++;
        }

        var verticalMovement = 0;
        if (Input.GetKey(controls[2]))
        {
            verticalMovement++;
        }
        if (Input.GetKey(controls[3]))
        {
            verticalMovement--;
        }

        if(verticalMovement != 0 || horizontalMovement != 0)
        {
            GetComponent<Animator>().SetBool("moving", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("moving", false);
        }

        transform.Translate(Vector3.up * (verticalMovement - (horizontalMovement/2)) * Time.deltaTime);
        transform.Translate(Vector3.right * (horizontalMovement - (verticalMovement/2)) * Time.deltaTime);
    }

    private void OnDisable()
    {
        GetComponent<Animator>().SetBool("moving", false);
    }
}
