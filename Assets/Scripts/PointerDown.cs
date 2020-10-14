
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PointerDown : MonoBehaviour, IPointerDownHandler
{


    public void OnPointerDown(PointerEventData eventData)
    {
        SizeController._canShoot = true;
        RoadDetecting._canWin = false;
        SceneManager.LoadScene(0);
    }

}
