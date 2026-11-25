using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private HUD _hud;
    [SerializeField] private StartGameUI _startGameUI;
    [SerializeField] private GameOverUI _gameOverUI;
    [SerializeField] private Player _player;
    [SerializeField] private float _maxTimeSeconds;
    // Add a member variable that's a LIST (or ARRAY) of all of the objects
    //      that the player is required to kick.
    // This variable should be editable from the inspector.

    [SerializeField] private List<GameObject> _requiredItems = new List<GameObject>();
    private List<GameObject> _kickedItems = new List<GameObject>();

    private int _commonPoints;
    private int _uncommonPoints;
    private int _rarePoints;
    private float _timeRemaining;
    private bool _runGame;

    // ------------------------------------------------------------------------
    private void Start()
    {
        SetGameEnabled(false);

        // Tell the HUD what the required items are.
        _hud.SetRequiredItems(_requiredItems);
        _hud.UpdateTimerText(_maxTimeSeconds);
        _startGameUI.gameObject.SetActive(true);
        _gameOverUI.gameObject.SetActive(false);
    }

    // ------------------------------------------------------------------------
    // You don't need to code in this method :)
    private void Update()
    {
        if (_runGame)
        {
            _timeRemaining = Mathf.Max(0f, _timeRemaining - Time.deltaTime);
            _hud.UpdateTimerText(_timeRemaining);

            if (_timeRemaining <= 0)
            {
                EndGame();
            }
        }
    }

    // ------------------------------------------------------------------------
    // You don't need to code in this method :)
    public void StartGame()
    {
        if (_runGame) return;
        _kickedItems.Clear();
        _startGameUI.gameObject.SetActive(false);
        _gameOverUI.gameObject.SetActive(false);

        SetGameEnabled(true);

        _commonPoints = 0;
        _uncommonPoints = 0;
        _rarePoints = 0;
        _hud.UpdatePointsText(_commonPoints, _uncommonPoints, _rarePoints);

        _timeRemaining = _maxTimeSeconds;
        _hud.UpdateTimerText(_timeRemaining);
    }

    // ------------------------------------------------------------------------
    // You don't need to code in this method :)
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    // ------------------------------------------------------------------------
    private void EndGame()
    {
        SetGameEnabled(false);

        // Calculate the player's total points.

        // Tell the GameOverUI.
        _gameOverUI.ShowChecklistResult(_kickedItems.Count, _requiredItems.Count);
    }

    // ------------------------------------------------------------------------
    // You don't need to code in this method :)
    private void SetGameEnabled(bool enabled)
    {
        _player.enabled = enabled;
        Rigidbody2D body = _player.GetComponent<Rigidbody2D>();
        body.linearVelocity = Vector2.zero;
        body.simulated = enabled;
        if (!enabled) _player.GetComponent<Animator>().SetBool("Walking", false);
        _runGame = enabled;
    }

    // ------------------------------------------------------------------------
    // You will need to ADD PARAMETERS to this method to represent the GameObject
    //  kicked and the rarity of that object.
    public void Kicked(GameObject item)
    {
        if (!_runGame || !_requiredItems.Contains(item) || _kickedItems.Contains(item)) return;
        _kickedItems.Add(item);
        _hud.KickedItem(item);

        // Use a switch statement.
        // Based on the rarity of the object kicked, change the number of points
        //      for that rarity category.
        

        // Tell the HUD what item was kicked, and how many points we now have.
        
    }
}