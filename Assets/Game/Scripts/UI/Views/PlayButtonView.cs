using MVVM;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.Views
{
    public class PlayButtonView : MonoBehaviour
    {
        [Data("OnToPlayClick")] [SerializeField] public Button button;
    }
}