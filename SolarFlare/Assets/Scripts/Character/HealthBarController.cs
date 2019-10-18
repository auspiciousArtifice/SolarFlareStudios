/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    private GameObject[] heartContainers;
    private Image[] heartFills;

    public Transform heartsParent;
    public GameObject heartContainerPrefab;

    private void Start()
    {
        // Should I use lists? Maybe :)
        heartContainers = new GameObject[(int)PlayerHealth.Instance.MaxHealth];
        heartFills = new Image[(int)PlayerHealth.Instance.MaxHealth];

        PlayerHealth.Instance.onHealthChangedCallback += UpdateHeartsHUD;
        InstantiateHeartContainers();
        UpdateHeartsHUD();
    }

    public void UpdateHeartsHUD()
    {
        SetHeartContainers();
        SetFilledHearts();
    }

    void SetHeartContainers()
    {
        for (int i = 0; i < heartContainers.Length; i++)
        {
            if (i < PlayerHealth.Instance.MaxHealth)
            {
                heartContainers[i].SetActive(true);
            }
            else
            {
                heartContainers[i].SetActive(false);
            }
        }
    }

    void SetFilledHearts()
    {
        for (int i = 0; i < heartFills.Length; i++)
        {
            if (i < PlayerHealth.Instance.Health)
            {
                heartFills[i].fillAmount = 1;
            }
            else
            {
                heartFills[i].fillAmount = 0;
            }
        }

        if (PlayerHealth.Instance.Health % 1 != 0)
        {
            int lastPos = Mathf.FloorToInt(PlayerHealth.Instance.Health);
            heartFills[lastPos].fillAmount = PlayerHealth.Instance.Health % 1;
        }
    }

    void InstantiateHeartContainers()
    {
        for (int i = 0; i < PlayerHealth.Instance.MaxHealth; i++)
        {
            GameObject temp = Instantiate(heartContainerPrefab);
            temp.transform.SetParent(heartsParent, false);
            heartContainers[i] = temp;
            heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();
        }
    }
}
