using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2023_7
{
    internal class Hand : IComparable<Hand>
    {
        public string Cards { get; }
        public int CombinationScore { get; }
        public int HandValue { get; }
        public Hand(string cards, bool joker)
        {
            Cards = cards;
            if (joker) Cards = Cards.Replace('J', '0');
            SortedDictionary<char, int> card_count = new();
            foreach (var c in Cards)
                card_count[c] = (card_count.ContainsKey(c) ? card_count[c] : 0) + 1;
            var card_countSorted = (from cc in card_count.Where(card => card.Key != '0') orderby cc.Value descending select cc).ToList();

            if (card_count.ContainsKey('0'))
            {
                if (card_countSorted.Count > 0)
                    card_countSorted[0] = new(card_countSorted[0].Key, card_countSorted[0].Value + card_count['0']);
                else
                    card_countSorted.Add(new('0', card_count['0']));
            }

            CombinationScore = card_countSorted.First().Value switch
            {
                5 => 7,
                4 => 6,
                3 => card_countSorted.ElementAt(1).Value == 2 ? 5 : 4,
                2 => card_countSorted.ElementAt(1).Value == 2 ? 3 : 2,
                1 => 1,
                _ => throw new Exception("Wrong hand bucko")
            };
            HandValue = 0;
            for (int i = 0; i < Cards.Count(); i++)
            {
                int cardValue = cardValues.ContainsKey(Cards[i])
                                ? cardValues[Cards[i]]
                                : Cards[i] - '0';
                HandValue += Convert.ToInt32((Math.Pow(15, Cards.Count() - i))) * cardValue;
            }
        }

        static Dictionary<char, int> cardValues = new()
    {
        {'A',14 },
        {'K',13 },
        {'Q',12 },
        {'J',11 },
        {'T',10 },
    };

        public int CompareTo(Hand? other)
        {
            if (CombinationScore != other.CombinationScore)
                return CombinationScore - other.CombinationScore;
            return HandValue - other.HandValue;
        }
    }
}
