using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColourBtn : MonoBehaviour
{
    public string part;

    private Color32 colour;
    private string price;
    private Button button;

    private PartColour colourObj;

    void Start()
    {
        // Initialize variables
        colour = GetComponent<Image>().color;
        price = GetComponentInChildren<TextMeshProUGUI>().text;
        button = this.GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        // When a colour is clicked perform the SelectColour method
        FindAnyObjectByType<CustomisationManager>().SelectColour(part, colour, float.Parse(price, CultureInfo.InvariantCulture.NumberFormat));
    }
}
