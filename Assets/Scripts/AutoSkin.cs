using UnityEngine;

public class AutoSkin : MonoBehaviour
{
    [SerializeField] private Texture2D[] texture2DSkin;
    [SerializeField] private SkinnedMeshRenderer[] meshRenderers;


    private void Start()
    {
        int random = Random.Range(0, texture2DSkin.Length);

        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material.mainTexture = texture2DSkin[random];
        }
    }
}