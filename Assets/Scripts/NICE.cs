using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class displays a big "NICE" Ui-Overlay when the KL-Divergence is low 
/// </summary>
public class NICE : MonoBehaviour
{
    [SerializeField]
    KullbackLeiblerDivergence kl;
    [SerializeField]
    Transform start;
    [SerializeField]
    Transform target;
    [SerializeField]
    GameObject nice;
    [SerializeField]
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        kl.lowDivergence += Nice;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Nice()
    {
        StopAllCoroutines();
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        nice.transform.position = start.position;
        while ((target.position-nice.transform.position).sqrMagnitude>1)
        {
            nice.transform.Translate((target.position - start.position) * Time.deltaTime * speed);
            yield return null;
        }
    }
}
