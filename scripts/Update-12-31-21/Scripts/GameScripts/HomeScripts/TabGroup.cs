using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    
    public List<TabButton> tabButtons;
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    public TabButton selectedTab;
    public GameObject objectContainer;
    public List<GameObject> objectsToSwap;


    public void Subscribe(TabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            button.background.sprite = tabHover;
        }
        
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }
    public void OnTabSelected(TabButton button)
    {

        ViewPanelFunctions view = new ViewPanelFunctions();
        if(objectContainer != null)
        {
            if(objectContainer.activeSelf == false)
            {
                objectContainer.SetActive(true);
            }
        }

        selectedTab = button;
        ResetTabs();
        //button.background.sprite = tabActive;
        int index = button.transform.GetSiblingIndex();

        for (int i = 0; i< objectsToSwap.Count; i++)
        {
            if(i == index)
            {
                objectsToSwap[i].SetActive(true);

                view.PanelChange(objectsToSwap[i]);
                TabPanelFirstChildPage(button.gameObject, objectsToSwap[i]);
                ClearAndSetAll();
            }
            else
            {
                objectsToSwap[i].SetActive(false);

                if (this.name == "InventoryTabGroup" && objectsToSwap[i].name == "Equip")
                {
                        objectsToSwap[i].SetActive(true);
                }
            }
        }

    }

    private void TabPanelFirstChildPage(GameObject buttonPressed, GameObject PannelShown)
    {
        if (buttonPressed.transform.parent.name == "TabPanel" && buttonPressed.name != "tab3")
        {
            PannelShown.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TabButton>().tabGroup.OnTabSelected(PannelShown.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TabButton>());
        }
    }
    /*
    private void TabPanelSuccess(GameObject a)
    {

        ViewPanelFunctions view = new ViewPanelFunctions();
        

        if (objectContainer != null)
        {
            if (objectContainer.activeSelf == false)
            {
                objectContainer.SetActive(true);
            }
        }

        selectedTab = button;
        ResetTabs();
        button.background.sprite = tabActive;
        int index = button.transform.GetSiblingIndex();

        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);

                view.PanelChange(objectsToSwap[i]);

                ClearAndSetAll();
            }
            else
            {
                objectsToSwap[i].SetActive(false);

                if (this.name == "InventoryTabGroup" && objectsToSwap[i].name == "Equip")
                {
                    objectsToSwap[i].SetActive(true);
                }
            }
        }
    }
    */

    public void ClearAndSetAll()
    {
        bool loopbreak = false;
        Inventory inventory = new Inventory();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            int y = 0;
            for (int x = 0; x < objectsToSwap[i].transform.childCount - 1; x++)
            {
                if (objectsToSwap[i].transform.GetChild(x).name != "Image" && objectsToSwap[i].transform.GetChild(x).name != "Left" &&
                        objectsToSwap[i].transform.GetChild(x).name != "Right")
                {
                    loopbreak = false;
                    //objectsToSwap[i].transform.GetChild(x).transform.GetChild(0).GetComponent<Image>().sprite = tabHover;
                    do
                    {
                        if (inventory.ItemsInInventory.Count > y)
                        {
                            if (inventory.ItemsInInventory[y] != null)
                            {
                                if (inventory.ItemsInInventory[y].type == objectsToSwap[i].name || objectsToSwap[i].name == "AllItems")
                                {
                                    //Debug.Log(y);
                                    Sprite sprite1 = Resources.Load<Sprite>("Sprites/000" + inventory.ItemsInInventory[y].IDnumber);
                                    objectsToSwap[i].transform.GetChild(x).transform.GetChild(0).GetComponent<Image>().sprite = sprite1;
                                    objectsToSwap[i].transform.GetChild(x).transform.GetChild(0).name = inventory.ItemsInInventory[y].IDnumber.ToString();
                                    loopbreak = true;
                                }
                            }
                        }
                        else
                        {
                            loopbreak = true;
                        }
                        y++;


                    } while (y < objectsToSwap[i].transform.childCount - 1 && !loopbreak);

                    if (inventory.ItemsInInventory.Count < y && objectsToSwap[i].transform.GetChild(x).childCount != 0)
                    {
                        //objectsToSwap[i].transform.GetChild(x).transform.GetChild(0).GetComponent<Image>().sprite = tabHover;
                    }
                    
                }
            }
        }
    }

    public void ResetTabs()
    {
        foreach(TabButton button in tabButtons)
        {
            if (selectedTab == null || button != selectedTab)
            {
                button.background.sprite = tabIdle;
            }
        }
    }
}
