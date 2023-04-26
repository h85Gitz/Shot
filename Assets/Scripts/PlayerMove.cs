using UnityEngine;
using Manager;
using Utilities;
using Test.SubscriberModel;

internal class PlayerMove : MonoSingleton<PlayerMove>
{
    [SerializeField]
    LayerMask layerMask;

    public Vector3 target => _target;

    public bool isRotate { get; private set; }
    public Vector3 movement;
    public int speed;

    private Animator animator;
    private GameManager _gameManager;
    private Rigidbody _rb;
    private Vector3 _target;

    RaycastHit hitInfo;

    protected override  void Awake()
    {
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _gameManager = GameManager.Instance;
    }


 

    public void Move(float h , float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        _rb.MovePosition(transform.position + movement);
    }


    public void Animating(float h, float v)
    {
        bool isMove = h!=0.0f || v!=0.0f;
        animator.SetBool("IsMove", isMove);
    }

    public  void LookAtMouse()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);
        if (Physics.Raycast(ray, out hitInfo, 1000, layerMask) )
        {
            var playerToMouse = hitInfo.point - transform.position;
            playerToMouse.y = 0f;

            var rotation = Quaternion.LookRotation(playerToMouse);
            _rb.MoveRotation(rotation);
            isRotate = true;
        }
    }

    //保持与事件声明类访问性一致
    public  void  Subscribe<T> (Stackle <T> stack , Stackle<T>.StackEventArgs args )
    {
        Debug.Log("text"+ args);
    }
}
