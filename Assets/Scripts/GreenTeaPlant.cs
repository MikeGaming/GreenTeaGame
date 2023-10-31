using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GreenTeaPlant : MonoBehaviour, IInteractable
{

    bool canGrow = true, canHarvest = false;
    static int a = 0;
    [HideInInspector] public bool harvesting, growing;
    [SerializeField] GameObject seed;
    [SerializeField] Bush teaBush;
    PlayerStats stats;
    [SerializeField] Vector3 offset;
    [Header("Bush Size")]
    [SerializeField] float targetSizeMin;
    [SerializeField] float targetSizeMax;
    float targetSize;
    [SerializeField] float spawnSize;
    [Header("Decay Time")]
    [SerializeField] float decayTimeMin;
    [SerializeField] float decayTimeMax;
    [HideInInspector] public float decayTime;
    [Header("Great Leaf Timeframe")]
    [SerializeField] public float greatLeafMin;
    [SerializeField] public float greatLeafMax;
    [Header("Spawn Time")]
    [SerializeField] float spawnTimeMin;
    [SerializeField] float spawnTimeMax;
    float spawnTime;
    [Header("Growth Rate")]
    [SerializeField] float growRateMin;
    [SerializeField] float growRateMax;
    [Header("Bush UI Stuff")]
    [SerializeField] GameObject bushUI;
    [SerializeField] TextMeshProUGUI bushPercent;
    [SerializeField] TextMeshProUGUI bushLeafType;
    Transform playerCam;
    [SerializeField] Color[] bushTextColors;

    private void Start()
    {
        bushUI.SetActive(false);
        teaBush.gameObject.SetActive(false);
        seed.SetActive(false);
        playerCam = Camera.main.transform;
        stats = FindObjectOfType<PlayerStats>();
        targetSize = Random.Range(targetSizeMin, targetSizeMax);
        decayTime = Random.Range(decayTimeMin, decayTimeMax);
        spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
        LayerMask mask = LayerMask.GetMask("whatIsGround");
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, mask))
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                transform.position = hit.point - offset;
            }
        }
        teaBush.transform.localRotation = Quaternion.Euler(-90, 0, Random.Range(0, 360));
        teaBush.transform.localScale = new Vector3(spawnSize, spawnSize, spawnSize);
    }

    private void Update()
    {
        if (teaBush.overgrown)
        {
            bushLeafType.text = "Leaf Type: Bad";
            bushLeafType.color = bushTextColors[0];
        }
        else
        {
            if (!teaBush.isGreatLeaf)
            {
                bushLeafType.text = "Leaf Type: Good";
                bushLeafType.color = bushTextColors[1];
            }
            else
            {
                bushLeafType.text = "Leaf Type: Great";
                bushLeafType.color = bushTextColors[2];
            }
        }
        bushPercent.text = "Growth: " + (int)(teaBush.transform.localScale.z / targetSize * 100) + "%";
        bushUI.transform.LookAt(playerCam);
    }

    public void Interact()
    {
        if (canGrow)
            StartCoroutine(Grow());
        if (teaBush.transform.localScale.z >= 75f && canHarvest)
            StartCoroutine(Harvest());
    }

    IEnumerator Harvest()
    {
        if (a == 1)
        {
            FactsUtility.SetFactText("To keep your shrubs healthy and strong, make sure to harvest the top green leaves and one bud from the flowering part of the plant at each harvest!");
            a = 2;
        }
        harvesting = true;
        teaBush.transform.localScale = new Vector3(teaBush.transform.localScale.x - 5f, teaBush.transform.localScale.y - 5f, teaBush.transform.localScale.z - 5f);
        bushUI.transform.position = new Vector3(bushUI.transform.position.x, bushUI.transform.position.y - 0.1f, bushUI.transform.position.z);

        if (teaBush.overgrown)
        {
            stats.AddLeafCount(0);
        }
        else
        {
            if (!teaBush.isGreatLeaf)
            {
                stats.AddLeafCount(1);
            }
            else
            {
                stats.AddLeafCount(2);
            }
        }

        yield return new WaitForSeconds(0.1f);
        harvesting = false;
        if (!growing)
        {
            StopCoroutine(Grow());
            StartCoroutine(Grow());
        }
    }
    IEnumerator Grow()
    {
        if (teaBush.transform.localScale.z >= targetSize)
        {
            canGrow = false;
            growing = false;
        }
        else
        {
            growing = true;
            if (a == 0)
            {
                FactsUtility.SetFactText("Tea shrubs should not be harvested until they reach three years of growth. Once they reach that point, there are two primary harvests throughout the year - one in the spring and one in the summer.");
                a = 1;
            }
            canGrow = false;
            seed.SetActive(true);
            yield return new WaitForSeconds(spawnTime);
            seed.SetActive(false);
            bushUI.SetActive(true);
            teaBush.gameObject.SetActive(true);

        }
        while (growing)
        {
            if (teaBush.transform.localScale.z >= targetSize)
            {
                growing = false;
                canHarvest = true;
            }
            else
            {
                teaBush.transform.localScale = new Vector3(teaBush.transform.localScale.x + 0.25f, teaBush.transform.localScale.y + 0.25f, teaBush.transform.localScale.z + 0.25f);
                bushUI.transform.position = new Vector3(bushUI.transform.position.x, bushUI.transform.position.y + 0.005f, bushUI.transform.position.z);
                yield return new WaitForSeconds(Random.Range(growRateMin, growRateMax));
            }
        }
    }
}
