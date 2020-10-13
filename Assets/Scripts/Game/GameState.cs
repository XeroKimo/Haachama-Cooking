using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//V0.12
public class GameState : MonoBehaviour
{
    public static GameState instance { get; private set; }

    Restaurant restaurant;
    float timeLeftTillDayEnds;
    [SerializeField]
    UnityEngine.UI.GraphicRaycaster raycaster;


    const float customerRate = 5.0f;
    private void Awake()
    {
        instance = this;
        restaurant = new Restaurant(new List<Recipe>(Resources.LoadAll<Recipe>("Recipes")));
    }

    private void Start()
    {
        Debug.LogWarning("New customer is invoked with a static value, reminder to be invoked at random intervals instead");
        timeLeftTillDayEnds = GameConstants.restaurantOpenSeconds;

        Invoke("NewCustomer", customerRate);
    }

    // Update is called once per frame
    void Update()
    {
        restaurant.Update();

        timeLeftTillDayEnds -= Time.deltaTime;
        if(timeLeftTillDayEnds <= 0)
        {
            Debug.LogWarning("Day ended");
            CancelInvoke();
            //Handle day end stuff here
        }
    }

    void NewCustomer()
    {
        restaurant.EnterRestaurant(new Customer());

        Invoke("NewCustomer", customerRate);
    }

    public Restaurant GetRestaurant()
    {
        return restaurant;
    }

    public float GetTimeLeftTillDayEnds()
    {
        return timeLeftTillDayEnds;
    }

    public UnityEngine.UI.GraphicRaycaster GetRaycaster()
    {
        return raycaster;
    }
}
