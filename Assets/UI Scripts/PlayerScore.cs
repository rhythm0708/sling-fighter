using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;

// Used IComparable to be able to sort the scores
public class PlayerScore : IComparable<PlayerScore>
{
    public int Score { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public int ID { get; set; }

    public PlayerScore(int id, int score, string name, DateTime date)
    {
        this.Score = score;
        this.Name = name;
        this.ID = id;
        this.Date = date;
    }

    // Method to compare and be able to sort based on Score and Date
    public int CompareTo(PlayerScore other)
    {
        if (other.Score < this.Score)
        {
            return -1;
        }
        else if (other.Score > this.Score)
        {
            return 1;
        }
        if (other.Date < this.Date)
        {
            return -1;
        }
        else if (other.Date > this.Date)
        {
            return 1;
        }
        return 0;

    }
}
