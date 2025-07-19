using UnityEngine;

public class MeteorMove : MonoBehaviour
{
    [SerializeField] float Speed = 2;
    public Vector2 Dir = Vector2.down;
    Vector3 RoatationEular = new Vector3(0,1,0);
    Vector3 velocity = Vector3.zero;
    float xSpeed, ySpeed;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        Dir.Set(GameEvents.MeteorDir.x, GameEvents.MeteorDir.y);
    }

    void Update()
    {
        transform.Rotate(RoatationEular);
    }
    private void FixedUpdate()
    {
        xSpeed = Dir.x * Speed * GameEvents.SpeedMultiplier * Time.fixedDeltaTime;
        ySpeed = Dir.y * Speed * GameEvents.SpeedMultiplier * Time.fixedDeltaTime;
        velocity.Set(xSpeed, 0, ySpeed);
        rb.linearVelocity = velocity;
    }
}
