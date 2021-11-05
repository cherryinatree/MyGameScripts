using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeNPCs
{
   public MakeNPCs()
    {

    }

    public void MakeMerchant()
    {
        ResourceLoader resource = new ResourceLoader();
        GameObject merchant = MonoBehaviour.Instantiate(resource.Merchants[0]);
        merchant.AddComponent<InteractableNPC>();
        merchant.GetComponent<InteractableNPC>().Merchant = true;
        merchant.transform.position = new Vector3(1f, 1f, 1f);
        merchant.GetComponent<InteractableNPC>().resourceLoader = GameObject.Find("GameMaster").GetComponent<TownMainTestScript>().Town.resourceLoader;
    }
}
