using System;
using System.Linq;

namespace War
{
	public partial class Default : System.Web.UI.Page
	{
		public static Random random = new Random();

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void playButton_Click(object sender, EventArgs e)
		{
			Player Player1 = new Player();
			Player Player2 = new Player();
			Player TieGame = new Player(); // this is HIGHLY unlikely, but still necessary
			Deck deck = new Deck();
			int round = 0;

			// shuffle and deal the deck
			resultLabel.Text = "<h2>Dealing cards...</h2>";

			for (int i = 52; i > 0; i--)
			{
				if (i % 2 == 0)
				{
					int randomIndex = random.Next(0, i);
					Player1.Hand.Add(deck.Unshuffled.ElementAt(randomIndex));
					resultLabel.Text += String.Format("Player 1 is dealt the {0}<br>",
						deck.Unshuffled.ElementAt(randomIndex).CardName);
						deck.Unshuffled.RemoveAt(randomIndex);
				}
				else
				{
					int randomIndex = random.Next(0, i);
					Player2.Hand.Add(deck.Unshuffled.ElementAt(randomIndex));
					resultLabel.Text += String.Format("Player 2 is dealt the {0}<br><br>",
						deck.Unshuffled.ElementAt(randomIndex).CardName);
						deck.Unshuffled.RemoveAt(randomIndex);
				}
			}

			resultLabel.Text += "<h2>Begin battle...</h2>";

			// game logic
			while ((!Player1.IsWinner) && (!Player2.IsWinner))
			{
				round++;
				// conduct battle
				resultLabel.Text += "Round " + round + " Battle Cards: " + Player1.Hand.Last().CardName + " vs. " + Player2.Hand.Last().CardName;

				// player 1 win scenario
				if (Player1.Hand.Last().CardValue > Player2.Hand.Last().CardValue)
				{
					resultLabel.Text += "<br>Bounty..." + "<br>&nbsp;&nbsp" + Player1.Hand.Last().CardName + "<br>&nbsp;&nbsp" + Player2.Hand.Last().CardName + "<br><strong>Player 1 wins the round!</strong>";
					Player1.Hand.Insert(0, Player2.Hand.Last());
					Player2.Hand.Remove(Player2.Hand.Last());
					resultLabel.Text += "<br>Player 1 has " + Player1.Hand.Count.ToString() + " cards." + "<br>Player 2 has " + Player2.Hand.Count.ToString() + " cards.<br><br>";
				}

				// player 2 win scenario	
				else if (Player1.Hand.Last().CardValue < Player2.Hand.Last().CardValue)
				{
					resultLabel.Text += "<br>Bounty..." + "<br>&nbsp;&nbsp" + Player1.Hand.Last().CardName + "<br>&nbsp;&nbsp" + Player2.Hand.Last().CardName + "<br><strong>Player 2 wins the round!</strong>";
					Player2.Hand.Insert(0, Player1.Hand.Last());
					Player1.Hand.Remove(Player1.Hand.Last());
					resultLabel.Text += "<br>Player 1 has " + Player1.Hand.Count.ToString() + " cards." + "<br>Player 2 has " + Player2.Hand.Count.ToString() + " cards.<br><br>";
				}

				// war scenario	
				else
				{
					// if one player lacks the cards for a war, the other player wins the game
					if (Player2.Hand.Count < 5)
					{
						resultLabel.Text += "<br>Player 2 does not have enough cards for war.<br><strong>Player 1 wins the round and takes Player 2's remaining cards!</strong><br>Player 1 has 52 cards.<br>Player 2 has 0 cards.";
						Player1.IsWinner = true;
					}
					else if (Player1.Hand.Count < 5)
					{
						resultLabel.Text += "<br>Player 1 does not have enough cards for war.<br><strong>Player 2 wins the round and takes Player 1's remaining cards!</strong><br>Player 2 has 52 cards.<br>Player 1 has 0 cards.";
						Player2.IsWinner = true;
					}

					// if both players have enough cards for war, they each add three cards to the pot, then flip a fourth to determine the winner
					else if (((Player1.Hand.ElementAt(Player1.Hand.Count - 5).CardValue) > (Player2.Hand.ElementAt(Player2.Hand.Count - 5).CardValue)))
					{
						resultLabel.Text += "<br>***************WAR****************" + "<br>Bounty..." + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 5).CardName + "<br><strong>Player 1 wins the round!</strong>";
						for (int i = 0; i < 5; i++)
						{
							Player1.Hand.Insert(0, Player2.Hand.Last());
							Player2.Hand.Remove(Player2.Hand.Last());
						};
						resultLabel.Text += "<br>Player 1 has " + Player1.Hand.Count.ToString() + " cards." + "<br>Player 2 has " + Player2.Hand.Count.ToString() + " cards.<br><br>";
					}
					else if (((Player1.Hand.ElementAt(Player1.Hand.Count - 5).CardValue) < (Player2.Hand.ElementAt(Player2.Hand.Count - 5).CardValue)))
					{
						resultLabel.Text += "<br>***************WAR****************" + "<br>Bounty..." + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 5).CardName + "<br><strong>Player 2 wins the round!</strong>";
						for (int i = 0; i < 5; i++)
						{
							Player2.Hand.Insert(0, Player1.Hand.Last());
							Player1.Hand.Remove(Player1.Hand.Last());
						};
						resultLabel.Text += "<br>Player 1 has " + Player1.Hand.Count.ToString() + " cards." + "<br>Player 2 has " + Player2.Hand.Count.ToString() + " cards.<br><br>";
					}
			
					// double war scenario: if the players tie in war
					else
					{
						if (Player2.Hand.Count < 9)
						{
							resultLabel.Text += "<br>Player 2 does not have enough cards for double war.<br><strong>Player 1 wins the round and takes Player 2's remaining cards!</strong><br>Player 1 has 52 cards.<br>Player 2 has 0 cards.";
							Player1.IsWinner = true;
						}
						
						else if (Player1.Hand.Count < 9)
						{
							resultLabel.Text += "<br>Player 1 does not have enough cards for double war.<br><strong>Player 2 wins the round and takes Player 1's remaining cards!</strong><br>Player 2 has 52 cards.<br>Player 1 has 0 cards.";
							Player2.IsWinner = true;
						}
						else if (((Player1.Hand.ElementAt(Player1.Hand.Count - 9).CardValue) > (Player2.Hand.ElementAt(Player2.Hand.Count - 9).CardValue)))
						{
							resultLabel.Text += "<br>***************DOUBLE WAR****************" + "<br>Bounty..." + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 6).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 6).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 7).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 7).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 8).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 8).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 9).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 9).CardName + "<br><strong>Player 1 wins the round!</strong>";
							for (int i = 0; i < 9; i++)
							{
								Player1.Hand.Insert(0, Player2.Hand.Last());
								Player2.Hand.Remove(Player2.Hand.Last());
							};
							resultLabel.Text += "<br>Player 1 has " + Player1.Hand.Count.ToString() + " cards." + "<br>Player 2 has " + Player2.Hand.Count.ToString() + " cards.<br><br>";
						}
						else if (((Player1.Hand.ElementAt(Player1.Hand.Count - 9).CardValue) < (Player2.Hand.ElementAt(Player2.Hand.Count - 9).CardValue)))
						{
							resultLabel.Text += "<br>***************DOUBLE WAR****************" + "<br>Bounty..." + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 6).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 6).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 7).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 7).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 8).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 8).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 9).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 9).CardName + "<br><strong>Player 2 wins the round!</strong>";
							for (int i = 0; i < 9; i++)
							{
								Player2.Hand.Insert(0, Player1.Hand.Last());
								Player1.Hand.Remove(Player1.Hand.Last());
							};
							resultLabel.Text += "<br>Player 1 has " + Player1.Hand.Count.ToString() + " cards."+ "<br>Player 2 has " + Player2.Hand.Count.ToString() + " cards.<br><br>";
						}

						// triple war scenario: if the players tie in double war
						else
						{
							if (Player2.Hand.Count < 13)
							{
								resultLabel.Text += "<br>Player 2 does not have enough cards for triple war.<br><strong>Player 1 wins the round and takes Player 2's remaining cards!</strong><br>Player 1 has 52 cards.<br>Player 2 has 0 cards.";
								Player1.IsWinner = true;
							}
							else if (Player1.Hand.Count < 13)
							{
								resultLabel.Text += "<br>Player 1 does not have enough cards for triple war.<br><strong>Player 2 wins the round and takes Player 1's remaining cards!</strong><br>Player 2 has 52 cards.<br>Player 1 has 0 cards.";
								Player2.IsWinner = true;
							}
							else if (((Player1.Hand.ElementAt(Player1.Hand.Count - 13).CardValue) > (Player2.Hand.ElementAt(Player2.Hand.Count - 13).CardValue)))
							{
								resultLabel.Text += "<br>***************TRIPLE WAR****************" + "<br>Bounty..." + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 6).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 6).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 7).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 7).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 8).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 8).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 9).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 9).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 10).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 10).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 11).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 11).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 12).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 12).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 13).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 13).CardName + "<br><strong>Player 1 wins the round!</strong>";
								for (int i = 0; i < 13; i++)
								{
									Player1.Hand.Insert(0, Player2.Hand.Last());
									Player2.Hand.Remove(Player2.Hand.Last());
								};
								resultLabel.Text += "<br>Player 1 has " + Player1.Hand.Count.ToString() + " cards."+ "<br>Player 2 has " + Player2.Hand.Count.ToString() + " cards.<br><br>";
							}
							else if (((Player1.Hand.ElementAt(Player1.Hand.Count - 13).CardValue) < (Player2.Hand.ElementAt(Player2.Hand.Count - 13).CardValue)))
							{
								resultLabel.Text += "<br>***************TRIPLE WAR****************" + "<br>Bounty..." + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 6).CardName+ "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 6).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 7).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 7).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 8).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 8).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 9).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 9).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 10).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 10).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 11).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 11).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 12).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 12).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 13).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 13).CardName + "<br><strong>Player 2 wins the round!</strong>";
								for (int i = 0; i < 13; i++)
								{
									Player2.Hand.Insert(0, Player1.Hand.Last());
									Player1.Hand.Remove(Player1.Hand.Last());
								};
								resultLabel.Text += "<br>Player 1 has " + Player1.Hand.Count.ToString() + " cards." + "<br>Player 2 has " + Player2.Hand.Count.ToString() + " cards.<br><br>";
							}

							// quadruple war scenario: if the players tie in triple war
							else
							{
								if (Player2.Hand.Count < 17)
								{
									resultLabel.Text += "<br>Player 2 does not have enough cards for quadruple war.<br><strong>Player 1 wins the round and takes Player 2's remaining cards!</strong><br>Player 1 has 52 cards.<br>Player 2 has 0 cards.";
									Player1.IsWinner = true;
								}
								else if (Player1.Hand.Count < 17)
								{
									resultLabel.Text += "<br>Player 1 does not have enough cards for quadruple war.<br><strong>Player 2 wins the round and takes Player 1's remaining cards!</strong><br>Player 2 has 52 cards.<br>Player 1 has 0 cards.";
									Player2.IsWinner = true;
								}
								else if (((Player1.Hand.ElementAt(Player1.Hand.Count - 17).CardValue) > (Player2.Hand.ElementAt(Player2.Hand.Count - 17).CardValue)))
								{
									resultLabel.Text += "<br>***************QUADRUPLE WAR****************" + "<br>Bounty..." + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 6).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 6).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 7).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 7).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 8).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 8).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 9).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 9).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 10).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 10).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 11).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 11).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 12).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 12).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 13).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 13).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 14).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 14).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 15).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 15).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 16).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 16).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 17).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 17).CardName + "<br><strong>Player 1 wins the round!</strong>";
									for (int i = 0; i < 17; i++)
									{
										Player1.Hand.Insert(0, Player2.Hand.Last());
										Player2.Hand.Remove(Player2.Hand.Last());
									};
									resultLabel.Text += "<br>Player 1 has " + Player1.Hand.Count.ToString() + " cards." + "<br>Player 2 has " + Player2.Hand.Count.ToString() + " cards.<br><br>";
								}
								else if (((Player1.Hand.ElementAt(Player1.Hand.Count - 17).CardValue) < (Player2.Hand.ElementAt(Player2.Hand.Count - 17).CardValue)))
								{
									resultLabel.Text += "<br>***************QUADRUPLE WAR****************" + "<br>Bounty..." + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 6).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 6).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 7).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 7).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 8).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 8).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 9).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 9).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 10).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 10).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 11).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 11).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 12).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 12).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 13).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 13).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 14).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 14).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 15).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 15).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 16).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 16).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 17).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 17).CardName + "<br><strong>Player 2 wins the round!</strong>";
									for (int i = 0; i < 17; i++)
									{
										Player2.Hand.Insert(0, Player1.Hand.Last());
										Player1.Hand.Remove(Player1.Hand.Last());
									};
									resultLabel.Text += "<br>Player 1 has " + Player1.Hand.Count.ToString() + " cards." + "<br>Player 2 has " + Player2.Hand.Count.ToString() + " cards.<br><br>";
								}

								// quintuple war scenario: if the players tie in quadruple war
								else
								{
									if (Player2.Hand.Count < 21)
									{
										resultLabel.Text += "<br>Player 2 does not have enough cards for quintuple war.<br><strong>Player 1 wins the round and takes Player 2's remaining cards!</strong><br>Player 1 has 52 cards.<br>Player 2 has 0 cards.";
										Player1.IsWinner = true;
									}
									else if (Player1.Hand.Count < 21)
									{
										resultLabel.Text += "<br>Player 1 does not have enough cards for quintuple war.<br><strong>Player 2 wins the round and takes Player 1's remaining cards!</strong><br>Player 2 has 52 cards.<br>Player 1 has 0 cards.";
										Player2.IsWinner = true;
									}
									else if (((Player1.Hand.ElementAt(Player1.Hand.Count - 21).CardValue) > (Player2.Hand.ElementAt(Player2.Hand.Count - 21).CardValue)))
									{
										resultLabel.Text += "<br>***************QUINTUPLE WAR****************" + "<br>Bounty..." + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 6).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 6).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 7).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 7).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 8).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 8).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 9).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 9).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 10).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 10).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 11).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 11).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 12).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 12).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 13).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 13).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 14).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 14).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 15).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 15).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 16).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 16).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 17).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 17).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 18).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 18).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 19).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 19).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 20).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 20).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 21).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 21).CardName + "<br><strong>Player 1 wins the round!</strong>";
										for (int i = 0; i < 21; i++)
										{
											Player1.Hand.Insert(0, Player2.Hand.Last());
											Player2.Hand.Remove(Player2.Hand.Last());
										};
										resultLabel.Text += "<br>Player 1 has " + Player1.Hand.Count.ToString() + " cards." + "<br>Player 2 has " + Player2.Hand.Count.ToString() + " cards.<br><br>";
									}
									else if (((Player1.Hand.ElementAt(Player1.Hand.Count - 21).CardValue) < (Player2.Hand.ElementAt(Player2.Hand.Count - 21).CardValue)))
									{
										resultLabel.Text += "<br>***************QUINTUPLE WAR****************" + "<br>Bounty..." + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 6).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 6).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 7).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 7).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 8).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 8).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 9).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 9).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 10).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 10).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 11).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 11).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 12).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 12).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 13).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 13).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 14).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 14).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 15).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 15).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 16).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 16).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 17).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 17).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 18).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 18).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 19).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 19).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 20).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 20).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 21).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 21).CardName + "<br><strong>Player 2 wins the round!</strong>";
										for (int i = 0; i < 21; i++)
										{
											Player2.Hand.Insert(0, Player1.Hand.Last());
											Player1.Hand.Remove(Player1.Hand.Last());
										};
										resultLabel.Text += "<br>Player 1 has " + Player1.Hand.Count.ToString() + " cards." + "<br>Player 2 has " + Player2.Hand.Count.ToString() + " cards.<br><br>";
									}

									// sextuple war: if the players tie in quintuple war
									else
									{
										if (Player2.Hand.Count < 25)
										{
											resultLabel.Text += "<br>Player 2 does not have enough cards for sextuple war.<br><strong>Player 1 wins the round and takes Player 2's remaining cards!</strong><br>Player 1 has 52 cards.<br>Player 2 has 0 cards.";
											Player1.IsWinner = true;
										}	
										else if (Player1.Hand.Count < 25)
										{
											resultLabel.Text += "<br>Player 1 does not have enough cards for sextuple war.<br><strong>Player 2 wins the round and takes Player 1's remaining cards!</strong><br>Player 2 has 52 cards.<br>Player 1 has 0 cards.";
											Player2.IsWinner = true;
										}
										else if (((Player1.Hand.ElementAt(Player1.Hand.Count - 25).CardValue) > (Player2.Hand.ElementAt(Player2.Hand.Count - 25).CardValue)))
										{
											resultLabel.Text += "<br>***************SEXTUPLE WAR****************" + "<br>Bounty..." + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 6).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 6).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 7).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 7).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 8).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 8).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 9).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 9).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 10).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 10).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 11).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 11).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 12).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 12).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 13).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 13).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 14).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 14).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 15).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 15).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 16).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 16).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 17).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 17).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 18).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 18).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 19).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 19).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 20).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 20).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 21).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 21).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 22).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 22).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 23).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 23).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 24).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 24).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 25).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 25).CardName + "<br><strong>Player 1 wins the round!</strong>";
											for (int i = 0; i < 25; i++)
											{
												Player1.Hand.Insert(0, Player2.Hand.Last());
												Player2.Hand.Remove(Player2.Hand.Last());
											};
											resultLabel.Text += "<br>Player 1 has " + Player1.Hand.Count.ToString() + " cards." + "<br>Player 2 has " + Player2.Hand.Count.ToString() + " cards.<br><br>";
										}
										
										else if (((Player1.Hand.ElementAt(Player1.Hand.Count - 25).CardValue) < (Player2.Hand.ElementAt(Player2.Hand.Count - 25).CardValue)))
										{
											resultLabel.Text += "<br>***************SEXTUPLE WAR****************" + "<br>Bounty..." + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 6).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 6).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 7).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 7).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 8).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 8).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 9).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 9).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 10).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 10).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 11).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 11).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 12).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 12).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 13).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 13).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 14).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 14).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 15).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 15).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 16).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 16).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 17).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 17).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 18).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 18).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 19).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 19).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 20).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 20).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 21).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 21).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 22).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 22).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 23).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 23).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 24).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 24).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 25).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 25).CardName + "<br><strong>Player 2 wins the round!</strong>";
											for (int i = 0; i < 25; i++)
											{
												Player2.Hand.Insert(0, Player1.Hand.Last());
												Player1.Hand.Remove(Player1.Hand.Last());
											};
											resultLabel.Text += "<br>Player 1 has " + Player1.Hand.Count.ToString() + " cards." + "<br>Player 2 has " + Player2.Hand.Count.ToString() + " cards.<br><br>";
										}
										
										// sextuple war tiebreaker: if the players tie in sextuple war
										else
										{
											resultLabel.Text += "<br>***************SEXTUPLE WAR****************" + "<br>Bounty..." + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 1).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 2).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 3).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 4).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 5).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 6).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 6).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 7).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 7).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 8).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 8).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 9).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 9).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 10).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 10).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 11).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 11).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 12).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 12).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 13).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 13).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 14).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 14).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 15).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 15).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 16).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 16).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 17).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 17).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 18).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 18).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 19).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 19).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 20).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 20).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 21).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 21).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 22).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 22).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 23).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 23).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 24).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 24).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 25).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 25).CardName + "<br>&nbsp;&nbsp" + Player1.Hand.ElementAt(Player1.Hand.Count - 26).CardName + "<br>&nbsp;&nbsp" + Player2.Hand.ElementAt(Player2.Hand.Count - 26).CardName;
											if ((Player1.Hand.ElementAt(Player1.Hand.Count - 26).CardValue) > (Player2.Hand.ElementAt(Player2.Hand.Count - 26).CardValue))
											{
												resultLabel.Text += "<br><strong>Player 1 wins the round!</strong>";
												for (int i = 0; i < 26; i++)
												{
													Player1.Hand.Insert(0, Player2.Hand.Last());
													Player2.Hand.Remove(Player2.Hand.Last());
												};
												resultLabel.Text += "<br>Player 1 has " + Player1.Hand.Count.ToString() + " cards." + "<br>Player 2 has " + Player2.Hand.Count.ToString() + " cards.<br><br>";
											}
											else if ((Player1.Hand.ElementAt(Player1.Hand.Count - 26).CardValue) < (Player2.Hand.ElementAt(Player2.Hand.Count - 26).CardValue))
											{
												resultLabel.Text += "<br><strong>Player 2 wins the round!</strong>";
												for (int i = 0; i < 26; i++)
												{
													Player2.Hand.Insert(0, Player1.Hand.Last());
													Player1.Hand.Remove(Player1.Hand.Last());
												};
												resultLabel.Text += "<br>Player 1 has " + Player1.Hand.Count.ToString() + " cards." + "<br>Player 2 has " + Player2.Hand.Count.ToString() + " cards.<br><br>";
											}
											else
												TieGame.IsWinner = true;
										}
									}
								}
							}
						}
					}
				}

				// determine and display winner
				if (Player1.Hand.Count == 0) Player2.IsWinner = true;
				if (Player2.Hand.Count == 0) Player1.IsWinner = true;
				if (Player1.IsWinner)
				{
					resultLabel.Text += "<h2>Player 1 wins the game!</h2>";
				}
				else if (Player2.IsWinner)
				{
					resultLabel.Text += "<h2>Player 2 wins the game!</h2>";
				}
				else if (TieGame.IsWinner)
				{
					resultLabel.Text += "<h2>On this battlefield no one wins.";
				}
			}
		}
	}
}