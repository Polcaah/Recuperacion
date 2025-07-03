using UnityEngine;

public class SettingsPanelController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject[] panels; 

    public void ShowPanel(int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == index);
        }
    }
}
