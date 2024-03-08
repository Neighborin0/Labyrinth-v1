using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Healthbar : MonoBehaviour
{
    public Slider slider;
    public Unit unit;
    public TextMeshProUGUI text;
    public GameObject damagePopUp;
    public TextMeshProUGUI namePlate;



    void Update()
    {
        slider.maxValue = unit.maxHP;
        slider.value = unit.currentHP;
        text.text = unit.currentHP.ToString() + "/" + unit.maxHP.ToString();
        namePlate.text = unit.unitName;
    }


    public void TakeDamage(int damage)
    {
        var truedamage = damage - unit.defenseStat;
        if (truedamage < 1)
        {
            truedamage = 1;   
        }
        print(truedamage);
        unit.currentHP -= truedamage;
        StartCoroutine(DamagePopUp(truedamage));
    }

    public void Die()
    {
        var battlesystem = GameObject.FindObjectOfType<BattleSystem>();
        battlesystem.numOfUnits.Remove(unit);
        Destroy(unit.gameObject);
    }

    private IEnumerator DamagePopUp(int damage)
    {
        if (unit.currentHP < 1)
        {
            var popup = Instantiate(damagePopUp, new Vector3(unit.transform.position.x, unit.transform.position.y + 2f, unit.transform.position.z), Quaternion.identity);
            var number = popup.GetComponentInChildren<TextMeshProUGUI>();
            try
            {
                number.SetText(damage.ToString());

            }
            catch
            {
                print("text isn't being found?");
            }
            StartCoroutine(Tools.SmoothMove(popup, 0.001f, 20, 0, 0.005f));
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(Tools.SmoothMove(popup, 0.001f, 40, 0, -0.005f));
            var sprite = unit.GetComponent<SpriteRenderer>();
            sprite.forceRenderingOff = true;
            yield return new WaitForSeconds(0.1f);
            sprite.forceRenderingOff = false;
            yield return new WaitForSeconds(0.1f);
            sprite.forceRenderingOff = true;
            yield return new WaitForSeconds(0.1f);
            sprite.forceRenderingOff = false;
            yield return new WaitForSeconds(0.1f);
            sprite.forceRenderingOff = true;
            yield return new WaitForSeconds(0.1f);
            sprite.forceRenderingOff = false;
            yield return new WaitForSeconds(0.1f);
            sprite.forceRenderingOff = true;
            yield return new WaitForSeconds(0.1f);
            Die();
            Destroy(popup);
        }
        else
        {
            var popup = Instantiate(damagePopUp, new Vector3(unit.transform.position.x, unit.transform.position.y + 2f, unit.transform.position.z), Quaternion.identity);
            var number = popup.GetComponentInChildren<TextMeshProUGUI>();
            try
            {
                number.SetText(damage.ToString());

            }
            catch
            {
                print("text isn't being found?");
            }
            StartCoroutine(Tools.SmoothMove(popup, 0.001f, 20, 0, 0.005f));
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(Tools.SmoothMove(popup, 0.001f, 40, 0, -0.005f));
            yield return new WaitForSeconds(0.5f);
            Destroy(popup);
        }
    }


}
