![homepage](https://user-images.githubusercontent.com/130514366/233876718-50f12ca1-9c64-40b3-821d-0327be8e4807.PNG)
# Welcome To Blockbuster
**BlockBuster Search** brings our catalogue of classic & new movies to your fingertips.
Search by movie Title, Cast, Ratings, and more.

![cast search](https://user-images.githubusercontent.com/130514366/233876725-1bd122cd-005c-463b-a3bc-4c5367e79562.PNG)

![rating search](https://user-images.githubusercontent.com/130514366/233876731-747f83c7-0aeb-4fc0-8f0c-038ce04f5882.PNG)

# For Users / Testing

## Solr Setup

### Download Java for Windows (if you don't already have it)
Download Java from www.java.com \n
Follow the prompts for download and install of Java for Windows

### Download Solr for Windows (if you don't already have it)
Download `Solr 8.11.2` Binary Release `solr-8.11.2.zip` for Windows.\nWhen the download is complete, unzip the file, and open the main directory in terminal: `cd solr-8.11.2\solr-8.11.2`

### (1) Start the Solr localhost server
It will run by default on `localhost:8983`\n
Start by running: `bin\solr start`

### (2) Create the new Blockbuster Solr Collection (will migrate Indexed data later)
(This part will not work if you do not have Java installed)\n 
In your terminal, run the following commands, and follow the prompts as they appear:\n
(1) `bin\solr create -c blockbuster -s 1 -rf 1`
// We are doing 1 shard and replica, for this demo. \n
// For ease of use,  will prevent potential bugs -- when trying to restart the Solr service, where some replicas / ports may be inactive, thereby not permitting search
\n\n

### (3) You will manually need to set each Column to string before indexing
Make sure the collection was created successfully by going in your browser to `localhost:8983/solr/#`. Then in the left column, click Core Selector to see if blockbuster is there.
\n\n
Select `blockbuster`\n
Then, we need to add the columns and types, so that the auto indexer does not confuse the dataset types.\n\n
Click Schema, and then add each of the following Fields by clicking Add Field for each:


name: Title
field type: string
Make sure only "stored" is checked off
Add Field


name: Released
field type: string
Make sure only "stored" is checked off
Add Field

name: Plot
field type: string
Make sure only "stored" is checked off
Add Field

name: Actors
field type: string
Make sure only "stored" is checked off
Add Field


name: Genre
field type: string
Make sure only "stored" is checked off
Add Field

name: Runtime
field type: string
Make sure only "stored" is checked off
Add Field


name: imdbRating
field type: string
Make sure only "stored" is checked off
Add Field


name: imdbID
field type: string
Make sure only "stored" is checked off
Add Field

### 4) Index the provided Movie Data; run this command in your Solr doqnload directory
For ease of use, you can move the `movieData` folder to your Desktop for this step.
In the Solr project directory, again in terminal, run:\n
`java -jar -Dc=blockbuster -Dauto example\exampledocs\post.jar C:\Users\<your_user>\Desktop\movieData\*.json`

Based on your computer, this indexing process should only take about 1 to 2 mins.

To test that this suceeded, go back into the Admin UI in your browser. Select the blockbuster Core, click the Query tab. In the `q` field, enter `*:*`, then scroll down, and click `Execute Query` (or a similarly named button). If it is successful, you should see JSON data to the right with Movie objects, having Title, Released, Cast, and more fields.

### (5) Shutdown server after use (can skip for now)
`bin\solr stop -all`

### (6) Resume server (can skip for now)
`bin\solr start -c -p 8983 -s example\cloud\node1\solr` (may be a different node, like node1 -- check the directory if it doesn't work)

**troubleshoot:** if errors are encoutered, try to delete all replicas on second port. have 1 replica per shard

## ASP.NET App Setup

Make sure your Solr sever is running. If you shut it down before, you will need to start it again using Solr step (6)


### (1) Open the Visual Studio Project in the GitHub repo by double clicking the file `BlockbusterRentals\BlockbusterRentals.sln`
All Package dependancies should be included in the Git repo. If not, you will need to use PM> to: `install-package SolrNet`


**troubleshoot:** On some machines, the Solr dataset migration may not line up to the same Node naming convention. This will cause the Solr file migration, and the Visual Studio code, to be out of sync.\n\n

Make sure that the node, shard, and replica name in the Solr Admin console in your browser MATCH the url in Visual studio (and for Solr Step 6). The Visual Studio line is in `Controllers\HomeController.cs`, `Search()`, `SolrNet.Startup.Init....("http://localhost:8983/solr/blockbuster_shard1_replica_n2")`.

You may need to change it to
`("http://localhost:8983/solr/blockbuster_shard1_replica_n1")`, or whatever `n<number>` your configuration setup.

Or, if you are setting up Solr for the first time, and you haven't used it before, you may need to use:
`("http://localhost:8983/solr/blockbuster")`

### (2) Click Build and Run in Visual Studio!
The Home controller Index action is where the demo app is targetted to launch from. Your browser should auto open, otherwise go to `http://localhost*8983`


**Troubleshoot:** HTTPS should be disabled for the localhost running in the project, so the browser should accept http. 

If it prompts you for HTTPS when you run the app, do not click Yes or No, click the "X" to close that dialog box. Then, your browser should load the website properly.

If it does not load the site, and gives you an HTTPS error, you may need to go into the Project's Solution Explorer > right click Project > click Properties > click Web > change the project URL to http from https. Clean the project, and build and run again.


### Voila! Enjoy your searching!
Select if you want to search for movie Titles, Cast members, Genres, Ratings (/10), Release years, and more!

### DISCLAIMER: 
The movie data was gathered with the OMDb API. Movie data is not guarenteed to be accurate, and it has not been screened for inappropriate or offensive content. There are about 3,000 movie entries, since many of the several-thousand Movie records retreieved from the OMDb database were not official IMDb movie record Ids -- ie, Movie Ids are random, and some Id numbers do not pertain to an actual movie. This is also a personal project, and not officially from Blockbuster.
