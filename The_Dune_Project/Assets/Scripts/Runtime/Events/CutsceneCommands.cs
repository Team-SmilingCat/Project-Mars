using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneCommands : MonoBehaviour
{
    private Vector3 lastPos;
    [SerializeField] AudioSource audioBGM;

    public void DisableGameObject(GameObject a)
    {
        a.SetActive(false);
    }

    public void EnableGameObject(GameObject a)
    {
        a.SetActive(true);
    }

    public void DisableRenderOnMesh(GameObject a)
    {
        a.GetComponent<MeshRenderer>().enabled = false;
    }

    public void DisablePlayerRender(GameObject a)
    {
        lastPos = a.transform.position;
        a.transform.Translate(new Vector3(0, -50, 0));
    }

    public void EnablePlayerRender(GameObject a)
    {
        a.transform.position = lastPos;
    }

    public void EnableRenderOnMesh(GameObject a)
    {
        a.GetComponent<MeshRenderer>().enabled = true;
    }

    public void ChngeBGM(AudioClip bgm)
    {
        audioBGM.clip = bgm;
        audioBGM.Play();
    }

    public void SetAnimatorBool(GameObject a)
    {

    }
}
