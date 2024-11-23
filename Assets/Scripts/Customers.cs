using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Customers : MonoBehaviour
{
    public BurgerMaking bm;
    public GameObject[] customer;
    public GameObject Exit;
    public GameObject Burger;
    public GameObject timer;
    bool served = false;
    private void Start()
    {
        TimerOpen();
    }
    private void Update()
    {
        if (bm.burgerCount == 5)
        {
            served = true;
        }
    }
    public void Serve()
    {
        if (served == true)
        {
            timer.GetComponent<Image>().DOFillAmount(0, 0.01f);
            timer.SetActive(false);
            DOTween.Kill(1f,false);
            served = false;
            bm.burgerCount = 0;
            served = false;
            for (int i = 0; i < 5; i++)
            {
                Burger.transform.GetChild(i).gameObject.SetActive(false);
            }
            for (int i = 0; i < customer.Length; i++)
            {
                if (i + 1 == 5) break;
                customer[i + 1].transform.DOMove(customer[i].transform.position, 2);
                customer[i + 1].transform.LookAt(customer[i].transform);

            }
            customer[0].transform.DOMove(Exit.transform.position, 10);
            customer[0].transform.LookAt(Exit.transform);
            TimerOpen();
        }

    }
    public void TimerOpen()
    {
        timer.GetComponent<Image>().DOFillAmount(0, 0.01f);
        timer.SetActive(true);
        timer.GetComponent<Image>().DOFillAmount(100, 80).SetId(1f).OnComplete(() => {
            timer.SetActive(false);
            timer.GetComponent<Image>().DOFillAmount(0, 0.01f);
            served = true;
            Serve();
        });
    }

}
