using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public PlayerController player;
    public Transform Spawnpoint;
    public CameraController Camera;
    public GameObject PlayerPrefab;
    public Animator CheckPointSetUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!player)
        {
            player = Instantiate(PlayerPrefab,Spawnpoint.position,Quaternion.identity).GetComponent<PlayerController>();
            Camera.Player = player.transform;
        }
    }
    public void SetCheckPoint(Transform CheckPoint)
    {
        Spawnpoint = CheckPoint;
        if (CheckPointSetUI) CheckPointSetUI.SetTrigger("CheckPointSet");
    }
}
