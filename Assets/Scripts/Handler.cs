using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Handler : MonoBehaviour
{
    [Tooltip("Currently Held Item")]
    public Item item;

    [Tooltip("The Radius in which the Handler can Pick Up Items")]
    public float pickUpRadius;

    private Item nearbyItem;

    [Tooltip("The Point at which an Item is Held")]
    public Transform holdPoint;

    [Tooltip("Is the Item currently in Use")]
    public bool itemInUse = false;

    [Tooltip("An Button Indicator for the Item")]
    public GameObject indicator;

    private void Update()
    {
        nearbyItem = FindNearbyItem();

        //Display Indicator Above Item
        if(nearbyItem && indicator)
        {
            indicator.GetComponent<ButtonIndicator>().Icon.sprite = GetComponent<PlayerController>().ChangeIndicator("Pick Up");
            indicator.transform.position = nearbyItem.transform.position;
            indicator.SetActive(true);
        }
        else if(indicator)
        {
            indicator.SetActive(false);
        }
    }

    // Picks Up the Closest Item
    public void PickUpItem()
    {      
        if(nearbyItem)
        {
            // Picked Up Item
            item = nearbyItem;
            item.PickUp(this);
            HoldItem(item);
        }
    }

    // Uses the Held Item
    public void UseItem()
    {
        // If No Item is Held or the Item is currently In Use, Do Nothing
        if(!item || itemInUse)
            return;

        item.UseItem();
        itemInUse = true;
    }

    // Drops the Held Item
    public void DropItem()
    {
        // If No Item is Held or the Item is currently In Use, Do Nothing
        if(!item || itemInUse)
            return;

        
        // Unattach Item From Handler
        item.gameObject.transform.SetParent(null);
        //item.gameObject.transform.localScale = item.gameObject.transform.localScale * 1.5f;


        item.Drop();
        item = null;      
    }

    // Attaches the Item to the Handler
    private void HoldItem(Item item)
    {
        item.gameObject.transform.SetParent(gameObject.transform);
        item.gameObject.transform.position = holdPoint.position;       
        //item.gameObject.transform.localScale = item.gameObject.transform.localScale / 1.5f;
    }

    // Returns the Closest Item to the Handler
    private Item FindNearbyItem()
    {
        Collider2D pickUpCollider = Physics2D.OverlapCircle(transform.position, pickUpRadius, 1 << LayerMask.NameToLayer("Item"));
        if(pickUpCollider) // If the Collider is an Item
        {
            Item item = pickUpCollider.gameObject.GetComponent<Item>();
            // Make sure Item isn't already Picked Up
            if(item.pickedUp)
                return null;

            return item;
        }
    
        // No Nearby Items Found
        return null;
    }
}
