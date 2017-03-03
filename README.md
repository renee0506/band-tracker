# Band Tracker

#### A web app that tracks bands and places they've played at.

#### By Renee Mei

## Description
*


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
* Example Input: function call
* Example Output: List<Stylist>{ }

**The Assert.Equal method will return true if two band has the exactly same name**
* Example Input: Stylist1, Stylist1
* Example Output: true

**The Save method will save the information of a new band to the database**
* Example Input: Stylist1
* Example Output: Stylist1 saved to table: stylists

**The Save method will save the information of a new band to the database, and the new band should have an id assigned**
* Example Input: Stylist1
* Example Output: Stylist1 has an id

**The DeleteAll method will clear all content in the bands table and the bands_venues table**
* Example Input: DeleteAll( )
* Example Output: Table: stylists is empty

**The Find method will find a specific band in the database by its id**
* Example Input: 1
* Example Output: Stylist1

**The GetVenues method will find all the vanues that the band has played at.**
* Example Input: Stylist1.GetClients( )
* Example Output: List<Client>{Client1, Client2}

#### Class: venues
**The GetAll method will return an empty list if the list of venues is empty in the beginning**
* Example Input: Client.GetAll( )
* Example Output: List<Client>{ }

**The Assert.Equal method will return true if two venues has the exactly same name**
* Example Input: Client1, Client1
* Example Output: true

**The Save method will save the information of a new venue to the database**
* Example Input: Client1.Save( )
* Example Output: Client1 saved to table: venues

**The Save method will save the information of a new venue to the database, and the new venue should have an id assigned**
* Example Input: Client1.Save( )
* Example Output: Client1 has an id

**The DeleteAll method will clear all content in the venues table and the bands_venues table**
* Example Input: Client.DeleteAll( )
* Example Output: Table: venues is empty

**The Find method will find a specific venue in the database by its id**
* Example Input: 1
* Example Output: Client1

**The Update method will update the name of a venue**
* Example Input: Client1.Update("Patria")
* Example Output: Client1.GetName = "Patria"

**The Delete method will delete the entry of venue from venues table and all the corrresponding table in the bands_venues table**
* Example Input: Client1.Delete( )
* Example Output: Table: venues does not have Client1's information anymore


## Support and contact details

Contact Renee Mei at meiqianye@gmail.com

## Technologies Used

This web application uses:
* Nancy
* Mono
* DNVM
* C#
* Razor
* Microsoft SQL Command
* Microsoft SQL Server Management Studio

### License

*This project is licensed under the MIT license.*

Copyright (c) 2017 *Tammy Dang*
