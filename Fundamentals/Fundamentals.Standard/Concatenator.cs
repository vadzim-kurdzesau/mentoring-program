using System;

namespace Fundamentals.Standard
{
    public static class Concatenator
    {
        public static string GetGreetingMessage(string username)
        {
            return $"{DateTime.Now.ToString("HH:mm")} Hello, {username}!";
        }
    }
}
