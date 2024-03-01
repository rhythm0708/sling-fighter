using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;

public class PlayerScoreManager : MonoBehaviour
{
    private string connectString;
    private List<PlayerScore> playerScores = new List<PlayerScore>();
    [SerializeField]private GameObject scorePrefab;
    [SerializeField]private Transform scoreParent;
    [SerializeField]private int topRanks;

    // Start is called before the first frame update
    void Start()
    {
        // Loads the file for HighScoreDB where we'll store score and name 
        connectString = "URI=file:" + Application.dataPath + "/PlayerScoreDB.sqlite";

        // Call Insert Score
        // InsertScore("Annie", 16);
        // InsertScore("Ken", 100);

        // Call Delete Score
        // DeleteScore(11); 

        // Call GetScores
        // GetScores();
        ShowScores();

    }

    // This method will insert the name and score of player inside our database
    private void InsertScore(string name, int newScore)
    {   
        // connect to Database
        using(IDbConnection dbConnection = new SqliteConnection(connectString))
        {   
            // Open db
            dbConnection.Open();

            using(IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                // Insert new score and name into row
                string sqlQuery = String.Format("INSERT INTO PlayerScores(Name,Score) VALUES(\"{0}\",\"{1}\") ", name,newScore);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }
    // This method will delete a row of scores
    private void DeleteScore(int id)
    {   
        // connect to Database
        using(IDbConnection dbConnection = new SqliteConnection(connectString))
        {   
            // Open db
            dbConnection.Open();

            using(IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                // Insert new score and name into row
                string sqlQuery = String.Format("DELETE FROM PlayerScores WHERE PlayerID = \"{0}\"", id);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    // This method gets/reads the score and name stored inside our database
    private void GetScores()
    {   
        playerScores.Clear();
        // connect to Database
        using(IDbConnection dbConnection = new SqliteConnection(connectString))
        {   
            // Open db
            dbConnection.Open();

            using(IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                // inside Database Query selects the name and score to read
                string sqlQuery = "SELECT * FROM PlayerScores";

                dbCmd.CommandText = sqlQuery;
                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        playerScores.Add(new PlayerScore(reader.GetInt32(0), reader.GetInt32(2), reader.GetString(1), reader.GetDateTime(3)));
                    }

                    // close the connection
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        playerScores.Sort();
    }

    private void ShowScores()
    {
        GetScores();

        // Ensure that topRanks does not exceed the available playerScores count
        int count = Mathf.Min(topRanks, playerScores.Count);

        for (int i = 0; i < count; i++)
        {
            GameObject tempObject = Instantiate(scorePrefab);

            PlayerScore tempScore = playerScores[i];

            tempObject.GetComponent<ScoreBoard>().SetScore(tempScore.Name, tempScore.Score.ToString(), "#" + (i + 1).ToString());

            tempObject.transform.SetParent(scoreParent);

            tempObject.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
            
        }
    }
}
