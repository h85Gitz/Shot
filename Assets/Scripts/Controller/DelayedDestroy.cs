using System.Collections;
using Manager;
using Models;
using UnityEngine;

namespace Controller
{
    public class DelayedDestroy : MonoBehaviour
    {
        private void OnEnable()
        {
            var ps = GetComponent<ParticleSystem>();
            ps.Play();
            StartCoroutine(DestroyEffect());
        }

        private IEnumerator DestroyEffect()
        {
            yield return new WaitForSeconds(1.5f);
            ObjectPool.Instance.PushPool(this.gameObject);
        }
    }


}
