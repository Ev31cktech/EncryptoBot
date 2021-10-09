using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using EncryptoBot.Moves;
using System.Linq;
using Bot.Utilities.Processed.Packet;
using RLBotDotNet;

namespace EncryptoBot
{
	public class EncryptoBotMind
	{
		public List<RLBotDotNet.Bot> BotList = new List<RLBotDotNet.Bot>();
		private List<IMoves> MovesDict = new List<IMoves>();
		Packet packet;

		public EncryptoBotMind()
		{
			FindAllMoves();
		}
		public void UpdatePacket(Packet _packet, EncryptoAgent encAgent)
		{
			packet = packet == _packet? packet:_packet;
			encAgent.moves = MovesDict;
			encAgent.moves.ForEach(m => m.Update(packet, encAgent));
			if (encAgent.getActiveMove().Done)
			{
				encAgent.moves = encAgent.moves.Where( m => m.Available).ToList();
				encAgent.moves = encAgent.moves.OrderByDescending(m => m.Priority).ToList();
				encAgent.moves[0].Run(_packet, encAgent);
			}
		}
		public void FindAllMoves()
		{
			Assembly.GetExecutingAssembly()
				.GetTypes()
				.Where(t => t.IsSubclassOf(typeof(IMoves)) && !t.IsAbstract)
				.ToList()
				.ForEach(t => MovesDict.Add((IMoves)Activator.CreateInstance(t)));
		}
		public void AddBot(EncryptoAgent encAgent)
		{
			BotList.Add(encAgent);
			encAgent.moves = new List<IMoves>();
			encAgent.moves.Add(new StandStil());
		}
		internal Controller GetMove(int index)
		{
			return new Controller();
		}
	}
}