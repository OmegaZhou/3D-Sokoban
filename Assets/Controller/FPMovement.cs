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
        var key = gameObject.GetComponent<Sound>().GetDirection();

        if ((key == 3 || Input.GetKeyDown(KeyCode.D)) && Time.time > next)
        {
            right();
        }
        if ((key == 2 || Input.GetKeyDown(KeyCode.A)) && Time.time > next)
        {
            left();
        }
        if ((key == 0 || Input.GetKeyDown(KeyCode.W)) && Time.time > next)
        {
            forward();
        }
        if ((key == 1 || Input.GetKeyDown(KeyCode.S) && Time.time > next))
        {
            back();
        }
        if ((key == 4 || Input.GetKeyDown(KeyCode.UpArrow)) && Time.time > next)
        {
            up();
        }
        if ((key == 5 || Input.GetKeyDown(KeyCode.DownArrow)) && Time.time > next)
        {
            down();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        all.GetComponent<CheckFinishment>().checkMatch();
    }


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
            else if (hit.transform.tag == "wall" || hit.transform.tag == "fire" || hit.transform.tag == "button")
            {
                print("wall found");
                change = new Vector3(0, 0, 0);
            }
        }
    }

    public void forward()
    {
        if (!isPaused)
        {
            moveDir = transform.forward;
            next = delay + Time.time;
            detect(moveDir);

            iTween.MoveTo(this.gameObject, transform.position + change, 0.2f);
        }
    }

    public void back()
    {
        if (!isPaused)
        {
            moveDir = -transform.forward;
            next = delay + Time.time;

            transform.eulerAngles = transform.eulerAngles + Vector3.up * 180;
        }
    }

    public void left()
    {
        if (!isPaused)
        {
            moveDir = -transform.right;
            next = delay + Time.time;


            transform.eulerAngles = transform.eulerAngles + Vector3.up * -90;
        }
    }

    public void right()
    {
        if (!isPaused)
        {
            moveDir = transform.right;
            next = delay + Time.time;

            transform.eulerAngles = transform.eulerAngles + Vector3.up * 90;
        }
    }

    public void up()
    {
        if (!isPaused)
        {
            moveDir = transform.up;
            next = delay + Time.time;
            detect(moveDir);

            iTween.MoveTo(gameObject, transform.position + change, 0.2f);
        }
    }

    public void down()
    {
        if (!isPaused)
        {
            moveDir = -transform.up;
            next = delay + Time.time;
            detect(moveDir);

            iTween.MoveTo(gameObject, transform.position + change, 0.2f);
        }
    }
}
