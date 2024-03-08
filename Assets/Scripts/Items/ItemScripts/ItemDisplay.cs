using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class ItemDisplay : MonoBehaviour
{
    public Image displayImage;
    public TextMeshProUGUI itemDesc;
    public TextMeshProUGUI itemName;
    public Item item;
   
    public IEnumerator TransitionIn(float DistanceToMove)
    {
        //print("this is being ran");
        for (int i = 0; i < 60; i++)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + DistanceToMove, gameObject.transform.position.z);
            yield return new WaitForSeconds(0.000f);
            print("battle log should be moving");
        }
        displayImage.sprite = item.sprite;
        itemDesc.text = item.itemDescription;
        itemName.text = item.itemName;
        yield return new WaitForSeconds(000f);
    }
    public void OnInteracted()
    {
        //print("item should be added");
        var director = GameObject.FindObjectOfType<Director>();
        var unit = director.party[0];
        unit.inventory.Add(item);
        item.OnPickup(unit);
        director.party[0] = unit;
        StartCoroutine(Transition());
        //SceneManager.LoadScene(0);

    }

    IEnumerator Transition()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
    }


}
