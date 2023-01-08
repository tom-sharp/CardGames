using Games.Card.TexasHoldEm;
using Syslib;
using Syslib.Games.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames
{

	public static class Factory { 
	}

	public static class TestRank
	{
		public static void Run() {
			var rankit = new TexasHoldEmRankHand();
			var cards = new PlayCards();
			int cardrank, cardrank1, cardrank2;
			ulong signature, rank;
			IPlayCardHandRank handrank;

			cardrank = 2;
			cards.Add(new PlayCardHeart(cardrank));
			handrank = rankit.HandSignature(cards);
			Console.WriteLine($" {cardrank,2}:{handrank.Signature}:{handrank.Rank}");
			cards.Clear();

			cardrank = 3;
			cards.Add(new PlayCardHeart(cardrank));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank,2}:{signature}:{rank}");
			cards.Clear();

			cardrank = 4;
			cards.Add(new PlayCardHeart(cardrank));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank,2}:{signature}:{rank}");
			cards.Clear();

			cardrank = 5;
			cards.Add(new PlayCardHeart(cardrank));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank,2}:{signature}:{rank}");
			cards.Clear();

			cardrank = 6;
			cards.Add(new PlayCardHeart(cardrank));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank,2}:{signature}:{rank}");
			cards.Clear();

			cardrank = 7;
			cards.Add(new PlayCardHeart(cardrank));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank,2}:{signature}:{rank}");
			cards.Clear();

			cardrank = 8;
			cards.Add(new PlayCardHeart(cardrank));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank,2}:{signature}:{rank}");
			cards.Clear();

			cardrank = 9;
			cards.Add(new PlayCardHeart(cardrank));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank,2}:{signature}:{rank}");
			cards.Clear();

			cardrank = 10;
			cards.Add(new PlayCardHeart(cardrank));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank,2}:{signature}:{rank}");
			cards.Clear();

			cardrank = 11;
			cards.Add(new PlayCardHeart(cardrank));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank,2}:{signature}:{rank}");
			cards.Clear();

			cardrank = 12;
			cards.Add(new PlayCardHeart(cardrank));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank,2}:{signature}:{rank}");
			cards.Clear();

			cardrank = 13;
			cards.Add(new PlayCardHeart(cardrank));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank,2}:{signature}:{rank}");
			cards.Clear();

			cardrank = 14;
			cards.Add(new PlayCardHeart(cardrank));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank,2}:{signature}:{rank}");
			cards.Clear();



			cardrank1 = 2; cardrank2 = 2;
			cards.Add(new PlayCardHeart(cardrank1));
			cards.Add(new PlayCardClub(cardrank2));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank1,2} {cardrank2,2}:{signature}:{rank}");
			cards.Clear();

			cardrank1 = 2; cardrank2 = 3;
			cards.Add(new PlayCardHeart(cardrank1));
			cards.Add(new PlayCardClub(cardrank2));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank1,2} {cardrank2,2}:{signature}:{rank}");
			cards.Clear();

			cardrank1 = 2; cardrank2 = 4;
			cards.Add(new PlayCardHeart(cardrank1));
			cards.Add(new PlayCardClub(cardrank2));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank1,2} {cardrank2,2}:{signature}:{rank}");
			cards.Clear();

			cardrank1 = 2; cardrank2 = 13;
			cards.Add(new PlayCardHeart(cardrank1));
			cards.Add(new PlayCardClub(cardrank2));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank1,2} {cardrank2,2}:{signature}:{rank}");
			cards.Clear();

			cardrank1 = 2; cardrank2 = 14;
			cards.Add(new PlayCardHeart(cardrank1));
			cards.Add(new PlayCardClub(cardrank2));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank1,2} {cardrank2,2}:{signature}:{rank}");
			cards.Clear();

			cardrank1 = 14; cardrank2 = 13;
			cards.Add(new PlayCardHeart(cardrank1));
			cards.Add(new PlayCardClub(cardrank2));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank1,2} {cardrank2,2}:{signature}:{rank}");
			cards.Clear();

			cardrank1 = 14; cardrank2 = 14;
			cards.Add(new PlayCardHeart(cardrank1));
			cards.Add(new PlayCardClub(cardrank2));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank1,2} {cardrank2,2}:{signature}:{rank}");
			cards.Clear();



			cardrank1 = 2; cardrank2 = 2;
			cards.Add(new PlayCardHeart(cardrank1));
			cards.Add(new PlayCardHeart(cardrank2));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank1,2} {cardrank2,2}:{signature}:{rank}");
			cards.Clear();

			cardrank1 = 2; cardrank2 = 3;
			cards.Add(new PlayCardHeart(cardrank1));
			cards.Add(new PlayCardHeart(cardrank2));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank1,2} {cardrank2,2}:{signature}:{rank}");
			cards.Clear();

			cardrank1 = 2; cardrank2 = 4;
			cards.Add(new PlayCardHeart(cardrank1));
			cards.Add(new PlayCardHeart(cardrank2));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank1,2} {cardrank2,2}:{signature}:{rank}");
			cards.Clear();

			cardrank1 = 2; cardrank2 = 13;
			cards.Add(new PlayCardHeart(cardrank1));
			cards.Add(new PlayCardHeart(cardrank2));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank1,2} {cardrank2,2}:{signature}:{rank}");
			cards.Clear();

			cardrank1 = 2; cardrank2 = 14;
			cards.Add(new PlayCardHeart(cardrank1));
			cards.Add(new PlayCardHeart(cardrank2));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank1,2} {cardrank2,2}:{signature}:{rank}");
			cards.Clear();

			cardrank1 = 14; cardrank2 = 13;
			cards.Add(new PlayCardHeart(cardrank1));
			cards.Add(new PlayCardHeart(cardrank2));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank1,2} {cardrank2,2}:{signature}:{rank}");
			cards.Clear();

			cardrank1 = 14; cardrank2 = 14;
			cards.Add(new PlayCardHeart(cardrank1));
			cards.Add(new PlayCardHeart(cardrank2));
			signature = rankit.HandSignature(cards).Signature;
			rank = rankit.HandSignature(cards).Rank;
			Console.WriteLine($" {cardrank1,2} {cardrank2,2}:{signature}:{rank}");
			cards.Clear();

		}
	}
}
