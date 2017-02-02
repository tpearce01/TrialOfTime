using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TargettingSystem : MonoBehaviour
{
    public Sprite icon;
    public GameObject targetHUD;

    void OnMouseDown()
    {
        Player.i.target = gameObject;
        targetHUD.SetActive(true);
        targetHUD.GetComponentInChildren<Image>().sprite = icon;
    }

    public void ClearTarget()
    {
        targetHUD.SetActive(false);
        Player.i.target = null;
    }

}
