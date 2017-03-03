using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Xunit;

namespace BandTracker
{
  public class BandTest : IDisposable
  {
    public BandTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }

    [Fact]
    public void GetAll_DatabaseEmptyAtFirst_Empty()
    {
      int result = Band.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Equals_EntryIsEqual_true()
    {
      Band band1 = new Band ("Rocket");
      Band band2 = new Band ("Rocket");

      Assert.Equal(band1, band2);
    }

    [Fact]
    public void Save_SaveToDatabase_Save()
    {
      Band testBand = new Band("THE SPACE");
      testBand.Save();

      List<Band> result = Band.GetAll();
      List<Band> expected = new List<Band>{testBand};

      Assert.Equal(expected, result);
    }

    [Fact]
    public void Save_SaveToDatabase_SaveWithId()
    {
      Band testBand = new Band("THE SPACE");
      testBand.Save();
      Band savedBand = Band.GetAll()[0];

      int result = savedBand.GetId();
      int expected = testBand.GetId();

      Assert.Equal(expected, result);
    }

    [Fact]
    public void Find_VenueId_ReturnVenueFromDatabase()
    {
      Band testBand = new Band("THE SPACE");
      testBand.Save();

      Band foundBand = Band.Find(testBand.GetId());

      Assert.Equal(testBand, foundBand);
    }


  }
}
