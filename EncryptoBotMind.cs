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

		public List<IMoves> MovesDict = new List<IMoves>();
		public EncryptoBotMind()
		{
			FindAllMoves();
		}
		public void UpdatePacket(Packet packet)
		{
			/*
			MovesDict.ForEach(m => m.Update(packet));
			if (MovesDict[0].Done)
			{
				MovesDict.OrderByDescending(m => m.Priority);
			}*/
		}
		public void FindAllMoves()
		{
			Assembly.GetExecutingAssembly()
				.GetTypes()
				.Where(t => t.IsSubclassOf(typeof(IMoves)) && !t.IsAbstract)
				.ToList()
				.ForEach(t => MovesDict.Add((IMoves)Activator.CreateInstance(t)));
		}
		internal Controller GetMove(int index)
		{
			return new Controller();
		}
	}
}