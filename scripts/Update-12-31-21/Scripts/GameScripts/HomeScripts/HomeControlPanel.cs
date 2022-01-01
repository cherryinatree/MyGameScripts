using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamingTools;
using UnityEngine.UI;

public class HomeControlPanel
{
    private GameObject characterPanel;

    public HomeControlPanel()
    {
        characterPanel = ResourseLoader.GetGameObject("Prefabs/PrefabsUI/CharacterCard");
    }

    public void CardListFullRoster(GameObject Panel)
    {
        ClearViewport(Panel);
        RectTransform panelRectTransform = Panel.GetComponent<RectTransform>();

        panelRectTransform.sizeDelta = new Vector2(190,101 * HomeSingleton.Instance.save.characterRoster.Count);
        panelRectTransform.anchorMin = new Vector2(0.5f, 1);
        panelRectTransform.anchorMax = new Vector2(0.5f, 1);
        panelRectTransform.pivot = new Vector2(0.5f, 0.5f);


        foreach (Stats stats in HomeSingleton.Instance.save.characterRoster)
        {
            GameObject card = GameObject.Instantiate(characterPanel);
            card.transform.SetParent(Panel.transform, false);
            card.GetComponentInChildren<Button>().onClick.AddListener(delegate { SelectCard(stats); }) ;
            CardLoader(card, stats);
        }
        ViewPanelFunctions view = new ViewPanelFunctions();
        view.HighlightCard();
    }

    public void CardListBattleRoster(GameObject Panel)
    {
        ClearViewport(Panel);
        RectTransform panelRectTransform = Panel.GetComponent<RectTransform>();

        panelRectTransform.sizeDelta = new Vector2(190, 101 * HomeSingleton.Instance.save.characterBattleTeam.Count);
        panelRectTransform.anchorMin = new Vector2(1, 0.5f);
        panelRectTransform.anchorMax = new Vector2(1, 0.5f);
        panelRectTransform.pivot = new Vector2(0.5f, 0.5f);


        foreach (Stats stats in HomeSingleton.Instance.save.characterBattleTeam)
        {
            GameObject card = GameObject.Instantiate(characterPanel);
            card.transform.SetParent(Panel.transform, false);
            card.GetComponentInChildren<Button>().onClick.AddListener(delegate { SelectBattleCard(stats); });
            CardLoader(card, stats);
        }

        ViewPanelFunctions view = new ViewPanelFunctions();
        view.HighlightBattleCard();
    }

    private void SelectCard(Stats stats)
    {
        HomeSingleton.Instance.FocusOnCard = stats;

        ViewPanelFunctions view = new ViewPanelFunctions();
        view.HighlightCard();


        ReloadPanel();
    }
    private void SelectBattleCard(Stats stats)
    {
        HomeSingleton.Instance.BattleCard = stats;

        ViewPanelFunctions view = new ViewPanelFunctions();
        view.HighlightBattleCard();
        ReloadPanel();
    }

    public void ReloadPanel()
    {

        ViewPanelFunctions view = new ViewPanelFunctions();
        Transform viewPanel = GameObject.Find("ViewPanel").transform;
        for (int i = 0; i < viewPanel.childCount; i++)
        {
            if (viewPanel.GetChild(i).gameObject.activeSelf)
            {
                for (int x = 0; x < viewPanel.GetChild(i).transform.childCount; x++)
                {
                    if (viewPanel.GetChild(i).transform.GetChild(x).gameObject.activeSelf)
                    {
                        view.PanelChange(viewPanel.GetChild(i).transform.GetChild(x).gameObject);
                    }
                }
            }
        }
    }


    private void CardLoader(GameObject card, Stats stats)
    {
        card.transform.Find("Image").GetComponent<Image>().sprite = ResourseLoader.GetSprite("Images/CharacterPics/" + stats.picture);
        card.transform.Find("Text").GetComponent<Text>().text = stats.name + "\n\n" + stats.CharacterClass + "\nLevel: " + stats.level;
    }


    private void ClearViewport(GameObject Panel)
    {
        foreach (Transform child in Panel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
