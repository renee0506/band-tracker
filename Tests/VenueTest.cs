using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Xunit;

namespace BandTracker
{
  public class VenueTest : IDisposable
  {
    public VenueTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Venue.DeleteAll();
    }

    [Fact]
    public void GetAll_DatabaseEmptyAtFirst_Empty()
    {
      int result = Venue.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Equals_EntryIsEqual_true()
    {
      Venue venue1 = new Venue ("THE SPACE");
      Venue venue2 = new Venue ("THE SPACE");

      Assert.Equal(venue1, venue2);
    }

    [Fact]
    public void Save_SaveToDatabase_Save()
    {
      Venue testVenue = new Venue ("THE SPACE");
      testVenue.Save();

      List<Venue> result = Venue.GetAll();
      List<Venue> expected = new List<Venue>{testVenue};

      Assert.Equal(expected, result);
    }

    [Fact]
    public void Save_SaveToDatabase_SaveWithId()
    {
      Venue testVenue = new Venue ("THE SPACE");
      testVenue.Save();
      Venue savedVenue = Venue.GetAll()[0];

      int result = savedVenue.GetId();
      int expected = testVenue.GetId();

      Assert.Equal(expected, result);
    }

    [Fact]
    public void Find_VenueId_ReturnVenueFromDatabase()
    {
      //Arrange
      Venue testVenue = new Venue ("THE SPACE");
      testVenue.Save();

      //Act
      Venue foundVenue = Venue.Find(testVenue.GetId());

      //Assert
      Assert.Equal(testVenue, foundVenue);
    }
  }
}
