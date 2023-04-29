using TMPro;
using UnityEngine;

namespace Features.LoadingScene
{
    public class LoadingItemController : MonoBehaviour
    {
        public TextMeshProUGUI Title;

        public TextMeshProUGUI Status;

        public void Initialize(string title, string status)
        {
            Title.text = title;
            Status.text = status;
        }

        public void SetStatus(string status)
        {
            Status.text = status;
        }
    }
}