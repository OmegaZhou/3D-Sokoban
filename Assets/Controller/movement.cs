using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float delay;

    private Vector3 moveDir;
    private Vector3 change;
    private Vector3 playerNext;
    private float next = 0;

    // Start is called before the first frame update
    void Start()
    {
        Quaternion.LookRotation(Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && Time.time > next)
        {
            moveDir = Vector3.right;
            next = delay + Time.time;
            //transform.Translate(transform.right, Space.Self);
            detect(moveDir);
            move(new Vector3(0, 90, 0));
        }
        if (Input.GetKeyDown(KeyCode.A) && Time.time > next)
        {
            moveDir = Vector3.left;
            next = delay + Time.time;
            //transform.Translate(-transform.right, Space.Self);
            detect(moveDir);
            move(new Vector3(0, -90, 0));
        }
        if (Input.GetKeyDown(KeyCode.W) && Time.time > next)
        {
            moveDir = Vector3.forward;
            next = delay + Time.time;
            //transform.Translate(transform.forward, Space.Self);
            detect(moveDir);
            move(new Vector3(0, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.S) && Time.time > next)
        {
            moveDir = Vector3.back;
            next = delay + Time.time;
            //transform.Translate(-transform.forward, Space.Self);
            detect(moveDir);
            move(new Vector3(0, 180, 0));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && Time.time > next)
        {
            moveDir = Vector3.up;
            next = delay + Time.time;
            //transform.Translate(-transform.up, Space.Self);
            detect(moveDir);
            move(new Vector3(-90, 0, 0)) ;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && Time.time > next)
        {
            moveDir = Vector3.down;
            next = delay + Time.time;
            //transform.Translate(-transform.down, Space.Self);
            detect(moveDir);
            move(new Vector3(90, 0, 0));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        print("Trigger entered");
        GameObject go = other.gameObject;
        float distance = (go.transform.position - this.transform.position).sqrMagnitude;
        print(go.transform.position + "go's position");
        print(this.transform.position + "this's position"); 
        print(distance);
        if (distance < 1)
        {
            go.transform.Translate(moveDir);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        print("Collision entered");
        GameObject go = collision.gameObject;
        if((go.transform.position - this.transform.position).sqrMagnitude <= 1)
        {
            go.transform.Translate(moveDir);
        }
    }

    void move(Vector3 DirectEuler)
    {
        playerNext = this.gameObject.transform.position + change;
        iTween.MoveTo(this.gameObject, playerNext, 0.5f);
        iTween.RotateTo(this.gameObject, DirectEuler, 0.2f);
    }

    void detect(Vector3 direct)
    {
        RaycastHit hit;

        change = direct;

        if (Physics.Raycast(this.gameObject.transform.position, direct, out hit, 1))
        {
            if (hit.transform.tag == "box")
            {
                print("box found");

                RaycastHit boxhit;
                if (Physics.Raycast(hit.transform.position, direct, out boxhit, 1) && boxhit.transform.tag != "des")
                {
                    change = new Vector3(0, 0, 0);
                }
                hit.transform.position = hit.transform.position + change;
            }
            else if (hit.transform.tag == "wall")
            {
                print("wall found");
                change = new Vector3(0, 0, 0);
            }
        }
    }
}

