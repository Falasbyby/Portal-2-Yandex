using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private ParticleSystem effect;
    public bool grab = false;
    private Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
    }

    public void RestartBox()
    {
        effect.gameObject.transform.SetParent(null);
        effect.gameObject.transform.position = transform.position;
        effect.Play();
        effect.GetComponent<AudioSource>().Play();
        transform.position = startPos;
      

    }
    public void GrabBox(bool grab)
    {
        this.grab = grab;
    }
    
}
