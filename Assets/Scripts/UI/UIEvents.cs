using System;

namespace STR.UI
{
    public static class UIEvents
    {
        public static event Action<int> MoneyChanged;
        public static event Action<int> WaveChanged;
        public static event Action<float> WaveTimerChanged;
        public static event Action<bool> WaveTimerVisibilityChanged;
        public static event Action<string> NotificationRequested;
        public static event Action PlayRequested;
        public static event Action PauseRequested;
        public static event Action ResumeRequested;
        public static event Action RestartRequested;
        public static event Action MainMenuRequested;
        public static event Action GameOverTriggered;
        public static event Action GameWinTriggered;

        public static void RaiseMoneyChanged(int money) => MoneyChanged?.Invoke(money);
        public static void RaiseWaveChanged(int waveNumber) => WaveChanged?.Invoke(waveNumber);
        public static void RaiseWaveTimerChanged(float timeRemaining) => WaveTimerChanged?.Invoke(timeRemaining);
        public static void RaiseWaveTimerVisibilityChanged(bool visible) => WaveTimerVisibilityChanged?.Invoke(visible);
        public static void RaiseNotificationRequested(string message) => NotificationRequested?.Invoke(message);
        public static void RaisePlayRequested() => PlayRequested?.Invoke();
        public static void RaisePauseRequested() => PauseRequested?.Invoke();
        public static void RaiseResumeRequested() => ResumeRequested?.Invoke();
        public static void RaiseRestartRequested() => RestartRequested?.Invoke();
        public static void RaiseMainMenuRequested() => MainMenuRequested?.Invoke();
        public static void RaiseGameOverTriggered() => GameOverTriggered?.Invoke();
        public static void RaiseGameWinTriggered() => GameWinTriggered?.Invoke();
    }
}
