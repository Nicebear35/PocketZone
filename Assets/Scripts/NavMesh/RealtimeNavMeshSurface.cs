using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RealtimeNavMeshSurface : MonoBehaviour
{
    [SerializeField] private NavMeshSurface[] _navMeshSurfaces;

    private void Awake()
    {
        for (int i = 0; i < _navMeshSurfaces.Length; i++)
        {
            _navMeshSurfaces[i].BuildNavMesh();
        }
    }
}
