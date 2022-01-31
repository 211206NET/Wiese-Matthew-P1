using CustomExceptions;
using System.Text.RegularExpressions;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace Models;

public class Game
{
    
    public Game(){}

    public Game(DataRow row)
    {
        this.GameID = (int) row["GameID"];
        this.TrackTurn = (int) row["TrackTurn"];
        this.P1x = (int)row["P1x"];
        this.P1y = (int)row["P1y"];
        this.P2x = (int)row["P2x"];
        this.P2y = (int)row["P2y"];
        this.P3x = (int)row["P3x"];
        this.P3y = (int)row["P3y"];
        this.P4x = (int)row["P4x"];
        this.P4y = (int)row["P4y"];
        //this.PlayerXPositions = (int[]) row["City"];
        //this.PlayerYPositions = (int[]) row["State"];
    }

    public int GameID { get; set; }//Each different Game has an ID [PK]
    public int TrackTurn { get; set; } //Server will track whose turn it is 

    //Old position for each player will be stored locally for each player,
    //this will send new position data to each player via the database
    public double P1x { get; set; }
    public double P1y { get; set; }
    public double P2x { get; set; }
    public double P2y { get; set; }
    public double P3x { get; set; }
    public double P3y { get; set; }
    public double P4x { get; set; }
    public double P4y { get; set; }

    //This is just a tech demo proof of concept, who cares about using arrays, make it work
    //public int[] PlayerXPositions = new int[4]; //Tracks the X coord for each player
    //public int[] PlayerYPositions = new int[4]; //Tracks the Y coord fo reach player


    public void ToDataRow(ref DataRow row)
    {
        //row["GameId"] = this.GameID;
        row["TrackTurn"] = this.TrackTurn;
        row["P1x"] = this.P1x;
        row["P1y"] = this.P1y;
        row["P2x"] = this.P2x;
        row["P2y"] = this.P2y;
        row["P3x"] = this.P3x;
        row["P3y"] = this.P3y;
        row["P4x"] = this.P4x;
        row["P4y"] = this.P4y;
        //row["PlayerXPositions"] = this.PlayerXPositions;
        //row["PlayerYPositions"] = this.PlayerYPositions;
    }

}