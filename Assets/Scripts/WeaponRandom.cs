using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponRandom : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image backImage;
    public Button _button;
    public List<WeaponUI> weaponList = new List<WeaponUI>();

    private void Awake()
    {
        backImage = GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(() =>
        {
            foreach (WeaponUI weapon in WeaponSelectPanel.Instance._weaponList.GetComponentsInChildren<WeaponUI>())
            {
                weaponList.Add(weapon);
            }

            WeaponUI w = GameManager.Instance.RandomOne(weaponList) as WeaponUI;

            w.RenewUI(w.weaponData);
            w.ButtonClick(w.weaponData);
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
