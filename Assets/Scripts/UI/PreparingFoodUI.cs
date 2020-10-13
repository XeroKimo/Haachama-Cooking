using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

//V0.1
public class PreparingFoodUI : MonoBehaviour, IDragHandler, IEndDragHandler
{
    PreparingFood food;


    [SerializeField]
    Slider progressBar;
    [SerializeField]
    TextMeshProUGUI timerText;
    [SerializeField]
    Image image;

    Vector3 initialPos;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Raycast at our current position
        List<RaycastResult> output = new List<RaycastResult>();
        GameState.instance.GetRaycaster().Raycast(eventData, output);

        //Check to see if we're dropping where customer is, or where trash is
        foreach(RaycastResult result in output)
        {
            if(result.gameObject.tag == "Customer" && food.timeRemaining <= 0)
            {
                //If it's a customer, attempt to serve it off
                if(GameState.instance.GetRestaurant().ServeFood(food, result.gameObject.GetComponent<CustomerUI>().GetCustomer()))
                {
                    SetPreparingFood(null);
                    break;
                }
            }
            else if(result.gameObject.tag == "Trash")
            {
                //If it's the trash can, attempt to stop preparing the food
                if(GameState.instance.GetRestaurant().StopPreparingFood(food))
                {
                    SetPreparingFood(null);
                    break;
                }
            }
        }

        //Reset our position
        transform.position = initialPos;
    }

    public void SetPreparingFood(PreparingFood food)
    {
        this.food = food;
        gameObject.SetActive(food != null);

        if(food == null)
            return;
        image.sprite = food.recipe.food.sprite;
    }

    public PreparingFood GetPreparingFood()
    {
        return food;
    }

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(food != null)
        {
            progressBar.value = 1 - food.timeRemaining / food.recipe.timeToPrepare;
            timerText.text = food.timeRemaining.ToString("F0") + "s";
        }
    }
}
