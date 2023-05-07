using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CarPrinter : MonoBehaviour
{
    public CustomisationManager customisationManager;
    public ColourLibrary colourLibrary;

    public void PrintCarConfigurations()
    {
        using (StreamWriter writer = new StreamWriter("car_data.txt"))
        {
            foreach (GameObject carGO in customisationManager.cars)
            {
                CarObj car = carGO.GetComponent<CarObj>();
                writer.WriteLine($"Car: {car.name}");
                writer.WriteLine($"Handling: {car.handling}");
                writer.WriteLine($"Speed: {car.speed}");
                writer.WriteLine($"Acceleration: {car.acceleration}");
                writer.WriteLine("Part Colors:");

                writer.WriteLine("Body Colors-");
                PrintPartColors(writer, colourLibrary.GetColours(CustomisationManager.CarPart.Body));

                writer.WriteLine("Interior Colors-");
                PrintPartColors(writer, colourLibrary.GetColours(CustomisationManager.CarPart.Interior));

                writer.WriteLine("Windows Colors-");
                PrintPartColors(writer, colourLibrary.GetColours(CustomisationManager.CarPart.Windows));

                writer.WriteLine("Wheels Colors-");
                PrintPartColors(writer, colourLibrary.GetColours(CustomisationManager.CarPart.Wheels));

                writer.WriteLine("Lights Colors-");
                PrintPartColors(writer, colourLibrary.GetColours(CustomisationManager.CarPart.Lights));

                writer.WriteLine("Misc Colors-");
                PrintPartColors(writer, colourLibrary.GetColours(CustomisationManager.CarPart.Misc));

                writer.WriteLine();
            }
        }

        Debug.Log("Car data has been printed to the file.");
    }

    private void PrintPartColors(StreamWriter writer, List<PartColour> colours)
    {
        foreach (PartColour colour in colours)
        {
            writer.WriteLine($"- {colour.name} (Price: £{colour.price})");
        }
    }
}
