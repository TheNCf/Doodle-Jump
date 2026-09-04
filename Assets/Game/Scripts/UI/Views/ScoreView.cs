using MVVM;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.Views
{
    public class ScoreView : MonoBehaviour
    {
        [Data("Score")] [SerializeField] private TMP_Text _scoreText;
    }
}