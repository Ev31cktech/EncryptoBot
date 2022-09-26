using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using RLBotDotNet;
using EncryptoBot.Moves;
using EncryptoBot.States;
using Bot.Utilities.Processed.Packet;
using System.Numerics;

namespace EncryptoBot
{
	public class EncryptoBotMind
	{
		public List<RLBotDotNet.Bot> BotList = new List<RLBotDotNet.Bot>();
		private List<IMove> MovesList = new List<IMove>();
		private List<IState> StatesList = new List<IState>();
		Packet packet = null;

		public EncryptoBotMind()
		{
			FindAllMoves();
			FindAllStates();
		}
		public void UpdatePacket(EncryptoAgent encAgent)
		{
			if(encAgent.state.moves.Peek().GetType() == typeof(Jump) && ((Jump)encAgent.state.moves.Peek()).StartTime == 0)
				encAgent.state.moves.Peek().Run(encAgent);

			encAgent.state.Update(encAgent);
			if (encAgent.state.moves.Peek().Done)
			{
				encAgent.state.moves.Pop();
				encAgent.state.moves.Peek().Run(encAgent);
			}
		}
		public void SetState(EncryptoAgent encAgent)
		{
			if (!encAgent.carObject.HasWheelContact)
			{
				encAgent.state = new Recover();
			}
			else if (encAgent.packet.GameInfo.IsKickoffPause)
			{
				int closestCarI = 0;
				Player[] pls = encAgent.packet.Players;
				for (int i = 0; i < BotList.Count; i++)
				{
					if (Vector3.Distance(pls[i].Location, encAgent.packet.Ball.Physics.Location) < Vector3.Distance(pls[closestCarI].Location, encAgent.packet.Ball.Physics.Location))
						closestCarI = i;
				}
				if (pls[closestCarI].Location == encAgent.Location)
				{
					encAgent.state = new States.KickOff();
				}
				else
				{
					encAgent.state = new States.GetSmallBoost();
				}
			}
			else if (encAgent.BoostAmount <.8)
			{
				encAgent.state = new States.GetBigBoost();

			}
			else if (encAgent.BoostAmount < .4)
			{
				encAgent.state = new States.GetBigBoost();
			}
			else
			{
				encAgent.state = new States.DriveToBall();
			}
			// if ball is going towords our net stop it
			// if ball is not in their net shoot forward(+ find where ball is going to touch ground and intercept, hitbox calculation)
		}
		public void FindAllStates()
		{
			Assembly.GetExecutingAssembly()
				.GetTypes()
				.Where(t => t.IsSubclassOf(typeof(IState)) && !t.IsAbstract)
				.ToList()
				.ForEach(t =>
				{
					IState state = (IState)Activator.CreateInstance(t);
					state.Initialize(t.Name, StatesList.Count);
					StatesList.Add(state);
				});
		}
		public void FindAllMoves()
		{
			Assembly.GetExecutingAssembly()
				.GetTypes()
				.Where(t => t.IsSubclassOf(typeof(IMove)) && !t.IsAbstract)
				.ToList()
				.ForEach(t =>
				{
					IMove move = (IMove)Activator.CreateInstance(t);
					move.Initialize(t.Name, MovesList.Count);
					MovesList.Add(move);
				});
		}
		public void AddBot(EncryptoAgent encAgent)
		{
			Debugger.Info("");
			BotList.Add(encAgent);
			encAgent.state = new EmptyState();
			encAgent.state.Run(encAgent);
		}
	}
}