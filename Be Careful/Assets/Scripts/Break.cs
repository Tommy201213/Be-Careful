using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    // Start is called before the first frame update
    public float BreakTime = 3;
    private float startBreakTime;
    private bool startBreaking;
    void Start()
    {
            
    }

    // Update is called once per frame
    private void Update()
    {
        if(startBreaking && startBreakTime + BreakTime < Time.fixedTime)
        {
            Destroy(gameObject);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            startBreakTime = Time.fixedTime;
            startBreaking = true;
        }
    }
}
