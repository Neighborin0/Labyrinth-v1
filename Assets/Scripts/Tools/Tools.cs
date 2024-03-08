using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Tools : MonoBehaviour
{
    public static UnityEngine.RaycastHit2D GetMousePos()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        return hit;
    }

    public static Unit[] GetAllUnits()
    {
        Unit[] units = UnityEngine.Object.FindObjectsOfType<Unit>();
        return units;
    }

    public static Director GetDirector()
    {
        var director = GameObject.FindObjectOfType<Director>();
        return director;
    }

    public static void ClearAllCharacterSlots()
    {
        foreach (var slot in FindObjectsOfType<CharacterSlot>())
        {
            Destroy(slot.gameObject);
        }
    }

    public static IEnumerator SmoothMove(GameObject obj, float delay, int AmountofTimesToMove ,float x = 0, float y = 0, float z = 0)
    {
        for (int i = 0; i < AmountofTimesToMove; i++)
        {
            if (obj != null)
            {
                obj.transform.position = new Vector3(obj.gameObject.transform.position.x + x, obj.gameObject.transform.position.y + y, obj.gameObject.transform.position.z + z);
                yield return new WaitForSeconds(delay);
            }
        }
    }



}
