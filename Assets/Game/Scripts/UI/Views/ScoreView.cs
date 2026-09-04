using MVVM;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.Views
{
    public class ScoreView : MonoBehaviour
    {
        [Data("Currency")] [SerializeField] private TextMeshProUGUI _scoreText;
    }
}