using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Awaken()
    {
        SetScreenSize();
    }

    private void SetScreenSize(){
        Screen.SetResolution(600,600, false);
    }
}
