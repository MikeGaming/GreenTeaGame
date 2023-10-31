using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FactsUtility
{
    public static string factTextContent = string.Empty;

    public static void SetFactText(string text)
    {
        factTextContent = text;
    }

    public static string GetFactText()
    {
        return factTextContent;
    }
}
