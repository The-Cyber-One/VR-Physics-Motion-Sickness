using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> room1, room2;
    [SerializeField] private int room1Amount = 1, room2Amount = 1;
    [SerializeField] private GameObject collectablePrefab;

    [ContextMenu("Validate")]
    private void OnValidate()
    {
        room1.Clear();
        room2.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.name.Contains("Room 1"))
                room1.Add(child.transform);
            else
                room2.Add(child.transform);
        }

        room1Amount = Math.Clamp(room1Amount, 0, room1.Count);
        room2Amount = Math.Clamp(room2Amount, 0, room2.Count);
    }

    private void Start()
    {
        var availableRoom1 = room1.Select(room => room).ToList();
        availableRoom1.Shuffle();
        for (int i = 0; i < room1Amount; i++)
        {
            SpawnCollectable(availableRoom1[i]);
        }

        var availableRoom2 = room2.Select(room => room).ToList();
        availableRoom2.Shuffle();
        for (int i = 0; i < room2Amount; i++)
        {
            SpawnCollectable(availableRoom2[i]);
        }
    }

    private void SpawnCollectable(Transform transform) => Instantiate(collectablePrefab, transform);
}
