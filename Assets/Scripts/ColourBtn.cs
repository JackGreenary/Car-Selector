using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColourBtn : MonoBehaviour
{
    public string part;

    private Color32 colour;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        colour = GetComponent<Image>().color;
        button = this.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClick()
    {
        FindAnyObjectByType<CustomisationManager>().SelectColour(part, colour);
    }
}
