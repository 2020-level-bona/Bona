﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int index {get; private set;}

    public Text qtyText;

    public Image itemImage;

    public Button slotButton;

    public Image bgImage;
    public Image arrowImage;

    ISlotListener listener;

    public void Initialize(int index, ISlotListener listener) {
        this.index = index;
        this.listener = listener;

        qtyText.enabled = false;
        itemImage.enabled = false;
        bgImage.enabled = false;
        arrowImage.enabled = false;

        slotButton.onClick.AddListener(OnClick);
    }

    void OnClick() {
        listener.OnSlotClick(index);
    }

    public void DrawItem(Item item) {
        if (item == null) {
            EraseItem();
            return;
        }

        if (item.IsStackable()) {
            qtyText.enabled = true;
            qtyText.text = item.quantity.ToString();
        } else {
            qtyText.enabled = false;
        }

        itemImage.enabled = true;
        itemImage.sprite = item.GetSprite();
    }

    public void SetSelected(bool selected) {
        bgImage.enabled = selected;
        arrowImage.enabled = selected;
    }

    public void EraseItem() {
        qtyText.enabled = false;
        itemImage.enabled = false;
    }
}
