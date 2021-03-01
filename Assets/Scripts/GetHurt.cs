using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHurt : MonoBehaviour
{
    private List<Color> usedColors;
    private bool isHurt;
    private float hurtTime;

    public float hurtTimeVariable;
    public Material hurtMaterial;

    // Start is called before the first frame update
    void Start()
    {
        usedColors = new List<Color>();
        hurtTime = hurtTimeVariable;
        isHurt = false;
        foreach (Material m in this.GetComponent<Renderer>().materials)
            usedColors.Add(m.GetColor("_Color"));
    }

    // Update is called once per frame
    void Update()
    {
        if (isHurt)
        {
            hurtTime -= Time.deltaTime;
        }
        if (hurtTime <= 0)
        {
            for (int i = 0; i < this.GetComponent<Renderer>().materials.Length; i++)
            {
                this.GetComponent<Renderer>().materials[i].SetColor("_Color", usedColors[i]);
            }
            hurtTime = hurtTimeVariable;
            isHurt = false;
        }
    }

    public void Hurt()
    {
        isHurt = true;
        for (int i = this.GetComponent<Renderer>().materials.Length - 1; i >= 0; i--)
        { 
            this.GetComponent<Renderer>().materials[i].SetColor("_Color", hurtMaterial.GetColor("_Color"));
        }
    }


}
