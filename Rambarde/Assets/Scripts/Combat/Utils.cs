using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using UniRx;
using UnityEngine;

public static class Utils {
    public static T LoadResourceFromDir<T>(string dir, string filename) where T : UnityEngine.Object {
        return dir == "" ? Resources.Load<T>(filename) : Resources.Load<T>(dir + "/" + filename);
    }

    public static async Task AwaitObservable<T>(IObservable<T> obs, Action<T> f = null) {
        if (f is null)
            obs.Subscribe();
        else obs.Subscribe(f);
        await obs;
    }

    public delegate void LerpOnSubscribe(Pair<float> pair, GameObject go, ref float curLerpTime, float speed, float lerpTime, ref float t);

    public static IDisposable UpdateGameObjectLerp(Pair<float> values, GameObject go, float speed, float lerpTime, LerpOnSubscribe subscribe, Action<Pair<float>> doOnComplete) {
        float t = 0f, currentLerpTime = 0f;
        return Observable.EveryLateUpdate()
            .TakeWhile(_ => currentLerpTime < lerpTime + speed * Time.deltaTime)
            .DoOnCompleted(() => doOnComplete(values))
            .Subscribe(_ => { subscribe(values, go, ref currentLerpTime, speed, lerpTime, ref t); });
    }

    public static string SplitPascalCase(string s)
    {
        var r = new Regex(@"(?<!^)(?=[A-Z](?![A-Z]|$))", RegexOptions.IgnorePatternWhitespace);
        return r.Replace(s, " ");
    }
}