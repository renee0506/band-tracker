using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker
{
  public class Band
  {
    private int _id;
    private string _name;

    public Band(string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM bands;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static List<Band> GetAll()
    {
      List<Band> allBands = new List<Band>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Band newBand = new Band(name, id);
        allBands.Add(newBand);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allBands;
    }

    public override bool Equals(System.Object otherBand)
    {
      if(!(otherBand is Band))
      {
        return false;
      }
      else
      {
        Band newBand = (Band) otherBand;
        bool idEquality = (this.GetId() == newBand.GetId());
        bool nameEquality = (this.GetName() == newBand.GetName());
        return (idEquality && nameEquality);
      }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands (name) OUTPUT INSERTED.id VALUES (@BandName);", conn);
      cmd.Parameters.Add(new SqlParameter("@BandName", this.GetName()));

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Band Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id = @BandId;", conn);
      cmd.Parameters.Add(new SqlParameter("@BandId", id.ToString()));
      SqlDataReader rdr = cmd.ExecuteReader();

      int BandId = 0;
      string BandName = null;

      while (rdr.Read())
      {
        BandId = rdr.GetInt32(0);
        BandName = rdr.GetString(1);
      }

      Band foundBand = new Band(BandName, BandId);
      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundBand;
    }

    public void AddVenue(Venue newVenue)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (band_id, venue_id) OUTPUT INSERTED.id VALUES (@BandID, @VenueId);", conn);
      cmd.Parameters.Add(new SqlParameter("@VenueId", newVenue.GetId()));
      cmd.Parameters.Add(new SqlParameter("@BandId", this.GetId()));
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Venue> GetVenues()
    {
      List<Venue> allVenues = new List<Venue> {};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT venues.* FROM bands JOIN bands_venues ON (bands.id = bands_venues.band_id) JOIN venues ON (venues.id = bands_venues.venue_id) WHERE bands.id =@BandId;", conn);
      cmd.Parameters.Add(new SqlParameter("@BandId", this.GetId()));
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int Id = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        Venue newVenue = new Venue(venueName, Id);
        allVenues.Add(newVenue);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allVenues;
    }

  }
}
