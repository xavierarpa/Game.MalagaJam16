﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XavHelpTo;
using XavHelpTo.Get;
using XavHelpTo.Know;

public class NewsAtributteProcessor : MonoBehaviour
{
    public static NewsAtributteProcessor _;
    private List<AttributeScriptableObject> _attributeScriptableObjects = new List<AttributeScriptableObject>();
    [SerializeField] private List<ConclusionScriptableObject> conclusionScriptableObjects = new List<ConclusionScriptableObject>();

    
    private void Awake()
    {
        this.Singleton(ref _, true);
        // list_news_left = general.News.ToArray().ToList();
        _attributeScriptableObjects = new List<AttributeScriptableObject>();
    }

    public void AddAttributes(AttributeScriptableObject[] currentNewAttributes)
    {
        _attributeScriptableObjects = _attributeScriptableObjects.Concat(currentNewAttributes).ToList();
    }

    public ConclusionScriptableObject GetConclusion()
    {
        var selectedConclusionScriptableObjects = new List<ConclusionScriptableObject>(); 
        foreach (var conclusion in conclusionScriptableObjects)
        {
            if (conclusion.requireds.Contains(_attributeScriptableObjects.ToArray()))
            {
                selectedConclusionScriptableObjects.Add(conclusion);
            }
        }

        if (selectedConclusionScriptableObjects.Count == 0)
        {
            return conclusionScriptableObjects[0];
        }
        
        var conclusionScriptableObject = Get.Range(selectedConclusionScriptableObjects.ToArray());

        return conclusionScriptableObject;
    }
}