using UnityEngine;


[RequireComponent(typeof(Rigidbody),typeof(SwipeInput))]
public class PlayerMovement : MonoBehaviour
{

    public float speed;
    [SerializeField] Rigidbody rb;
    [SerializeField] float RotationSpeed;
    SwipeInput SwipeInput;
    public Vector3 Direction;

    public float Speed { get => speed; set => speed = value; }

    private void Awake()
    {
    
        SwipeInput = GetComponent<SwipeInput>();
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }
 
    void Update()
    {

 
        Direction = SwipeInput.Direction.normalized;
        if (SwipeInput.Direction.magnitude >=30f )
        {
            Vector3 direction = new Vector3(Direction.x, 0, Direction.y);
            if (direction.magnitude!=0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), RotationSpeed * Time.deltaTime);
                rb.velocity = transform.forward * Speed * 10 * Time.fixedDeltaTime;
            
            }

        }
      
     
    }
 
}
