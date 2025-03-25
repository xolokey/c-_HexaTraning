--CODE CHALLANGE

-- Create the Artists table 
CREATE TABLE Artists ( 
ArtistID INT PRIMARY KEY, 
Name VARCHAR(255) NOT NULL, 
Biography TEXT, 
Nationality VARCHAR(100));

-- Create the Categories table 
CREATE TABLE Categories ( 
CategoryID INT PRIMARY KEY, 
Name VARCHAR(100) NOT NULL); 

-- Create the Artworks table 
CREATE TABLE Artworks ( 
ArtworkID INT PRIMARY KEY, 
Title VARCHAR(255) NOT NULL, 
ArtistID INT, 
CategoryID INT, 
Year INT, 
Description TEXT, 
ImageURL VARCHAR(255), 
FOREIGN KEY (ArtistID) REFERENCES Artists (ArtistID), 
FOREIGN KEY (CategoryID) REFERENCES Categories (CategoryID));

-- Create the Exhibitions table 
CREATE TABLE Exhibitions ( 
ExhibitionID INT PRIMARY KEY, 
Title VARCHAR(255) NOT NULL, 
StartDate DATE, 
EndDate DATE, 
Description TEXT); 

-- Create a table to associate artworks with exhibitions 
CREATE TABLE ExhibitionArtworks ( 
ExhibitionID INT, 
ArtworkID INT, 
PRIMARY KEY (ExhibitionID, ArtworkID), 
FOREIGN KEY (ExhibitionID) REFERENCES Exhibitions (ExhibitionID), 
FOREIGN KEY (ArtworkID) REFERENCES Artworks (ArtworkID)); 

-- Insert sample data into the Artists table 
INSERT INTO Artists (ArtistID, Name, Biography, Nationality) VALUES 
(1, 'Pablo Picasso', 'Renowned Spanish painter and sculptor.', 'Spanish'), 
(2, 'Vincent van Gogh', 'Dutch post-impressionist painter.', 'Dutch'), 
(3, 'Leonardo da Vinci', 'Italian polymath of the Renaissance.', 'Italian'); 

-- Insert sample data into the Categories table 
INSERT INTO Categories (CategoryID, Name) VALUES 
(1, 'Painting'), 
(2, 'Sculpture'), 
(3, 'Photography');

-- Insert sample data into the Artworks table 
INSERT INTO Artworks (ArtworkID, Title, ArtistID, CategoryID, Year, Description, ImageURL) VALUES 
(1, 'Starry Night', 2, 1, 1889, 'A famous painting by Vincent van Gogh.', 'starry_night.jpg'), 
(2, 'Mona Lisa', 3, 1, 1503, 'The iconic portrait by Leonardo da Vinci.', 'mona_lisa.jpg'), 
(3, 'Guernica', 1, 1, 1937, 'Pablo Picassos powerful anti-war mural.', 'guernica.jpg');

-- Insert sample data into the Exhibitions table 
INSERT INTO Exhibitions (ExhibitionID, Title, StartDate, EndDate, Description) VALUES 
(1, 'Modern Art Masterpieces', '2023-01-01', '2023-03-01', 'A collection of modern art masterpieces.'), 
(2, 'Renaissance Art', '2023-04-01', '2023-06-01', 'A showcase of Renaissance art treasures.'); 


-- Insert artworks into exhibitions 
INSERT INTO ExhibitionArtworks (ExhibitionID, ArtworkID) VALUES 
(1, 1), 
(1, 2), 
(1, 3), 
(2, 2); 

select* from Artists
select* from Artworks

select* from Categories
select* from ExhibitionArtworks

select* from Exhibitions


--1 Retrieve the names of all artists along with the number of artworks they have in the gallery and list them in descending order of the number of artworks

select a.Name,count(ar.ArtworkID) as Artworkcount
from Artists a
left join Artworks ar on a.ArtistID=ar.ArtistID
group by a.Name
order by Artworkcount desc;

--2 List the titles of artworks created by artists from 'Spanish' and 'Dutch' nationalities, and order them by the year in ascending order.
 
select ar.Title , ar.Year
from Artworks ar
join Artists a on ar.ArtistID=a.ArtistID
where a.Nationality in ('Spanish','Dutch')
order by ar.Year asc;

select* from Artworks
select* from Artists

--3 names of all artists who have artworks in the 'Painting' category, and the number of artworks they have in this category
  
select a.Name,count(ar.ArtworkID) as Artworkcount
from Artists a
join Artworks ar on  a.ArtistID=ar.ArtistID
join Categories c on ar.CategoryID=c.CategoryID
where c.Name='painting'
group by a.Name;

 
select*from Categories
select*from Artists

