using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSound : MonoBehaviour
{
    public AudioSource audioS;

    public UnityEngine.AI.NavMeshAgent agent;

    public float multi = 0.25f;
    private void Update()
    {
        audioS.pitch = Mathf.Lerp(audioS.pitch, 0.25f + agent.velocity.magnitude * multi, Time.deltaTime*3f);
    }
}
