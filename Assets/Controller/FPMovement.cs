using UnityEngine;

public class FPMovement : MonoBehaviour
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
            moveDir = transform.right;
            next = delay + Time.time;

            print("moveDir is " + moveDir);

            //transform.Translate(transform.right, Space.Self);
            detect(moveDir);
            //move(new Vector3(0, 90, 0));
           
            //iTween.LookTo(this.gameObject, transform.position + moveDir, 0f);
            iTween.MoveTo(this.gameObject, transform.position + change, 0.2f);
            //transform.position = transform.position + change;
           transform.eulerAngles = transform.eulerAngles + Vector3.up * 90;
        }
        if (Input.GetKeyDown(KeyCode.A) && Time.time > next)
        {
            moveDir = -transform.right;
            next = delay + Time.time;
            //transform.Translate(-transform.right, Space.Self);
            detect(moveDir);
            //move(new Vector3(0, -90, 0));
            iTween.MoveTo(this.gameObject, transform.position + change, 0.2f);
            //transform.position = transform.position + change;
            transform.eulerAngles = transform.eulerAngles + Vector3.up * -90;
        }
        if (Input.GetKeyDown(KeyCode.W) && Time.time > next)
        {
            moveDir = transform.forward;
            next = delay + Time.time;
            //transform.Translate(transform.forward, Space.Self);
            detect(moveDir);
            //move(new Vector3(0, 0, 0));
            print("change is " + change);

            iTween.MoveTo(this.gameObject, transform.position + change, 0.2f);
            //transform.position = transform.position + change;
        }
        if (Input.GetKeyDown(KeyCode.S) && Time.time > next)
        {
            moveDir = -transform.forward;
            next = delay + Time.time;
            //transform.Translate(-transform.forward, Space.Self);
            detect(moveDir);
            //move(new Vector3(0, 180, 0));
            iTween.MoveTo(this.gameObject, transform.position + change, 0.2f);
            //transform.position = transform.position + change;
            transform.eulerAngles = transform.eulerAngles + Vector3.up * 180;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && Time.time > next)
        {
            moveDir = transform.up;
            next = delay + Time.time;
            //transform.Translate(-transform.up, Space.Self);
            detect(moveDir);
            //move(new Vector3(-90, 0, 0));
            //iTween.MoveTo(this.gameObject, transform.position + change, 0.2f);
            //transform.position = transform.position + change;
            if (transform.eulerAngles.x <= 90 || transform.eulerAngles.x > 270)
            {
                transform.eulerAngles = transform.eulerAngles + Vector3.right * -90;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && Time.time > next)
        {
            moveDir = -transform.up;
            next = delay + Time.time;
            //transform.Translate(-transform.down, Space.Self);
            detect(moveDir);
            //move(new Vector3(90, 0, 0));
            //iTween.MoveTo(this.gameObject, transform.position + change, 0.2f);
            //transform.position = transform.position + change;
            if (transform.eulerAngles.x < 90.0f || transform.eulerAngles.x >= 270)
            {
                transform.eulerAngles = transform.eulerAngles + Vector3.right * 90;
            }
            print("down arrow, x is " + transform.eulerAngles.x);
        }
    }

    /*void OnTriggerEnter(Collider other)
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
        if ((go.transform.position - this.transform.position).sqrMagnitude <= 1)
        {
            go.transform.Translate(moveDir);
        }
    }*/

    void move(Vector3 DirectEuler)
    {
        iTween.LookTo(this.gameObject, transform.position + moveDir, 0.5f);
        iTween.MoveTo(this.gameObject, transform.position + change, 0.5f);
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
