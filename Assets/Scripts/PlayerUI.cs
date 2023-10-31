using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI badLeafCount, goodLeafCount, greatLeafCount;
    [SerializeField] PlayerStats stats;
    [SerializeField] GameObject funFacts;
    [SerializeField] TextMeshProUGUI factText;
    bool funFactsActive, funFactsNotActive;
    bool a, b;

    private void Start()
    {
        FactsUtility.SetFactText(string.Empty);
    }

    void Update()
    {
        badLeafCount.text = "Bad Leaves: " + stats.GetLeafCount(0);
        goodLeafCount.text = "Good Leaves: " + stats.GetLeafCount(1);
        greatLeafCount.text = "Great Leaves: " + stats.GetLeafCount(2);
        if (FactsUtility.GetFactText() != string.Empty)
        {
            if (funFactsActive)
            {
                funFacts.GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(funFacts.GetComponent<RectTransform>().anchoredPosition, new Vector3(0, 0, 0), Time.deltaTime * 500f);
            }
            else if (funFactsNotActive)
            {
                funFacts.GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(funFacts.GetComponent<RectTransform>().anchoredPosition, new Vector3(500f, -500f, 0), Time.deltaTime * 500f);
            }
            else
            {
                StartCoroutine(FunFactsAtFreddies());
            }
        }
        if ((stats.GetLeafCount(2) >= 50f || stats.GetLeafCount(1) >= 50f) && !a)
        {
            FactsUtility.SetFactText("The Camellia sinensis plant is the primary tea plant used to make all the traditional tea blends we love - from Japanese Green tea to Chinese Green tea, black tea, and white tea!");
            a = true;
        }
        if (stats.GetLeafCount(0) >= 50f && !b)
        {
            FactsUtility.SetFactText("Growing tea at high altitude results in a tea with an unparalleled aroma, flavour, and character. Additionally, these teas have higher antioxidant levels, a brighter liquor, and better flavour characteristics than teas grown at lower altitudes.");
            b = true;
        }
    }

    IEnumerator FunFactsAtFreddies()
    {
        yield return new WaitForSeconds(5f);
        funFactsActive = true;
        factText.text = FactsUtility.GetFactText();
        funFactsActive = true;
        yield return new WaitForSeconds(10f);
        funFactsNotActive = true;
        funFactsActive = false;
        yield return new WaitForSeconds(5f);
        FactsUtility.SetFactText(string.Empty);
        funFactsNotActive = false;
    }
}
