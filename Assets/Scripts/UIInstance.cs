using UnityEngine;

public class UIInstance : MonoBehaviour
{
    public static UIInstance instance;

    [Header("UIPanels")]
    [Space]
    public Transform winPanel;
    public Transform losePanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(this);
        }

    }
}
