using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices.WindowsRuntime;

//V0.11
public class Customer
{
    //What food the customer ordered
    public Food order;

    //Determines how much time this customer
    //has left before leaving
    public float timeRemaining;
}

//V0.1
public class PreparingFood
{
    public Recipe recipe;
    public float timeRemaining;

    public PreparingFood(Recipe recipe)
    {
        this.recipe = recipe;
        timeRemaining = recipe.timeToPrepare;
    }

}

//V0.12
public class Restaurant
{
    List<Customer> customers;
    List<Recipe> recipes;
    List<PreparingFood> foodsBeingPrepared;

    public event Action<Customer> OnCustomerEntered;
    public event Action<Customer> OnCustomerServed;
    public event Action<Customer> OnCustomerLeave;

    public event Action<PreparingFood> OnFoodStartPreparing;
    public event Action<PreparingFood> OnFoodCanceledPreparing;
    public event Action<PreparingFood> OnFoodFinishedPreparing;

    public Restaurant(List<Recipe> recipes)
    {
        customers = new List<Customer>(GameConstants.maxCustomerCount);
        this.recipes = new List<Recipe>(recipes);
        recipes.Sort((Recipe lh, Recipe rh) => lh.timeToPrepare.CompareTo(rh.timeToPrepare));
        foodsBeingPrepared = new List<PreparingFood>(GameConstants.maxPrepareFoodCount);
    }

    //Check to see if our restaurant has room to serve the customer,
    //If we do, take the customer's order and invoke OnCustomerEnetered event
    public bool EnterRestaurant(Customer customer)
    {
        if(customers.Count >= GameConstants.maxCustomerCount)
            return false;

        customers.Add(customer);
        customer.order = recipes[UnityEngine.Random.Range(0, recipes.Count)].food;
        customer.timeRemaining = GameConstants.customerWaitTime;

        OnCustomerEntered?.Invoke(customer);

        return true;
    }

    //Check to see if we have room to prepare the recipe,
    //If we do, start preparing the the food and invoke OnFoodStartPreparing event
    public bool PrepareFood(Recipe recipe)
    {
        if(foodsBeingPrepared.Count >= GameConstants.maxPrepareFoodCount)
            return false;

        foodsBeingPrepared.Add(new PreparingFood(recipe));
        OnFoodStartPreparing?.Invoke(foodsBeingPrepared[foodsBeingPrepared.Count - 1]);

        return true;
    }

    //Find and remove the food we wish to stop preparing
    //Invoke OnFoodCanceledPreparing if we do
    public bool StopPreparingFood(PreparingFood food)
    {
        if(foodsBeingPrepared.Remove(food))
        {
            OnFoodCanceledPreparing?.Invoke(food);
            return true;
        }

        return false;
    }


    //Finds a customer to serve the item to
    public bool ServeFood(PreparingFood food)
    {
        //Find all customers with our given item
        List<Customer> waitingCustomers = customers.FindAll((Customer customer) => customer.order == food.recipe.food);

        if(waitingCustomers.Count >= 1)
            return ServeFood(food, GetLongestWaitingCustomer(waitingCustomers));

        return false;
    }

    public bool ServeFood(PreparingFood food, Customer customer)
    {
        if(customer.order != food.recipe.food)
            return false;

        //Remove the customer from our list, and fire the event
        customers.Remove(customer);
        OnCustomerServed?.Invoke(customer);

        //Remove the item for our list and fire the event
        foodsBeingPrepared.Remove(food);
        OnFoodFinishedPreparing?.Invoke(food);

        return true;
    }

    public void Update()
    {
        //Create a tempororary list for foods that will be served
        List<PreparingFood> foodsToServe = new List<PreparingFood>(foodsBeingPrepared.Count);
        foreach(PreparingFood food in foodsBeingPrepared)
        {
            food.timeRemaining -= Time.deltaTime;
            food.timeRemaining = Mathf.Max(0, food.timeRemaining);
        }


        List<Customer> leavingCustomers = new List<Customer>(customers.Count);
        foreach(Customer customer in customers)
        {
            customer.timeRemaining -= Time.deltaTime;
            if(customer.timeRemaining <= 0)
                leavingCustomers.Add(customer);
        }

        foreach(Customer customer in leavingCustomers)
        {
            customers.Remove(customer);
            OnCustomerLeave?.Invoke(customer);
        }
    }

    public List<Recipe> GetMenu()
    {
        return recipes;
    }

    Customer GetLongestWaitingCustomer(List<Customer> waitingCustomers)
    {
        if(waitingCustomers.Count == 0)
            return null;
        else if(waitingCustomers.Count == 1)
            return waitingCustomers[0];

        Customer customer = waitingCustomers[0];
        float closestToZero = float.MaxValue;

        for(int i = 1; i < waitingCustomers.Count; i++)
        {
            if(waitingCustomers[i].timeRemaining < closestToZero)
            {
                closestToZero = waitingCustomers[i].timeRemaining;
                customer = waitingCustomers[i];
            }
        }

        return customer;
    }
}
