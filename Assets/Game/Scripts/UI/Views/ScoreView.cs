using MVVM;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI.Views
{
    public class ScoreView : MonoBehaviour
    {
        [Data("Score")] [SerializeField] public TMP_Text _scoreText;
    }
}