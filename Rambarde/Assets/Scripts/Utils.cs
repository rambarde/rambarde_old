using System;
using System.Threading.Tasks;
using UniRx;

public static class Utils {
    public static async Task AwaitObservable<T>(IObservable<T> obs) {
        obs.Subscribe();
        await obs;
    }
}