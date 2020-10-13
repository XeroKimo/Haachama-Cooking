using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//V0.1
public class RestaurantUI : MonoBehaviour
{
    [SerializeField]
    CustomerUI[] customerUIs;
    [SerializeField]
    PreparingFoodUI[] preparingFoodUIs;

    // Start is called before the first frame update
    void Start()
    {
        GameState.instance.GetRestaurant().OnCustomerEntered += CustomerEnetered;
        GameState.instance.GetRestaurant().OnCustomerServed += CustomerServed;
        GameState.instance.GetRestaurant().OnCustomerLeave += CustomerLeave;

        GameState.instance.GetRestaurant().OnFoodStartPreparing += FoodPreparing;
    }

    //Finds a preparingFoodUI that's empty and sets it to the given value
    private void FoodPreparing(PreparingFood food)
    {
        foreach(PreparingFoodUI foodUI in preparingFoodUIs)
        {
            if(foodUI.GetPreparingFood() == null)
            {
                foodUI.SetPreparingFood(food);
            }
        }
    }

    //Finds a customerUI that's empty and sets it to the given value
    private void CustomerEnetered(Customer customer)
    {
        foreach(CustomerUI customerUI in customerUIs)
        {
            if(customerUI.GetCustomer() == null)
            {
                customerUI.SetCustomer(customer);
                break;
            }
        }
    }

    //Finds a customerUI that's matches the value and nulls it
    private void CustomerServed(Customer customer)
    {
        foreach(CustomerUI customerUI in customerUIs)
        {
            if(customerUI.GetCustomer() == customer)
            {
                customerUI.SetCustomer(null);
                break;
            }
        }
    }

    //Finds a customerUI that's matches the value and nulls it
    private void CustomerLeave(Customer customer)
    {
        foreach(CustomerUI customerUI in customerUIs)
        {
            if(customerUI.GetCustomer() == customer)
            {
                customerUI.SetCustomer(null);
                break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
