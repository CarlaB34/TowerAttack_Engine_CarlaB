﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : SingletonMono<EntityManager>
{
    // Ref vers la global target des entités Player
    public GameObject towerIA;
    // Ref vers la global target des entités IA
    public GameObject towerPlayer;

    ///+ //OutPost du player
  
    public GameObject outPostPlayerR;
    public GameObject outPostPlayerL;
    //OutPost du IA
    public GameObject outPostIAR;
    public GameObject outPostIAL;
    // +moveable
    public Transform other;
    // public MapData mapData;
    private MapManager m_mapManger;

    public Action<Alignment> OnTowerDestroy;

    public void PopElementFromData(EntityData entityData, Vector3 position)
    {
        GameObject newInstantiate = PoolManager.Instance.GetElement(entityData);
        if (newInstantiate != null)
        {
            SetPopElement(newInstantiate, position);
        }
        else
        {
            Debug.LogError("NO POOLED DATA PREFAB : " + entityData.name);
        }
    }

    public void PopElementFromPrefab(GameObject prefabToPop, Vector3 position)
    {
        GameObject newInstantiate = PoolManager.Instance.GetElement(prefabToPop);
        if (newInstantiate != null)
        {
            SetPopElement(newInstantiate, position);
        }
        else
        {
            Debug.LogError("NO POOLED PREFAB : " + prefabToPop.name);
        }
    }
    

    // Fonction centrale.
    // Toute instantiation d'entité doit passer par cette fonction.
    // Elle centralise l'initialisation de l'entité.
    private void SetPopElement(GameObject newInstantiate, Vector3 position)
    {
        newInstantiate.transform.position = position;
        newInstantiate.SetActive(true);
        Entity entity = newInstantiate.GetComponent<Entity>();
        if (entity is EntityMoveable moveable)
        {
            if (moveable.entityData.alignment == Alignment.IA)
            {  
                moveable.SetGlobalTarget(towerPlayer);
            }
            else if (moveable.entityData.alignment == Alignment.Player)
            {
                moveable.SetGlobalTarget(towerIA);
                //+ test de move vers l'outpost puis la tour, ne marche pas
                //si c'est un outpost
                //if (outPostIAL)
                //{
                //    moveable.SetGlobalTarget(outPostIAL);
                //}
                ////si c'est une tour et si l'outpost est détruit
                //else if (towerIA && !outPostIAL)
                //{

                //    moveable.SetGlobalTarget(towerIA);
                //}

            }
            entity.RestartEntity();
        }
    }

    public void PoolElement(GameObject toPool)
    {
        if (towerPlayer == toPool)
        {
            OnTowerDestroy?.Invoke(Alignment.Player);
        }
        else if (towerIA == toPool)
        {
            OnTowerDestroy?.Invoke(Alignment.IA);
        }
            PoolManager.Instance.PoolElement(toPool);
    }
}
