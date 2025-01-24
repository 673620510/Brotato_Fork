using UnityEngine;
using UnityEngine.UI;

public class ItemCardUI : MonoBehaviour
{
    public ItemData itemData;
    public Button _button;
    public CanvasGroup _canvasGroup;

    private void Awake()
    {
        _button = transform.GetChild(4).GetComponent<Button>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        _button.onClick.AddListener(() =>
        {

        });
    }
}
