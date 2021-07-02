using UnityEngine;
public class SwipeInput : MonoBehaviour
{

    public Vector2 Direction;
    Vector2 InitialPos;
    public Animator CarChoosePanel;
    private void Start()
    {
        
     //   CarChoosePanel = GameObject.FindGameObjectWithTag("Car Panel").GetComponent<Animator>();
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            InitialPos = Input.mousePosition;

        }
        else if (Input.GetMouseButtonUp(0))
        {
            InitialPos = Vector2.zero;
        }
        if (Input.GetMouseButton(0))
        {
            Direction = ((Vector2)(Input.mousePosition) - InitialPos);
        }
        else
            Direction = Vector3.zero;
    }
}
