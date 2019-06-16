using UnityEngine;

public class FPMovement : MonoBehaviour
{
    public float delay;
    public bool isPaused = false;
    public GameObject all;

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
        if (!isPaused && Input.GetKeyDown(KeyCode.D) && Time.time > next)
        {
            moveDir = transform.right;
            next = delay + Time.time;

            transform.eulerAngles = transform.eulerAngles + Vector3.up * 90;
        }
        if (!isPaused && Input.GetKeyDown(KeyCode.A) && Time.time > next)
        {
            moveDir = -transform.right;
            next = delay + Time.time;


            transform.eulerAngles = transform.eulerAngles + Vector3.up * -90;
        }
        if (!isPaused && Input.GetKeyDown(KeyCode.W) && Time.time > next)
        {
            moveDir = transform.forward;
            next = delay + Time.time;
            detect(moveDir);

            iTween.MoveTo(this.gameObject, transform.position + change, 0.2f);
        }
        if (!isPaused && Input.GetKeyDown(KeyCode.S) && Time.time > next)
        {
            moveDir = -transform.forward;
            next = delay + Time.time;

            transform.eulerAngles = transform.eulerAngles + Vector3.up * 180;
        }
        if (!isPaused && Input.GetKeyDown(KeyCode.UpArrow) && Time.time > next)
        {
            moveDir = transform.up;
            next = delay + Time.time;
            detect(moveDir);

            iTween.MoveTo(gameObject, transform.position + change, 0.2f);
        }
        if (!isPaused && Input.GetKeyDown(KeyCode.DownArrow) && Time.time > next)
        {
            moveDir = -transform.up;
            next = delay + Time.time;
            detect(moveDir);

            iTween.MoveTo(gameObject, transform.position + change, 0.2f);
        }

        if (!isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        all.GetComponent<CheckFinishment>().checkMatch();
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

        if (Physics.Raycast(transform.position + (Vector3.up * 0.25f), direct, out hit, 1))
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
            else if (hit.transform.tag == "wall" || hit.transform.tag == "button" || hit.transform.tag == "fire")
            {
                print("wall found");
                change = new Vector3(0, 0, 0);
            }
            print(hit.transform.tag);
        }
    }

}
