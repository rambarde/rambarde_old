using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace UI
{
    public class Billboard : MonoBehaviour
    {
        private void Start()
        {
            this.LateUpdateAsObservable()
                .Subscribe(_ =>
                {
                    transform.LookAt(transform.position + Camera.main.transform.forward);
                });

        }
    }
}
