using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    [HideInInspector]
    public float speed;
    [SerializeField] float editorSpeed;
    [SerializeField] Rigidbody rb;
    [SerializeField] float RotationSpeed;
    [SerializeField] Animator anim;


    public Joystick joystick;



    //Swipe Input
    public Vector2 direction;
    Vector2 initialPos;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    private void Start()
    {

        //if (isJoystick)
        //{
        //    useJoystick.text = "Use Swipe";
        //    joystick.transform.parent.gameObject.SetActive(true);
        //}
        //else
        //{
        //    useJoystick.text = "Use Joystick";
        //    joystick.transform.parent.gameObject.SetActive(false);
        //}
    }
    void Update()
    {
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    return;
        //}
        if (Input.GetMouseButtonDown(0))
        {
            speed = editorSpeed;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            rb.velocity = Vector3.zero;
            direction = Vector2.zero;
            anim.SetBool("isRunning", false);
        }

        //if (isJoystick)
        //{
        //    JoystickMovement();
        //}
        //else
        //{
        //    SwipeMovement();
        //}
        JoystickMovement();
    }
   
    //public void SwipeMovement()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        initialPos = Input.mousePosition;
    //    }
    //    else if (Input.GetMouseButton(0))
    //    {
    //        direction = ((Vector2)(Input.mousePosition) - initialPos);
    //        anim.SetBool("isRunning", true);
    //    }

    //    if (direction.magnitude >= 10f)
    //    {
                
    //            Vector3 _direction = new Vector3(direction.x, 0, direction.y);
    //            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_direction), RotationSpeed * Time.deltaTime);
    //            rb.velocity = transform.forward * speed * 10 * Time.fixedDeltaTime;
               
               
    //    }
    //}

    public void JoystickMovement()
    {


        if (Input.GetMouseButtonDown(0))
        {
            initialPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
           
            anim.SetBool("isRunning", true);
        }
        Vector3 joystickDirection = joystick.Direction;
        Vector3 _direction = new Vector3(joystickDirection.x, 0, joystickDirection.y);
        direction = joystickDirection;
        if (_direction.magnitude != 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_direction), RotationSpeed * Time.deltaTime);
            rb.velocity = _direction * speed * 10 * Time.fixedDeltaTime;
        }

    }


    //public void SwitchController()
    //{
    //    if (isJoystick)
    //    {
    //        isJoystick = false;
    //        useJoystick.text = "Use Joystick";
    //        joystick.transform.parent.gameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        isJoystick = true;
    //        useJoystick.text = "Use Swipe";
    //        joystick.transform.parent.gameObject.SetActive(true);
    //    }
    //}
}
