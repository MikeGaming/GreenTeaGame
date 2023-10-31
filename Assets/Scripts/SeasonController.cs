using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SeasonController : MonoBehaviour
{
    [SerializeField] Volume volume;
    [SerializeField] VolumeProfile[] seasons;
    [SerializeField] GameObject snow;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                volume.sharedProfile = seasons[0];
                snow.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                volume.sharedProfile = seasons[1];
                snow.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                volume.sharedProfile = seasons[2];
                snow.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                volume.sharedProfile = seasons[3];
                snow.SetActive(false);
            }
        }
    }
}
