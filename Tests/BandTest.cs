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


  }
}
