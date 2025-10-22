using UnityEngine;
using TMPro;

public class PointManager : MonoBehaviour
{
    public PointSystem system; // Reference to your points system
    public TextMeshProUGUI textMeshPro; // Reference to the TextMeshPro component

    void Update()
    {
        // Dynamically update the UI with the current points
        textMeshPro.text = "Points: " + system.points.ToString();
    }
}
