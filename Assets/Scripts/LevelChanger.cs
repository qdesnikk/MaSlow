using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private List<Level> _levels;

    private List<GridObject> _bottomTemplates = new List<GridObject>();
    private List<GridObject> _topTemplates = new List<GridObject>();
    private List<GridObject> _obstackles = new List<GridObject>();
    private int _currentLevel;

    public List<GridObject> BottomTemplates => _bottomTemplates;
    public List<GridObject> TopTemplates => _topTemplates;
    public List<GridObject> Obstackles => _obstackles;

    private void Awake()
    {
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel");

        _bottomTemplates = FillTemplatesList(_levels[_currentLevel - 1].BottomTemplates);
        _topTemplates = FillTemplatesList(_levels[_currentLevel - 1].TopTemplates);
        _obstackles = FillTemplatesList(_levels[_currentLevel - 1].Obstackles);
    }

    private List<GridObject> FillTemplatesList(List<GridObject> inputTemplates)
    {
        List<GridObject> tempTemplates = new List<GridObject>();

        for (int i = 0; i < inputTemplates.Count; i++)
        {
            tempTemplates.Add(inputTemplates[i]);
        }

        return tempTemplates;
    }

    public void OpenNextLevel()
    {
        if (_levels.Count >= _currentLevel + 1)
        {
            _currentLevel++;
            PlayerPrefs.SetInt("CurrentLevel", _currentLevel);

            TryUnlockNextLevel();

            SceneManager.LoadScene("Game", LoadSceneMode.Single);
            Time.timeScale = 1;
        }
    }

    private void TryUnlockNextLevel()
    {
        int totalCountUnlockedLevels = PlayerPrefs.GetInt("TotalCountUnlockedLevels");

        if (totalCountUnlockedLevels < _currentLevel)
        {
            totalCountUnlockedLevels++;
            PlayerPrefs.SetInt("TotalCountUnlockedLevels", totalCountUnlockedLevels);
        }
    }
}
