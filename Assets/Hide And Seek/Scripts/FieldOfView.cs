using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius;
    [Range(0,360)]
    public float angle;

    public float spreadAlertRadius;

    public GameObject playerRef;

    public LayerMask obstructionMask;
    public LayerMask targetMask;
    public LayerMask enemyMask;

    public bool canSeePlayer;
    public bool alert;
    public bool gotSignal;
    public bool signalSpreaded;

    public Coroutine delayCoroutine = null;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        
        
    }

    private void Update()
    {
        FieldOfViewCheck();
        ChangeColor(Color.red);
        SpreadBySignal(gotSignal, canSeePlayer);
    }

    public void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].gameObject.transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                    alert = true;
                }
                else
                {
                    canSeePlayer = false;
                    
                    
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if(canSeePlayer)
        {
            canSeePlayer = false;
        }
    }

    public virtual void ChangeColor(Color color)
    {
        if (alert == true)
        {
            Debug.Log("check");
            gameObject.GetComponent<Renderer>().material.color = color;
            SpreadSignal();
            
            if (delayCoroutine != null)
            {
                StopCoroutine(delayCoroutine);
                delayCoroutine = null;
            } 
            

        } else if (delayCoroutine == null)
        {
            delayCoroutine = StartCoroutine(DelayAfterSeePlayer());
        }
        
    }

    public virtual IEnumerator DelayAfterSeePlayer()
    {
        yield return new WaitForSeconds(5f);
        
        UnspreadSignal();
        alert = false;
        delayCoroutine = null;
        signalSpreaded = false;
        
    }

    public virtual IEnumerator DelayAfterAlert()
    {
        yield return new WaitForSeconds(5f);
        alert = false;
        delayCoroutine = null;
        signalSpreaded = false;

    }

    public void SpreadSignal()
    {
        if (canSeePlayer == true)
        {
            Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, spreadAlertRadius, enemyMask);
            foreach (Collider enemy in enemiesInRange)
            {
                enemy.gameObject.GetComponent<FieldOfView>().gotSignal = true;
                enemy.gameObject.GetComponent<Renderer>().material.color = Color.red;
                

            }
            alert = false;
        }

    }

   public void UnspreadSignal()
    {
        if (canSeePlayer == false)
        {
            Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, spreadAlertRadius, enemyMask);
            foreach (Collider enemy in enemiesInRange)
            {
                enemy.gameObject.GetComponent<FieldOfView>().gotSignal = false;
                gameObject.GetComponent<Renderer>().material.color = Color.white;

            }
        }
    }

    public virtual void SpreadBySignal(bool gotSignal, bool canSeePlayer)
    {
        if (gotSignal == true && (canSeePlayer == false) && signalSpreaded == false)
        {
            Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, spreadAlertRadius, enemyMask);
            foreach (Collider enemy in enemiesInRange)
            {
                enemy.gameObject.GetComponent<FieldOfView>().gotSignal = true;
                gameObject.GetComponent<Renderer>().material.color = Color.red;

            }
            signalSpreaded = true;
        }
    }

    
}
