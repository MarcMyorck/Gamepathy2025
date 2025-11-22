using UnityEngine;

public static class SessionData
{
    public static int cartsCollected = 0;
    public static int cartsTotal = 0;

    public static int empathieCollected = 0;
    public static int empathieTotal = 0;

    public static void Reset()
    {
        cartsCollected = 0;
        cartsTotal = 0;

        empathieCollected = 0;
        empathieTotal = 0;
    }
}
