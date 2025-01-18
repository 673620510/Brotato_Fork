using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DifficultyRandom : MonoBehaviour
{
    public Image backImage;
    public Button _button;
    public List<DifficultyUI> difficultyList = new List<DifficultyUI>();

    private void Awake()
    {
        backImage = GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(() =>
        {
            foreach (DifficultyUI weapon in DifficultySelectPanel.Instance._difficultyList.GetComponentsInChildren<DifficultyUI>())
            {
                difficultyList.Add(weapon);
            }

            DifficultyUI d = GameManager.Instance.RandomOne(difficultyList) as DifficultyUI;

            d.RenewUI(d.difficultyData);
            d.ButtonClick(d.difficultyData);
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        backImage.color = new Color(207 / 255f, 207 / 255f, 207 / 255f);
        RoleSelectPanel.Instance._contentCanvasGroup.alpha = 0;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        backImage.color = new Color(34 / 255f, 34 / 255f, 34 / 255f);
    }
}
