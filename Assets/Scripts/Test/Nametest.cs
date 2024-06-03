using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Nametest : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI names;
    // Start is called before the first frame update
    void Start()
    {
        names.text = PlayerPrefs.GetString("userid") +"     "+ PlayerPrefs.GetString("username");
    }

}
