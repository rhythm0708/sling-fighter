using System.Collections;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;

public class HighScoreManager : MonoBehaviour
{
    private string connectString;

    // Start is called before the first frame update
    void Start()
    {
        // Loads the file for HighScoreDB where we'll store score and name 
        connectString = "URI=file:" + Application.dataPath + "/HighScores.sqlite";

        // Call Insert Score
        //InsertScore("Ken", 10);

        // Call Delete Score
        // DeleteScore(1);
        // Call GetScores
        GetScores();

    }

    // Update is called once per frame
    void Update()
    {
        
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
                string sqlQuery = String.Format("INSERT INTO HighScores(Name,Score) VALUES(\"{0}\",\"{1}\") ", name,newScore);

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
                string sqlQuery = String.Format("DELETE FROM HighScores WHERE PlayerID = \"{0}\"", id);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    // This method gets/reads the score and name stored inside our database
    private void GetScores()
    {   
        // connect to Database
        using(IDbConnection dbConnection = new SqliteConnection(connectString))
        {   
            // Open db
            dbConnection.Open();

            using(IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                // inside Database Query selects the name and score to read
                string sqlQuery = "SELECT * FROM HighScores";

                dbCmd.CommandText = sqlQuery;
                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.Log(reader.GetString(1) + " " + reader.GetInt32(2));
                    }

                    // close the connection
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }
}
