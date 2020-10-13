using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CustomerUI : MonoBehaviour
{
    Customer customer;
    [SerializeField]
    TextMeshProUGUI recipeName;
    [SerializeField]
    Image image;
    [SerializeField]
    TextMeshProUGUI timerText;
    [SerializeField]
    Slider timingBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetCustomer(Customer customer)
    {
        this.customer = customer;
        gameObject.SetActive(customer != null);

        if(customer == null)
            return;


        recipeName.text = customer.order.foodName;

        image.sprite = customer.order.sprite;
    }

    public Customer GetCustomer()
    {
        return customer;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        timerText.text = customer.timeRemaining.ToString("F0") + "s";
        timingBar.value = (customer.timeRemaining / GameConstants.customerWaitTime);
    }
}
