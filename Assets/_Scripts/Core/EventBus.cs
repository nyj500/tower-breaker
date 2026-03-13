using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerBreaker.Core
{
    /// <summary>
    /// 정적 이벤트 버스. Action<T> 기반 pub/sub 패턴.
    /// 사용 예) EventBus.Subscribe<PlayerDeadEvent>(OnPlayerDead);
    ///          EventBus.Publish(new PlayerDeadEvent());
    ///          EventBus.Unsubscribe<PlayerDeadEvent>(OnPlayerDead);
    /// </summary>
    public static class EventBus
    {
        private static readonly Dictionary<Type, Delegate> subscribers = new();

        public static void Subscribe<T>(Action<T> callback)
        {
            // TODO: 타입별 딕셔너리에 콜백 등록
            var type = typeof(T);
            if (subscribers.ContainsKey(type))
                subscribers[type] = Delegate.Combine(subscribers[type], callback);
            else
                subscribers[type] = callback;
        }

        public static void Unsubscribe<T>(Action<T> callback)
        {
            // TODO: 해당 타입의 콜백 제거
            var type = typeof(T);
            if (subscribers.ContainsKey(type))
            {
                subscribers[type] = Delegate.Remove(subscribers[type], callback);
                if (subscribers[type] == null)
                    subscribers.Remove(type);
            }
        }

        public static void Publish<T>(T eventData)
        {
            // TODO: 해당 타입 구독자 전체에게 이벤트 데이터 전달
            var type = typeof(T);
            if (subscribers.TryGetValue(type, out var del))
                (del as Action<T>)?.Invoke(eventData);
        }

        public static void ClearAll()
        {
            // TODO: 씬 전환 시 전체 구독 초기화
            subscribers.Clear();
        }
    }
}
