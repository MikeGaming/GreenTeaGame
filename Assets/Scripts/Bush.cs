using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Bush : MonoBehaviour, IInteractable
{
    GreenTeaPlant plant;
    [SerializeField] Material originalMat;
    [HideInInspector] public bool overgrown;
    [HideInInspector] public bool isGreatLeaf;
    [HideInInspector] public Material _mat;
    Color col = Color.green;
    float t = 0;

    private void Start()
    {
        _mat = Instantiate(originalMat as Material);
        GetComponent<Renderer>().material = _mat;
        plant = GetComponentInParent<GreenTeaPlant>();
    }

    public void Interact()
    {
        plant.Interact();
    }

    private void Update()
    {
        if (plant.harvesting || plant.growing)
        {
            t = 0;
        }
        t += Time.deltaTime;
        if (t > plant.decayTime)
        {
            col = Color.Lerp(col, Color.yellow, Time.deltaTime);
            _mat.SetColor("_BaseColor", col);
            overgrown = true;
        }
        if (t > plant.greatLeafMin && t < plant.greatLeafMax)
        {
            isGreatLeaf = true;
        }
        else
        {
            isGreatLeaf = false;
        }
    }
}
