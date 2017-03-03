# Band Tracker

#### A web app that tracks bands and places they've played at.

#### By Renee Mei

## Description

The application allows user to track a list of band and the venues they have played at. The application also allows user to see which band has played at a certain venue. User can update and delete venues as they like.

## Setup/Installation Requirements

* Clone From GitHub
* In SQL Do the Following:

>CREATE DATABASE band_tracker;

>GO

>USE band_tracker;

>GO

>CREATE TABLE venues (id INT IDENTITY(1,1), name VARCHAR(255));

>CREATE TABLE bands (id INT IDENTITY(1,1), name VARCHAR(255));

>CREATE TABLE bands_venues (id INT IDENTITY(1,1), band_id INT, venue_id INT);

>GO

*  Now open the website using dnx kestrel
* Go to localhost:5004 to view

## Specification

#### Class: Band

**The GetAll method will return an empty list if the list of band is empty in the beginning**

**The Assert.Equal method will return true if two band has the exactly same name**

**The Save method will save the information of a new band to the database**

**The Save method will save the information of a new band to the database, and the new band should have an id assigned**

**The DeleteAll method will clear all content in the bands table and the bands_venues table**

**The Find method will find a specific band in the database by its id**

**The GetVenues method will find all the vanues that the band has played at.**

**The AddVenue method will add a venue that the band has played at.**

#### Class: venues
**The GetAll method will return an empty list if the list of venues is empty in the beginning**

**The Assert.Equal method will return true if two venues has the exactly same name**

**The Save method will save the information of a new venue to the database**

**The Save method will save the information of a new venue to the database, and the new venue should have an id assigned**

**The DeleteAll method will clear all content in the venues table and the bands_venues table**

**The Find method will find a specific venue in the database by its id**

**The Update method will update the name of a venue**

**The Delete method will delete the entry of venue from venues table and all the corrresponding table in the bands_venues table**

**The GetBands method will find all the bands that have played at a venue**

**The AddBand method will add a band to a venue**

## Support and contact details

Contact Renee Mei at meiqianye@gmail.com

## Technologies Used

This web application uses:
* Nancy
* Mono
* DNVM
* C#
* Razor
* Materialize
* Microsoft SQL Command
* Microsoft SQL Server Management Studio

### License

*This project is licensed under the MIT license.*

Copyright (c) 2017 *Tammy Dang*
