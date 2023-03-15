using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Upgrades : MonoBehaviour
{
    [SerializeField] GameObject[] _towerObjects;

    public void UpgradeTower()
    {
        if(Points.Instance.points >= 100)
        {
            Points.Instance.points -= 100;
            int button = int.Parse(EventSystem.current.currentSelectedGameObject.name);
            _towerObjects[button].SetActive(true);
            EventSystem.current.currentSelectedGameObject.SetActive(false);
            UIManager.Instance.UpdateGoldValue();
            GameManager.Instance.maxTowerHealth += 100f;
            GameManager.Instance.towerHealth += 100f;
            UIManager.Instance.UpdateTowerHealthSlider();
        }
    }
}
