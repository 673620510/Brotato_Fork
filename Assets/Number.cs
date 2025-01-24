using TMPro;
using UnityEngine;

public class Number : MonoBehaviour
{
    public TMP_Text text;
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }
    private void Start()
    {
        Destroy(gameObject, 1.1f);
    }
}
