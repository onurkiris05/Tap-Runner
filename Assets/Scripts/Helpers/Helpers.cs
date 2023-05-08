using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    // Dictionary to store WaitForSeconds instances based on the specified seconds 
    // And helper method to improve WaitForSeconds performance and avoid unnecessary object creation
    private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new();
    public static WaitForSeconds BetterWaitForSeconds(float seconds)
    {
        // Check if WaitForSeconds instance for the specified seconds exists in the dictionary
        if (!WaitDictionary.TryGetValue(seconds, out var wait))
        {
            wait = new WaitForSeconds(seconds);
            WaitDictionary.Add(seconds, wait);
        }

        return wait;
    }

    
    public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
    {
        // Convert the screen position of the canvas element to local position
        RectTransformUtility.ScreenPointToLocalPointInRectangle(element, element.position,
            Camera.main, out var localPoint);
        
        return localPoint;
    }
}