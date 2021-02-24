using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Inventory inventoryPrefab;

    Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Instantiate(inventoryPrefab);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {

            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;

            if (hitObject != null)
            {
                bool shouldDisappear = false;
                
                print("Hit: " + hitObject.objectName);

                switch(hitObject.itemType)
                {
                    case Item.ItemType.WATERFAIRY:

                    shouldDisappear = inventory.AddItem(hitObject);

                    shouldDisappear = true;
                    break;

                    default:
                        break;
                }
                collision.gameObject.SetActive(false);
            }
           
        }
    }
}
