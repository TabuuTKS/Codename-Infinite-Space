using UnityEngine;

//Used for Main Menu Animation
public class StartCam : MonoBehaviour
{
    public bool ShipSelect = false;
    #region Singleton
    public static StartCam Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
}
