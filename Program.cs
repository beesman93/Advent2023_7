using Advent2023_7;

SortedList<Hand, int> hands = new();
SortedList<Hand, int> joker_hands = new();

using (StreamReader reader = new StreamReader(args[0]))
{
    while (!reader.EndOfStream)
    {
        string line = reader.ReadLine();
        string cards = line.Split(' ')[0];
        int bet = Convert.ToInt32(line.Split(' ')[1]);
        hands.Add(new Hand(cards, false), bet);
        joker_hands.Add(new Hand(cards, true), bet);
    }
}

ulong totalWinnings = 0;
ulong totalWinningsJoker = 0;
for (int i = 0; i < hands.Count(); i++)
{
    totalWinnings += Convert.ToUInt64(hands.ElementAt(i).Value * (i + 1));
    totalWinningsJoker += Convert.ToUInt64(joker_hands.ElementAt(i).Value * (i + 1));
}
Console.WriteLine(totalWinnings);
Console.WriteLine(totalWinningsJoker);