using UnityEngine;
using System;

public static class GameEvents
{
    // Eventos públicos accesibles globalmente
    public static event Action OnCanHandle;
    public static event Action OnNoCanHandle;

    // Métodos para invocar eventos
    public static void CanHandle() => OnCanHandle?.Invoke();
    public static void NoCanHandle() => OnNoCanHandle?.Invoke();
}