using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private Transform _checklistParent;
    [SerializeField] private ChecklistItemUI _checklistItemPrefab;
    [SerializeField] private TMP_Text _timerText;

    private List<ChecklistItemUI> _checklistItems = new List<ChecklistItemUI>();

    // ------------------------------------------------------------------------
    public void SetRequiredItems (List<GameObject> items)
    {
        foreach (ChecklistItemUI oldItem in _checklistItems) Destroy(oldItem.gameObject);
        _checklistItems.Clear();
        foreach (GameObject item in items)
        {
            ChecklistItemUI entry = Instantiate(_checklistItemPrefab, _checklistParent);
            entry.SetItem(item);
            _checklistItems.Add(entry);
        }
        // For every entry in 'items',
        //      create a new ChecklistItemUI GameObject.
        // Set its parent to _checklistParent.
        // Use the correct method in ChecklistItemUI to setup its required item.
        
    }

    // ------------------------------------------------------------------------
    public void KickedItem(GameObject item)
    {
        // Find the ChecklistItem in _checklistItems whose required item is the
        //      'item' input for this method.
        // Tell that item we found it!
        foreach (ChecklistItemUI entry in _checklistItems)
        {
            if (entry._requiredItem == item)
            {
                entry.FoundItem();
                break;
            }
        }
    }

    // ------------------------------------------------------------------------
    public void UpdatePointsText (int commonPoints, int uncommonPoints, int rarePoints)
    {
        // Update all of the text.
    }

    // ------------------------------------------------------------------------
    public void UpdateTimerText (float timeLeft)
    {
        // Update the timer text.
        _timerText.text = Mathf.Max(0f, timeLeft).ToString("0.0") + "s";
    }
}