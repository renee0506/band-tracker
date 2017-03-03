using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace BandTracker
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Band> allBands = Band.GetAll();
        List<Venue> allVenues = Venue.GetAll();
        Dictionary<string, object> Model = new Dictionary<string, object>{{"bands", allBands}, {"venues", allVenues}};
        return View["index.cshtml", Model];
      };

      Post["/band/new"] = _ => {
        Band newBand = new Band(Request.Form["band-name"]);
        newBand.Save();
        List<Venue> thisBandVenues = newBand.GetVenues();
        List<Venue> allVenues = Venue.GetAll();
        Dictionary<string, object> Model = new Dictionary<string, object>{{"band", newBand},{"venues", thisBandVenues},{"all-venues", allVenues}};
        return View["band.cshtml", Model];
      };

      Post["/venue/new"] = _ => {
        Venue newVenue = new Venue(Request.Form["venue-name"]);
        newVenue.Save();
        List<Band> thisVenueBands = newVenue.GetBands();
        List<Band> allBands = Band.GetAll();
        Dictionary<string, object> Model = new Dictionary<string, object>{{"venue", newVenue},{"bands", thisVenueBands},{"all-bands", allBands}};
        return View["venue.cshtml", Model];
      };

      Post["/band/{id}/venues/new"] = parameters => {
        Venue addedVenue = Venue.Find(Request.Form["new-venue"]);
        Band thisBand = Band.Find(parameters.id);
        thisBand.AddVenue(addedVenue);
        List<Venue> thisBandVenues = thisBand.GetVenues();
        List<Venue> allVenues = Venue.GetAll();
        Dictionary<string, object> Model = new Dictionary<string, object>{{"band", thisBand},{"venues", thisBandVenues},{"all-venues", allVenues}};
        return View["band.cshtml", Model];
      };

      Post["/venue/{id}/bands/new"] = parameters => {
        Band addedBand = Band.Find(Request.Form["new-band"]);
        Venue thisVenue = Venue.Find(parameters.id);
        thisVenue.AddBand(addedBand);
        List<Band> thisVenueBands = thisVenue.GetBands();
        List<Band> allBands = Band.GetAll();
        Dictionary<string, object> Model = new Dictionary<string, object>{{"venue", thisVenue},{"bands", thisVenueBands},{"all-bands", allBands}};
        return View["venue.cshtml", Model];
      };

      Get["/band/{id}"] = parameters => {
        Band thisBand = Band.Find(parameters.id);
        List<Venue> thisBandVenues = thisBand.GetVenues();
        List<Venue> allVenues = Venue.GetAll();
        Dictionary<string, object> Model = new Dictionary<string, object>{{"band", thisBand},{"venues", thisBandVenues},{"all-venues", allVenues}};
        return View["band.cshtml", Model];
      };

      Get["/venue/{id}"] = parameters => {
        Venue thisVenue = Venue.Find(parameters.id);
        List<Band> thisVenueBands = thisVenue.GetBands();
        List<Band> allBands = Band.GetAll();
        Dictionary<string, object> Model = new Dictionary<string, object>{{"venue", thisVenue},{"bands", thisVenueBands},{"all-bands", allBands}};
        return View["venue.cshtml", Model];
      };

      Get["/venue/{id}/delete"] = parameters => {
        Venue thisVenue = Venue.Find(parameters.id);
        List<Band> allBands = Band.GetAll();
        Dictionary<string, object> Model = new Dictionary<string, object> {{"venue", thisVenue}, {"bands", allBands}};
        return View["venue_delete.cshtml", Model];
      };

      Delete["/venue/{id}/delete"] = parameters => {
        Venue thisVenue = Venue.Find(parameters.id);
        thisVenue.Delete();
        List<Band> allBands = Band.GetAll();
        List<Venue> allVenues = Venue.GetAll();
        Dictionary<string, object> Model = new Dictionary<string, object>{{"bands", allBands}, {"venues", allVenues}};
        return View["index.cshtml", Model];
      };

      Get["/venue/{id}/update"] = parameters => {
        Venue thisVenue = Venue.Find(parameters.id);
        List<Band> allBands = Band.GetAll();
        Dictionary<string, object> Model = new Dictionary<string, object> {{"venue", thisVenue}, {"bands", allBands}};
        return View["venue_update.cshtml", Model];
      };

      Patch["/venue/{id}/update"] = parameters => {
        Venue thisVenue = Venue.Find(parameters.id);
        thisVenue.Update(Request.Form["new-venue-name"]);
        List<Band> allBands = Band.GetAll();
        List<Venue> allVenues = Venue.GetAll();
        Dictionary<string, object> Model = new Dictionary<string, object>{{"bands", allBands}, {"venues", allVenues}};
        return View["index.cshtml", Model];
      };
    }
  }
}
