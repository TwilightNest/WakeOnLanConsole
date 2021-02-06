using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace WakeOnLanConsole
{
	public class WakeOnLan
	{
		public static void Send(string TargetMac, string SourceIp)
		{
			PhysicalAddress target = PhysicalAddress.Parse(TargetMac.ToUpper());
			IPAddress senderAddress = IPAddress.Parse(SourceIp);

			byte[] payload = new byte[102]; // 6 bytes of ff, plus 16 repetitions of the 6-byte target
			byte[] targetMacBytes = target.GetAddressBytes();

			// Set first 6 bytes to ff
			for (int i = 0; i < 6; i++)
				payload[i] = 0xff;

			// Repeat the target mac 16 times
			for (int i = 6; i < 102; i += 6)
				targetMacBytes.CopyTo(payload, i);

			// Create a socket to send the packet, and send it
			using (Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
			{
				sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
				sock.Bind(new IPEndPoint(senderAddress, 0));
				sock.SendTo(payload, new IPEndPoint(IPAddress.Broadcast, 7));
			}
		}
	}
}