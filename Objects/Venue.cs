using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker
{
  public class Venue
  {
    private int _id;
    private string _name;

    public Venue(string Name, int Id = 0)
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
      SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        Venue newVenue = new Venue(name, id);
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

    public override bool Equals(System.Object otherVenue)
    {
      if(!(otherVenue is Venue))
      {
        return false;
      }
      else
      {
        Venue newVenue = (Venue) otherVenue;
        bool idEquality = (this.GetId() == newVenue.GetId());
        bool nameEquality = (this.GetName() == newVenue.GetName());
        return (idEquality && nameEquality);
      }
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues (name) OUTPUT INSERTED.id VALUES (@VenueName);", conn);
      cmd.Parameters.Add(new SqlParameter("@VenueName", this.GetName()));

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

    public static Venue Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @VenueId;", conn);
      cmd.Parameters.Add(new SqlParameter("@VenueId", id.ToString()));
      SqlDataReader rdr = cmd.ExecuteReader();

      int VenueId = 0;
      string VenueName = null;

      while (rdr.Read())
      {
        VenueId = rdr.GetInt32(0);
        VenueName = rdr.GetString(1);
      }

      Venue foundVenue = new Venue(VenueName, VenueId);
      if(rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundVenue;
    }

    public void AddBand(Band newBand)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues (band_id, venue_id) OUTPUT INSERTED.id VALUES (@BandId, @VenueID);", conn);
      cmd.Parameters.Add(new SqlParameter("@BandId", newBand.GetId()));
      cmd.Parameters.Add(new SqlParameter("@VenueId", this.GetId()));
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Band> GetBands()
    {
      List<Band> allBands = new List<Band> {};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT bands.* FROM venues JOIN bands_venues ON (venues.id = bands_venues.venue_id) JOIN bands ON (bands.id = bands_venues.band_id) WHERE venues.id =@VenueId;", conn);
      cmd.Parameters.Add(new SqlParameter("@VenueId", this.GetId()));
      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        int Id = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        Band newBand = new Band(bandName, Id);
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

    public void Update(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE venues SET name = @newName OUTPUT INSERTED.name WHERE id = @Id;", conn);

      SqlParameter IdParameter = new SqlParameter();
      IdParameter.ParameterName = "@Id";
      IdParameter.Value = this.GetId();
      cmd.Parameters.Add(IdParameter);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@newName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while (rdr.Read())
      {
        this._name = rdr.GetString(0);
      }
      if(rdr!= null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }
    
  }
}
