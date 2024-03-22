using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using TMPro;

public class PlayerScoreManager : MonoBehaviour
{
    private string connectString;
    private List<PlayerScore> playerScores = new List<PlayerScore>();

    // Prefab to generate strings for rank, name and score
    [SerializeField] private GameObject scorePrefab;

    // Prefab where scorePrefab will be generated under
    [SerializeField] private Transform scoreParent;

    // the amount of ranks we want to see (ex: top 10, top 20)
    [SerializeField] private int topRanks;

    // variable to delete scores under topRanks
    [SerializeField] private int topScores;

    [SerializeField] private GameObject userName;
    [SerializeField] private GameObject nameDialog;


    // Start is called before the first frame update
    void Start()
    {
        // Loads the file for HighScoreDB where we'll store score and name 
        connectString = "URI=file:" + Application.dataPath + "/PlayerScoreDB.sqlite";

        CreateTable();

        // Initialize the score table
        using (IDbConnection dbConnection = new SqliteConnection(connectString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                // Insert new score and name into row
                string createTable = "CREATE TABLE IF NOT EXISTS PlayerScores (PlayerID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL  UNIQUE , Name TEXT NOT NULL , Score INTEGER NOT NULL , Date DATETIME NOT NULL DEFAULT CURRENT_DATE)";

                dbCmd.CommandText = createTable;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }

        // Call Insert Score
        // InsertScore("Annie", 160);

        // Call Delete Score
        // DeleteScore(11); 

        // Call GetScores
        // GetScores();
        ShowScores();

        // Call GetRidOfScores()
        GetRidOfScores();
    }

    public void SetString(string userName)
    {
        this.userName.GetComponent<TextMeshProUGUI>().text = userName;
    }

    void Update()
    {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                nameDialog.SetActive(!nameDialog.activeSelf);
            }
    }

    public void UserName()
    {
        if (userName.GetComponent<TextMeshProUGUI>().text != string.Empty)
        {
            int score = (int)GameManager.Instance.TotalScore;
            InsertScore(userName.GetComponent<TextMeshProUGUI>().text, score);
            userName.GetComponent<TextMeshProUGUI>().text = string.Empty;
            nameDialog.SetActive(false);

            ShowScores();
        }
    }

    // This method will insert the name and score of player inside our database
    private void InsertScore(string name, int newScore)
    {
        // connect to Database
        using (IDbConnection dbConnection = new SqliteConnection(connectString))
        {
            GetScores();

            if (playerScores.Count > 0)
            {
                PlayerScore lowestScore = playerScores[playerScores.Count - 1];

                // Check for null
                if (lowestScore != null && topScores > 0 && playerScores.Count >= topScores && newScore > lowestScore.Score)
                {
                    DeleteScore(lowestScore.ID);
                }
            }

            if (playerScores.Count < topScores)
            {
                // Open db
                dbConnection.Open();
                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                    // Insert new score and name into row
                    string sqlQuery = String.Format("INSERT INTO PlayerScores(Name,Score) VALUES(\"{0}\",\"{1}\") ", name, newScore);

                    dbCmd.CommandText = sqlQuery;
                    dbCmd.ExecuteScalar();
                    dbConnection.Close();
                }
            }
        }
    }

    // This method will create tables in Build
    private void CreateTable()
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectString))
        {
            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                // Insert new score and name into row
                string createTable = String.Format("CREATE TABLE if not exists PlayerScores (PlayerID INTEGER PRIMARY KEY  AUTOINCREMENT  NOT NULL  UNIQUE , Name TEXT NOT NULL , Score INTEGER NOT NULL , Date DATETIME NOT NULL  DEFAULT CURRENT_DATE)");

                dbCmd.CommandText = createTable;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
        }
    }

    // This method will delete a row of scores
    private void DeleteScore(int id)
    {
        // connect to Database
        using (IDbConnection dbConnection = new SqliteConnection(connectString))
        {
            // Open db
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                // Delete row and column
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
        using (IDbConnection dbConnection = new SqliteConnection(connectString))
        {
            // Open db
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
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

        foreach (GameObject score in GameObject.FindGameObjectsWithTag("Score"))
        {
            Destroy(score);
        }

        // Ensure that topRanks does not exceed the available playerScores count
        int count = Mathf.Min(topRanks, playerScores.Count);

        for (int i = 0; i < count; i++)
        {
            // Instantiates the generated prefabs based on the data inside db
            GameObject tempObject = Instantiate(scorePrefab);

            PlayerScore tempScore = playerScores[i];

            tempObject.GetComponent<ScoreBoard>().SetScore(tempScore.Name, tempScore.Score.ToString(), "#" + (i + 1).ToString());

            // Set the parent where we'll generate these prefabs under(location)

            tempObject.transform.SetParent(scoreParent);

            tempObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        }
    }

    // This method gets rid of scores that are <= playerScores.Count
    private void GetRidOfScores()
    {
        GetScores();
        
        if (topScores <= playerScores.Count)
        {
            int deleteScores = playerScores.Count - topScores;

            // run in reverse from list of scores
            playerScores.Reverse();

            using (IDbConnection dbConnection = new SqliteConnection(connectString))
            {
                // Open db
                dbConnection.Open();

                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                    for (int i = 0; i < deleteScores; i++)
                    {
                        string sqlQuery = String.Format("DELETE FROM PlayerScores WHERE PlayerID = \"{0}\"", playerScores[i]);
                        dbCmd.CommandText = sqlQuery;
                        dbCmd.ExecuteScalar();
                        dbConnection.Close();
                    }
                }
            }
        }
    }
}
