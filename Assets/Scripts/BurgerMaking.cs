using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerMaking : MonoBehaviour
{
    public GameObject[] burger;
    public int burgerCount=0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "slicedTomato(Clone)")
        {
            burger[1].SetActive(true);
            other.gameObject.SetActive(false);
            burgerCount++;
        }
        else if (other.gameObject.name == "köfte_pismis(Clone)")
        {
            burger[2].SetActive(true);
            other.gameObject.SetActive(false);
            burgerCount++;
        }
        else if (other.gameObject.name == "MARUL(Clone)")
        {
            burger[4].SetActive(true);
            other.gameObject.SetActive(false);
            burgerCount++;
        }
        else if (other.gameObject.name == "Ekmek(Clone)")
        {
            burger[0].SetActive(true);
            other.gameObject.SetActive(false);
            burgerCount++;
        }
        else if (other.gameObject.name == "cheese(Clone)")
        {
            burger[3].SetActive(true);
            other.gameObject.SetActive(false);
            burgerCount++;
        }
        else
        {
            other.gameObject.transform.DOMoveY(-100, 1);
        }
        

    }
}
