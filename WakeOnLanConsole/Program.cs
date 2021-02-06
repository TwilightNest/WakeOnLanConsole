namespace WakeOnLanConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            WakeOnLan.Send("00-24-1d-84-02-02", "192.168.100.2");
        }
    }
}