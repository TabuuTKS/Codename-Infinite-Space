using UnityEngine;
using UnityEngine.Events;
public class BasicTimer : MonoBehaviour
{
    public float WaitTime = 1.0f;
    [SerializeField] bool OneShot = false;
    [SerializeField] bool AutoStart = true;
    public UnityEvent OnTimeout;

    private float timeleft;
    private bool running = false;

    void Start()
    {
        if (AutoStart)
        {
            StartTimer();
        }
    }

    void Update()
    {
        if (running && !(timeleft <= 0f))
        {
            timeleft -= Time.deltaTime;
            if (timeleft <= 0f)
            {
                //? is used to check if the object is not null
                OnTimeout?.Invoke();
                if (OneShot)
                {
                    StopTimer();
                }
                else
                {
                    timeleft = WaitTime;
                }
            }
        }
    }

    public void StartTimer()
    {
        timeleft = WaitTime;
        running = true;
    }

    public void StopTimer()
    {
        running = false;
    }

    public bool isRunning()
    {
        return running;
    }
}