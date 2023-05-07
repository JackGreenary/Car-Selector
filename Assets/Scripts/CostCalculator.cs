using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static CustomisationManager;

public class CostCalculator : MonoBehaviour
{
    // Variables to store the cost text and cost value
    [SerializeField] private TextMeshProUGUI costTxt;
    [SerializeField] public float costVal;

    // Variables to store the selected colors for each car part
    private PartColour bodyColourSet;
    private PartColour interiorColourSet;
    private PartColour windowColourSet;
    private PartColour wheelColourSet;
    private PartColour lightColourSet;
    private PartColour miscColourSet;

    // Update the cost text when it changes
    void Update()
    {
        // Only update the cost text if it has changed
        if (costTxt.text != costVal.ToString())
        {
            costTxt.text = $"£{costVal.ToString()}";
        }
    }

    // Update the cost based on the selected car part and color
    public void UpdateCost(CarPart carPart, PartColour partColour)
    {
        switch (carPart)
        {
            case CarPart.Body:
                // Subtract the old color's price if it was set
                if (bodyColourSet != null)
                {
                    costVal -= bodyColourSet.price;
                }
                // Set the new color for the body
                bodyColourSet = partColour;
                // Add the price of the new color to the total cost
                costVal += bodyColourSet.price;
                break;

            case CarPart.Interior:
                if (interiorColourSet != null)
                {
                    costVal -= interiorColourSet.price;
                }
                interiorColourSet = partColour;
                costVal += interiorColourSet.price;
                break;

            case CarPart.Windows:
                if (windowColourSet != null)
                {
                    costVal -= windowColourSet.price;
                }
                windowColourSet = partColour;
                costVal += windowColourSet.price;
                break;

            case CarPart.Wheels:
                if (wheelColourSet != null)
                {
                    costVal -= wheelColourSet.price;
                }
                wheelColourSet = partColour;
                costVal += wheelColourSet.price;
                break;

            case CarPart.Lights:
                if (lightColourSet != null)
                {
                    costVal -= lightColourSet.price;
                }
                lightColourSet = partColour;
                costVal += lightColourSet.price;
                break;

            case CarPart.Misc:
                if (miscColourSet != null)
                {
                    costVal -= miscColourSet.price;
                }
                miscColourSet = partColour;
                costVal += miscColourSet.price;
                break;

            default:
                break;
        }
    }

    public void ResetCost()
    {
        // Clear cost values
        costVal = 0;

        // Clear colours
        bodyColourSet = null;
        interiorColourSet = null;
        windowColourSet = null;
        wheelColourSet = null;
        lightColourSet = null;
        miscColourSet = null;
    }
}
