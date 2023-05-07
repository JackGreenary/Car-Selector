using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CustomisationManager;

public class ColourLibrary : MonoBehaviour
{
    public Color32 lightYellow;
    public Color32 mintGreen;
    public Color32 cactiGreen;
    public Color32 skyBlue;
    public Color32 bloodRed;
    public Color32 lavenderPurple;
    public Color32 rosePink;
    public Color32 transparentGrey;
    public Color32 transparentBlack;
    public Color32 metal;

    public List<PartColour> bodyColours;
    public List<PartColour> interiorColours;
    public List<PartColour> windowsColours;
    public List<PartColour> wheelsColours;
    public List<PartColour> lightsColours;
    public List<PartColour> miscColours;

    public void SetColoursAndPrices()
    {
        // Set hardcoded values for colours and their prices
        bodyColours = new List<PartColour>();
        bodyColours.Add(new PartColour(lightYellow, "Light Yellow", 190));
        bodyColours.Add(new PartColour(mintGreen, "Mint Green", 200));
        bodyColours.Add(new PartColour(cactiGreen, "Cacti Green", 230));
        bodyColours.Add(new PartColour(skyBlue, "Sky Blue", 275));
        bodyColours.Add(new PartColour(bloodRed, "Blood Red", 350));
        bodyColours.Add(new PartColour(lavenderPurple, "Lavender Purple", 390));
        bodyColours.Add(new PartColour(rosePink, "Rose Pink", 420));

        interiorColours = new List<PartColour>();
        bodyColours.Add(new PartColour(mintGreen, "Mint Green", 360));
        interiorColours.Add(new PartColour(rosePink, "Rose Pink", 405));
        interiorColours.Add(new PartColour(bloodRed, "Blood Red", 560));
        interiorColours.Add(new PartColour(cactiGreen, "Cacti Green", 670));

        windowsColours = new List<PartColour>();
        windowsColours.Add(new PartColour(transparentBlack, "Black", 350));
        windowsColours.Add(new PartColour(transparentGrey, "Grey", 405));

        wheelsColours = new List<PartColour>();
        wheelsColours.Add(new PartColour(skyBlue, "Sky Blue", 350));
        wheelsColours.Add(new PartColour(bloodRed, "Blood Red", 390));

        lightsColours = new List<PartColour>();
        lightsColours.Add(new PartColour(lightYellow, "Light Yellow", 190));
        lightsColours.Add(new PartColour(bloodRed, "Blood Red", 390));
        lightsColours.Add(new PartColour(skyBlue, "Sky Blue", 275));

        miscColours = new List<PartColour>();
        miscColours.Add(new PartColour(skyBlue, "Sky Blue", 350));
        miscColours.Add(new PartColour(bloodRed, "Blood Red", 390));
    }

    public List<PartColour> GetColours(CarPart carPart)
    {
        // Get colours for car part
        if (bodyColours == null ||
            interiorColours == null ||
            windowsColours == null ||
            wheelsColours == null ||
            lightsColours == null ||
            miscColours == null)
            SetColoursAndPrices();

        switch (carPart)
        {
            case CarPart.Body:
                return bodyColours;
            case CarPart.Interior:
                return interiorColours;
            case CarPart.Windows:
                return windowsColours;
            case CarPart.Wheels:
                return wheelsColours;
            case CarPart.Lights:
                return lightsColours;
            case CarPart.Misc:
                return miscColours;
            default:
                return null;
        }
    }
}