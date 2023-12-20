using UnityEngine;



public class MovePlayerScript : MonoBehaviour
{   
    public FloatingJoystick joystick;

    public Rigidbody2D rigidbody2D;
    [HideInInspector] public Vector3 moveVector;

    //Upgrades
    private float speed = 7 + (0.5f * float.Parse(UpgradeLvlUp.upgrades["characterSpeedLevel"].ToString()));

    Animate animate;

    public float distanceTravelled = 0f;
    Vector3 lastPosition;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        moveVector = new Vector3();
        lastVectorH = -1;

        animate = GetComponent<Animate>();

        distanceTravelled = PlayerPrefs.GetFloat("distanceTravelled", 0f);
    }

    [HideInInspector] public float lastVectorV;
    [HideInInspector] public float lastVectorH;
    void Update()
    {
        moveVector.x = joystick.Horizontal;
        moveVector.y = joystick.Vertical;
        //moveVector.x = Input.GetAxisRaw("Horizontal");
        //moveVector.y = Input.GetAxisRaw("Vertical");

        if(moveVector.x !=0){
            lastVectorH = moveVector.x;
            Debug.Log(lastVectorH);
            Debug.Log( moveVector.x);
        } 
        if(moveVector.y !=0){
            lastVectorV = moveVector.y;
        }

        animate.horizontal = moveVector.x;
        moveVector *= speed;
        rigidbody2D.velocity = moveVector;

        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
        PlayerPrefs.SetFloat("distanceTravelled", distanceTravelled);
    }
}
