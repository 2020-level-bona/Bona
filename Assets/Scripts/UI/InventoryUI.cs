using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
 public GameObject InventoryUIPanel;
 bool activeInventory = false;

 private void Start()
 {
     InventoryUIPanel.SetActive(activeInventory);
 }

 private void Update()
 {
     if(Input.GetKeyDown(KeyCode.I))
     {
         activeInventory = !activeInventory;
         InventoryUIPanel.SetActive(activeInventory);
     }
 }
}