--4  names of artworks from the 'Modern Art Masterpieces' exhibition, along with their artists and categories. 
select* from Categories
select* from Exhibitions
select* from Artists
select* from Artworks

select ar.Title, a.Name as Artist, c.Name as Category
from ExhibitionArtworks ea
join Artworks ar on ea.ArtworkID = ar.ArtworkID
join Artists a on ar.ArtistID = a.ArtistID
join Categories c on ar.CategoryID = c.CategoryID
join Exhibitions e on ea.ExhibitionID = e.ExhibitionID
where e.Title = 'Modern Art Masterpieces';

--5 the artists who have more than two artworks in the gallery
select*from Artworks
select*from Artists

select a.Name, count(ar.ArtistID) as ArtistCount
from Artists a
join Artworks ar on a.ArtistID = ar.ArtistID
group by a.Name
having count(ar.ArtistID) > 2;

INSERT INTO Artworks (ArtworkID, Title, ArtistID, CategoryID, Year, Description, ImageURL) VALUES 
(5, 'Startroops', 2, 1, 1899, 'A famous painting by john cartler.', 'star_troops.jpg');

--7 total no of artwork in catagories
select* from Artworks
select* from Categories

select c.Name as Categories ,count(ar.ArtworkID) as totalartwork
from Categories c
left join Artworks ar on  c.CategoryID=ar.CategoryID
group by c.Name;

--8 artist with more than  3 artworks in galery(update)
select*from Artists

select a.Name,count(ar.ArtworkID) as artistcount
from Artists a
join Artworks ar on a.ArtistID=ar.ArtistID
group by a.Name
having count(ar.ArtworkID)> 3;

--9 artwork created by artist from specific nationality

select* from Artworks

select ar.Title
from Artworks ar
join Artists a on ar.ArtistID=a.ArtistID
where a.Nationality='Dutch';

--11 all the artworks that have not been included in exhibition
select* from ExhibitionArtworks
select* from Artworks
select* from Exhibitions

Select a.ArtworkID, a.Title, a.ArtistID, a.CategoryID, a.Year, a.Description
from Artworks a
left join ExhibitionArtworks ea on a.ArtworkID = ea.ArtworkID
where ea.ExhibitionID is null;

--12 artist created artwork in all avilable catagories

select* from Categories
select* from Artists
select* from Artworks

select a.Name
from Artists a 
join Artworks ar on a.ArtistID=ar.ArtistID
join Categories c on ar.CategoryID=c.CategoryID
group by a.Name
having count(distinct ar.CategoryID)=(select count(*) from Categories);

--13 total no of artwork in each category

select c.Name,count(ar.ArtworkID) as totalartwork
from Categories c
join Artworks ar on c.CategoryID=ar.CategoryID
group by c.Name;

--14 artists who have more than 2 artworks in the gallery 

select a.Name, count(ar.ArtworkID) as ArtworkCount
from Artists a
join Artworks ar on a.ArtistID = ar.ArtistID
group by a.Name
having count(ar.ArtworkID) > 2;

--15 categories with the average year of artworks they contain, only for categories with more than 1 artwork

select c.Name as Category, AVG(ar.Year) as AvgYear
from Categories c
join Artworks ar on c.CategoryID = ar.CategoryID
group by c.Name
having count(ar.ArtworkID) > 1;

--16 artworks that were exhibited in the Modern Art Masterpieces
select*from ExhibitionArtworks
select* from Exhibitions

select ar.Title
from Artworks ar
join ExhibitionArtworks ea on ar.ArtworkID = ea.ArtworkID
join Exhibitions e on ea.ExhibitionID = e.ExhibitionID
where e.Title = 'Modern Art Masterpieces';

--17
select*from Artworks


select c.Name as Category, avg(ar.Year) as AvgYear
from Categories c
join Artworks ar on c.CategoryID = ar.CategoryID
group by c.Name
having avg(ar.Year) > (select avg(Year) from Artworks);

--18  artworks that were not exhibited in any exhibition
select*from ExhibitionArtworks
select* from Artworks

select ar.Title
from Artworks ar
left join ExhibitionArtworks ea on ar.ArtworkID = ea.ArtworkID
where ea.ExhibitionID IS NULL;

--19 artists who have artworks in the same category as Mona Lisa

select distinct a.Name
from Artists a
join Artworks ar on a.ArtistID = ar.ArtistID
where ar.CategoryID = (select CategoryID from Artworks where Title = 'Mona Lisa');

--20 the names of artists and the number of artworks they have in the gallery

select a.Name, count(ar.ArtworkID) as ArtworkCount
from Artists a
left join Artworks ar on a.ArtistID = ar.ArtistID
group by a.Name;