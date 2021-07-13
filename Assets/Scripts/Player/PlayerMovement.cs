using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody),typeof(SwipeInput))]
public class PlayerMovement : MonoBehaviour
{

    [HideInInspector]
    public float speed;
    [SerializeField] float editorSpeed;
    [SerializeField] Rigidbody rb;
    [SerializeField] float RotationSpeed;
    SwipeInput SwipeInput;
    public Vector3 direction;

    //Joystick
    public Joystick joystick;
    public bool isJoystick;
    public Text useJoystick;


    private void Awake()
    {

        SwipeInput = GetComponent<SwipeInput>();
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    private void Start()
    {

        if (isJoystick)
        {
            useJoystick.text = "Use Swipe";
            joystick.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            useJoystick.text = "Use Joystick";
            joystick.transform.parent.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            speed = editorSpeed;
        }

        if (isJoystick)
        {
            JoystickMovement();
        }
        else
        {
            SwipeMovement();
        }
      
    }

    public void SwipeMovement()
    {
        direction = SwipeInput.direction.normalized;
      
            if (SwipeInput.direction.magnitude >= 30f)
            {
                Vector3 _direction = new Vector3(direction.x, 0, direction.y);
                if (_direction.magnitude != 0)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_direction), RotationSpeed * Time.deltaTime);
                    rb.velocity = transform.forward * speed * 10 * Time.fixedDeltaTime;

                }

            }

       
    }

    public void JoystickMovement()
    {
       
            direction = joystick.Direction;
            Vector3 _direction = new Vector3(direction.x, 0, direction.y);
            if (_direction.magnitude != 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_direction), RotationSpeed * Time.deltaTime);
                rb.velocity = _direction * speed * 10 * Time.fixedDeltaTime;
            }
  
    }


    public void SwitchController()
    {
        if (isJoystick)
        {
            isJoystick = false;
            useJoystick.text = "Use Joystick";
            joystick.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            isJoystick = true;
            useJoystick.text = "Use Swipe";
            joystick.transform.parent.gameObject.SetActive(true);
        }
    }
}
