using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<UIPanel> panels;

    private Dictionary<UIPanelType, UIPanel> panelLookup;

    private void Awake()
    {
        panelLookup = new Dictionary<UIPanelType, UIPanel>();

        foreach (UIPanel panel in panels)
        {
            panelLookup.Add(panel.panelType, panel);
        }

        AssignListeners(UIPanelType.GameOver);
    }


    private void AssignListeners(UIPanelType panelType)
    {
        if (panelLookup.TryGetValue(panelType, out UIPanel panel))
        {
            Button[] buttons = panel.panel.GetComponentsInChildren<Button>();

            if (panelType == UIPanelType.GameOver)
            {
                buttons[0].onClick.AddListener(OnRestartButton);
            }
            if (buttons.Length > 1)
                buttons[1].onClick.AddListener(OnStatsButton);
            buttons[2].onClick.AddListener(OnQuitButton);
        }
    }

    #region Panel Visibility Management
    public void ShowPanel(UIPanelType panelType)
    {
        if (panelLookup.TryGetValue(panelType, out UIPanel panel))
        {
            panel.panel.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Panel of type {panelType} not found.");
        }
    }

    public void HidePanel(UIPanelType panelType)
    {
        if (panelLookup.TryGetValue(panelType, out UIPanel panel))
        {
            panel.panel.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"Panel of type {panelType} not found.");
        }
    }
    #endregion

    public void UpdateDistance(float distance)
    {
        if (panelLookup.TryGetValue(UIPanelType.Distance, out UIPanel panel))
        {
            panel.elements[0].GetComponent<TMP_Text>().text = distance.ToString();
        }
    }

    #region Game Over Panel: Buttons

    public void OnRestartButton() 
    {
        GameManager.Instance.RestartGame();
    }

    public void OnQuitButton()
    {
        SceneManager.LoadScene("MenuScene");
    }
    
    public void OnStatsButton()
    {
        // Implement stats display logic here
    }

    #endregion
}