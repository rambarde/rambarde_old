using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Music {
    public class NoteGarbage : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            this.OnTriggerEnterAsObservable()
                .Where(x => x.gameObject.CompareTag($"Note"))
                .Subscribe(x => Destroy(x.gameObject));
        }
    }
}
