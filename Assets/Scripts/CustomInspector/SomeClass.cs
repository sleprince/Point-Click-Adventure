using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeClass : MonoBehaviour
{
    public string playerName;
    public float speed;
    public GameObject playerPrefabs;
    public Vector3 playerPosition;

    [SerializeField] string s_playerName; //use serialise instead of public so that it can only be changed in Editor, not by other scripts, to stop
    //bugs.
    [SerializeField] float s_speed;
    [SerializeField] GameObject s_playerPrefabs;
    [SerializeField] Vector3 s_playerPosition;
}
