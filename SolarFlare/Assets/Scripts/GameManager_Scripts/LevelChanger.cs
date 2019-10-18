using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public class LevelChanger : MonoBehaviour
    {
        private Animator animator;

        private void OnEnable()
        {
            GameManager_Master.Instance.RestartLevelEvent += FadeToLevel;
            animator = GetComponent<Animator>();
        }

        private void OnDisable()
        {
            GameManager_Master.Instance.RestartLevelEvent -= FadeToLevel;
        }

        public void FadeToLevel()
        {
            animator.SetTrigger("FadeOut");
        }

        private void OnFadeComplete()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
