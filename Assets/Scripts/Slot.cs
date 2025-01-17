﻿using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[System.Serializable]
public class Slot : MonoBehaviour
{
    public int id;
    public int type;
    public Item currentItem;
    public SlotState state = SlotState.Empty;

    public void CreateItem(int id) 
    {
        var itemGO = (GameObject)Instantiate(Resources.Load("Prefabs/Item"));
        
        itemGO.transform.SetParent(this.transform);
        itemGO.transform.localPosition = Vector3.zero;
        itemGO.transform.localScale = Vector3.one;

        currentItem = itemGO.GetComponent<Item>();
        currentItem.Init(id, this);

        ChangeStateTo(SlotState.Full);
    }

    public void CreateItem3D(int id)
    {
        var itemGO = (GameObject)Instantiate(Resources.Load("Prefabs/Item3D"));
        
        itemGO.transform.SetParent(this.transform);
        itemGO.transform.localPosition = Vector3.zero;
        //itemGO.transform.localRotation = Quaternion.Euler(-90,0,0);
        itemGO.transform.localScale = Vector3.one;

        currentItem = itemGO.GetComponent<Item>();

        var modelObj = currentItem.Init3D(id, this, type);
        var model = Instantiate(modelObj, Vector3.zero, itemGO.transform.rotation, itemGO.transform);
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = quaternion.Euler(0, 0, 0);
        model.transform.localScale = Vector3.one / 2;
        
        ChangeStateTo(SlotState.Full);
    }

    private void ChangeStateTo(SlotState targetState)
    {
        state = targetState;
    }

    public void ItemGrabbed()
    {
        Destroy(currentItem.gameObject);
        ChangeStateTo(SlotState.Empty);
    }

    private void ReceiveItem(int id)
    {
        switch (state)
        {
            case SlotState.Empty: 

                CreateItem(id);
                ChangeStateTo(SlotState.Full);
                break;

            case SlotState.Full: 
                if (currentItem.id == id)
                {
                    //Merged
                    Debug.Log("Merged");
                }
                else
                {
                    //Push item back
                    Debug.Log("Push back");
                }
                break;
        }
    }
}

public enum SlotState
{
    Empty,
    Full
}