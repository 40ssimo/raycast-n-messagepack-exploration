using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FieldOfView2 : FieldOfView
{
    // Start is called before the first frame update

    public FieldOfView fov;
    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        FieldOfViewCheck();
        if (fov.alert == false && canSeePlayer && fov.signalSpreaded == false)
        {
            ChangeColor(Color.yellow);
        }

    }

    public override void ChangeColor(Color color)
    {
        if (canSeePlayer)
        {
            gameObject.GetComponent<Renderer>().material.color = color;

            if (delayCoroutine != null)
            {
                StopCoroutine(delayCoroutine);
                delayCoroutine = null;
            }


        }
        else if (delayCoroutine == null && canSeePlayer == false)
        {
            delayCoroutine = StartCoroutine(DelayAfterSeePlayer());
        }
    }

    public override IEnumerator DelayAfterSeePlayer()
    {
        yield return new WaitForSeconds(5f);
        gameObject.GetComponent<Renderer>().material.color = Color.white;
        alert = false;
        delayCoroutine = null;
    }

    public override void SpreadBySignal(bool gotSignal, bool canSeePlayer)
    {
        
    }
}
