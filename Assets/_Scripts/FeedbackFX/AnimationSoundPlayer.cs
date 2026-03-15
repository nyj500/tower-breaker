using UnityEngine;
using TowerBreaker.Core;

namespace TowerBreaker.FeedbackFX
{
    /// <summary>
    /// 애니메이션 이벤트에서 호출.
    /// Animation Event → Function: PlaySFX → Object: AudioClip
    /// </summary>
    public class AnimationSoundPlayer : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] private float volumeScale = 1f;

        public void PlaySFX(Object clip)
        {
            SoundManager.Instance?.PlaySFX(clip as AudioClip, volumeScale);
        }
    }
}
