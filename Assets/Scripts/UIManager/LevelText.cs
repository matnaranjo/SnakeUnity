using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelText : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        slider.onValueChanged.AddListener(delegate {TextControl();});           //TextControl happens everytime the value changes in the slider
    }

    /// <summary>
    /// Takes the value in the slider , transforms it to string and assigns it to the visual TMPro element.
    /// </summary>
    private void TextControl(){
        text.text = (slider.value ).ToString();
    }
}
