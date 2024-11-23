using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using System.Xml.Serialization;
using UnityEngine.UI;
public class PickAndDrop : MonoBehaviour
{
    public BurgerMaking bm;

    private GameObject nearsetObject;
    private GameObject pickedObject;
    

    private GameObject[] interact;
    public GameObject[] foodPrefabs;

    public GameObject pickButton;
    public GameObject dropButton;
    public GameObject cookButton;
    public GameObject cutButton;
    public GameObject bellButton;

    public GameObject JoystickOutline;
    public GameObject Joystick;
    public GameObject loadCircle;

    public GameObject Oven;
    public GameObject okCircle;
    public GameObject handsPos;
    public GameObject CutingBorad;

    Vector3 currentPos;
    //Vector3 handPos;
    public GameObject firstColor;
    public GameObject secondColor;

    float minDist;
    float minGlobal = 3f;
    float jumpSpeed = 0.4f;
    
    bool pressed;
    bool Droppable;
    bool handsFull;
    bool cooked;

    private void Start()
    {
        
    }
    private void FixedUpdate()
    {
        interact = GameObject.FindGameObjectsWithTag("Interact");
        NearestObject();

        if (nearsetObject != null)
        {
            CheckForButton();
            if (pressed == true && nearsetObject.layer == 3 && handsFull == false)
            {
                pressed = false;
                if (nearsetObject.name == "Basket_tomato") PickSource(0);
                if (nearsetObject.name == "Basket_lettuce") PickSource(1);
                if (nearsetObject.name == "Basket_meat") PickSource(2);
                if (nearsetObject.name == "Basket_bun") PickSource(3);
                if (nearsetObject.name == "Basket_cheese") PickSource(4);
            }
            else if (pressed == true && nearsetObject.layer == 7 && handsFull == false && cooked == true)
            {
                pressed = false;
                PickSource(6);
            }
            else if (pressed == true && nearsetObject.layer != 3 && handsFull == false)
            {
                pressed = false;
                Pick();
            }
        }
        else if (nearsetObject == null && handsFull == false)
        {
            pickButton.SetActive(false);
            dropButton.SetActive(false);
            cookButton.SetActive(false);
            cutButton.SetActive(false);
            bellButton.SetActive(false);
        }

    }
    
    private void CheckForButton()
    {
        if (handsFull == false && nearsetObject.layer != 6 && nearsetObject.layer != 9)
        {
            pickButton.SetActive(true);
            dropButton.SetActive(false);
            cookButton.SetActive(false);
            cutButton.SetActive(false);
            bellButton.SetActive(false);
        }
        else if (handsFull == true && nearsetObject.layer == 6)
        {
            pickButton.SetActive(false);
            dropButton.SetActive(true);
            cookButton.SetActive(false);
            cutButton.SetActive(false);
            bellButton.SetActive(false);
        }
        else if (handsFull == true && nearsetObject.layer == 7 && pickedObject.name =="kofte(Clone)")
        {
            pickButton.SetActive(false);
            dropButton.SetActive(false);
            cookButton.SetActive(true);
            cutButton.SetActive(false);
            bellButton.SetActive(false);
        }
        else if (handsFull == true && nearsetObject.layer == 8 && (pickedObject.name == "Lettuce(Clone)" || pickedObject.name == "DOMATES(Clone)" ))
        {
            pickButton.SetActive(false);
            dropButton.SetActive(false);
            cookButton.SetActive(false);
            cutButton.SetActive(true);
            bellButton.SetActive(false);
        }
        else if (handsFull == false && nearsetObject.layer == 9)
        {
            pickButton.SetActive(false);
            dropButton.SetActive(false);
            cookButton.SetActive(false);
            cutButton.SetActive(false);
            bellButton.SetActive(true);
        }
        else
        {
            pickButton.SetActive(false);
            dropButton.SetActive(false);
            cookButton.SetActive(false);
            cutButton.SetActive(false);
            bellButton.SetActive(false);
        }

    }
    private void NearestObject()
    {
        currentPos = transform.position;
        minDist = minGlobal;
        foreach (GameObject o in interact)
        {
            float dist = Vector3.Distance(o.transform.position, currentPos);
            if (dist<minDist && dist <minGlobal)
            {
                nearsetObject = o;
                minDist = dist;
            }
        }
        if (nearsetObject != null && Vector3.Distance(nearsetObject.transform.position, currentPos) > minGlobal)
        {
            nearsetObject = null;
        }
    }
    public void Press()
    {
        pressed = true;
    }
    private void PickSource(int type)
    {
        if (type == 6)
        {
            cooked = false;
            okCircle.SetActive(false);
            okCircle.GetComponent<Image>().DOFillAmount(0, 0.1f);
            foodPrefabs[5].SetActive(false);
            Color b = firstColor.GetComponent<Renderer>().material.GetColor("_Color");
            firstColor.GetComponent<Renderer>().material.DOColor(b, 0.1f);
        }
        handsFull = true;
        GameObject obj = Instantiate(foodPrefabs[type], nearsetObject.transform);
        //handPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        obj.transform.DOJump(handsPos.transform.position, 2, 1, jumpSpeed).OnComplete(()=> { });
        obj.transform.SetParent(this.gameObject.transform);
        //obj.transform.position = handsPos.transform.position;
        pickedObject = obj;
    }
    private void Pick()
    {
        handsFull = true;
    }
    public void Drop()
    {
        handsFull = false;
        if (nearsetObject.layer == 6)
        {
            pickedObject.transform.DOJump(nearsetObject.transform.position, 2, 1,jumpSpeed);
            pickedObject.transform.parent = null;
        }
    }
    public void CookMeat()
    {
        Oven.tag = "Untagged";
        pickButton.SetActive(false);
        handsFull = false;
        pickedObject.transform.DOJump(foodPrefabs[5].transform.position, 2, 1, jumpSpeed).OnComplete(()=> { foodPrefabs[5].SetActive(true); Destroy(pickedObject); pickedObject = null;}) ;
        okCircle.SetActive(true);
        okCircle.transform.GetComponent<Image>().DOFillAmount(100, 20).OnComplete(()=> { Oven.tag = "Interact"; cooked = true; });
        pickedObject.transform.parent = null;
        Color b = secondColor.GetComponent<Renderer>().material.GetColor("_Color");
        foodPrefabs[5].GetComponent<Renderer>().material.DOColor(b, 20);
    }
    public void CutVegetable()
    {
        pickedObject.transform.DOJump(CutingBorad.transform.position, 2, 1, jumpSpeed);
        cutButton.SetActive(false);
        Joystick.SetActive(false);
        JoystickOutline.SetActive(false);
        loadCircle.SetActive(true);
        loadCircle.GetComponent<Image>().DOFillAmount(100, 4).OnComplete(() =>
        {
            Joystick.SetActive(true);
            JoystickOutline.SetActive(true);
            pickedObject.SetActive(false);
            if (pickedObject.name == "DOMATES(Clone)")
            {
                GameObject obj = Instantiate(foodPrefabs[7], CutingBorad.transform);
                obj.transform.DOJump(handsPos.transform.position, 2, 1, jumpSpeed);
                pickedObject = obj;
            }
            else
            {
                GameObject obj = Instantiate(foodPrefabs[8], CutingBorad.transform);
                obj.transform.DOJump(handsPos.transform.position, 2, 1, jumpSpeed);
                pickedObject = obj;
            }
            loadCircle.GetComponent<Image>().DOFillAmount(0, 0.1f);
            pickedObject.transform.SetParent(this.gameObject.transform);
        });

    }
}
