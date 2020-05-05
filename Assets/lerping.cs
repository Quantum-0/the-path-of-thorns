using UnityEngine;
using UnityEngine.UI;

public class lerping : MonoBehaviour
{
    Image img;
    public float transp;
    
    void Start()
    {
        img = GetComponent<Image>();
    }
    
    void Update()
    {
        img.color = new Color(0, 0, 0, Mathf.Lerp(img.color.a, transp, Time.deltaTime * 3));
    }
}
