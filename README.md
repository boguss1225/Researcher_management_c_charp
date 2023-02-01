# Researcher_management_c_charp
comprehensive researcher management program with GUI. Implemented in C#. Users can see researchers' personal details and research records.
![picture](docs/main_view.png) 

## Package Structure
![picture](docs/1_PackageDiagram1.jpg) </br>

## Class Structure
![picture](docs/2_ModelClassDiagram.jpg) </br>

![picture](docs/3_ViewClassDiagram1.jpg) </br>

![picture](docs/4_ControllerClassDiagram1.jpg) </br>

![picture](docs/5_DatabaseClassDiagram1.jpg) </br>

## User Sequence Overview
![picture](docs/6_SequenceDiagram5.jpg) </br>

![picture](docs/6_SequenceDiagram6.jpg) </br>

## USE CASE
![picture](docs/filtered_view.jpg) </br>
User can filter the researchers by name and check their details including further details of their publication. </br>


## Need to configure database connecter
in [ERDAdapter.cs](database/ERDAdapter.cs) file [Line 20~23](database/ERDAdapter.cs#L20-23)
```
		//Connect to the database
        private const string db = "dbname";
        private const string user = "username";
        private const string pass = "passwd";
        private const string server = "server.ip.utas.edu.au";

```
