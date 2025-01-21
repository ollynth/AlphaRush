using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthManager : MonoBehaviour
{
    public GameObject hearthPrefab;
    private List<HealthHearth> hearts = new List<HealthHearth>();

    public void DrawHearths(int currentHP, int maxHP)
    {
        ClearHearts();

        for (int i = 0; i < maxHP; i++)
        {
            if (i < currentHP)
            {
                CreateFullHearth();
            }
            else
            {
                CreateEmptyHearth();
            }
        }
    }

    public void CreateFullHearth()
    {
        GameObject newHeart = Instantiate(hearthPrefab, transform);
        HealthHearth hearthComponent = newHeart.GetComponent<HealthHearth>();
        hearthComponent.SetHearthImage(HearthStatus.Full);
        hearts.Add(hearthComponent);
    }

    public void CreateEmptyHearth()
    {
        GameObject newHeart = Instantiate(hearthPrefab, transform);
        HealthHearth hearthComponent = newHeart.GetComponent<HealthHearth>();
        hearthComponent.SetHearthImage(HearthStatus.Empty);
        hearts.Add(hearthComponent);
    }

    public void ClearHearts()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts.Clear();
    }
}
