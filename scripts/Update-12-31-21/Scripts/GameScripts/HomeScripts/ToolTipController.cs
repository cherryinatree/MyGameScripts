using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ToolTipController
{
    public ToolTips toolTipPanel;
    MainSaveData saveFile;

    public ToolTipController(MainSaveData saveFile)
    {
        this.saveFile = saveFile;
        toolTipPanel = GameObject.Find("PanelToolTip").GetComponent<ToolTips>();
    }


    public void ShowItemStats(GameObject hitObject)
    {
        string name = GetItemInfoText1(hitObject.GetComponent<Image>().sprite.name.Substring(6));
        string info = GetItemInfoText2(hitObject.GetComponent<Image>().sprite.name.Substring(6));
        toolTipPanel.gameObject.transform.Find("Text1").GetComponent<Text>().text = name;
        toolTipPanel.gameObject.transform.Find("Text2").GetComponent<Text>().text = info;
        toolTipPanel.gameObject.transform.Find("Image").GetComponent<Image>().sprite = hitObject.GetComponent<Image>().sprite;
    }

    private string GetItemInfoText1(string item)
    {
        foreach (Item i in saveFile.GeneralInventory.ItemsInInventory)
        {
            if (i.name == item)
            {
                item = i.name;
            }
        }
        return item;
    }
    private string GetItemInfoText2(string item)
    {
        foreach (Item i in saveFile.GeneralInventory.ItemsInInventory)
        {
            if (i.name == item)
            {
                if (i.consumable == false)
                {
                    item = "\nStrength: " + i.benifit0 + "\nWisdom: " + i.benifit1;
                }
            }
        }
        return item;
    }
}
