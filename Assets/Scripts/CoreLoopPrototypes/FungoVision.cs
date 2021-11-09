using System.Collections.Generic;
using UnityEngine;

public class FungoVision : MonoBehaviour
{
    public static readonly List<MeshRenderer> FungObjects = new List<MeshRenderer>();
    private bool visionIsActive;
    private MaterialPropertyBlock baseBlock;
    
    
    private void Start()
    {
        baseBlock = new MaterialPropertyBlock();
        FungObjects[0].GetPropertyBlock(baseBlock);
    }

    public void ToggleFungoVision()
    {
        visionIsActive = !visionIsActive;
        
        foreach (var fung in FungObjects)
        {
            fung.gameObject.SetActive(visionIsActive);
        }
    }
    
    public void EnableFungoVision()
    {
        visionIsActive = true;
        
        foreach (var fung in FungObjects)
        {
            fung.gameObject.SetActive(visionIsActive);
        }
    }
    
    public void DisableFungoVision()
    {
        visionIsActive = false;
        
        foreach (var fung in FungObjects)
        {
            fung.SetPropertyBlock(baseBlock);
            fung.gameObject.SetActive(visionIsActive);
        }
    }

    public void SetPropertyBlocks(MaterialPropertyBlock block)
    {
        foreach (var fungRend in FungObjects)
        {
            fungRend.SetPropertyBlock(block);
        }
    }
}
