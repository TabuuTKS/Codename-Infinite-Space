using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class SpaceShip : MonoBehaviour
{
    public InputSystem_Actions inputActions;

    Rigidbody rigidbody;
    float xdir;
    bool canFire = true, mustfire;

    [SerializeField] float MoveSpeed = 2f;
    [SerializeField] Transform BullitSpawn;
    [SerializeField] GameUI gameUI;
    [SerializeField] GameObject Mesh;
    [SerializeField] ShipObject shipObject;

    GameObject Bullit;
    BasicTimer FireTimer;
    Vector3 velocity;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Player.Move.performed += ctx => xdir = ctx.ReadValue<float>();
        inputActions.Player.Move.canceled += ctx => xdir = 0;
        inputActions.Player.Attack.performed += ctx => mustfire = true;
        inputActions.Player.Attack.canceled += ctx => mustfire = false;
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    public void setXdir(float value) => xdir = value;
    public void setAttack(bool value) => mustfire = value;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        FireTimer = GetComponent<BasicTimer>();

        ShipInit();
    }

    void ShipInit()
    {
        GameObject currentShip = Instantiate(shipObject.SpaceShipMesh, Mesh.transform);
        currentShip.transform.localPosition = Vector3.zero;
        currentShip.transform.localRotation = Quaternion.identity;

        MoveSpeed = shipObject.MoveSpeed;
        FireTimer.WaitTime = shipObject.BullitWaitTime;
    }

    void Update()
    {
        if (mustfire && canFire)
        {
            Bullit = ObjectPool.Instance.SpawnFromPool("Bullit", BullitSpawn.position, BullitSpawn.rotation);
            Bullit.SetActive(true);
            canFire = false;
            FireTimer.StartTimer();
        }
    }

    private void FixedUpdate()
    {
        velocity.Set(xdir * MoveSpeed * Time.fixedDeltaTime, 0, 0);
        rigidbody.linearVelocity = velocity;
    }

    public void SetFiring() { canFire = true; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Collectable"))
        {
            GameEvents.coins++;
            collision.collider.gameObject.SetActive(false);
        }
        else if (collision.collider.CompareTag("Meteor"))
        {
            collision.collider.gameObject.SetActive(false);
            gameUI.GameOver();
            this.gameObject.SetActive(false);
        }
    }
}
