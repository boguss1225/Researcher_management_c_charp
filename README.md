# Researcher_management_c_charp
comprehensive researcher management program with GUI. Implemented in C#. Users can see researchers' personal details and research records.

## Package Structure
![picture](https://github.com/boguss1225/Researcher_management_c_charp/blob/main/docs/1_PackageDiagram1.jpg) 

## Class Structure
![picture](https://github.com/boguss1225/Researcher_management_c_charp/blob/main/docs/2_ModelClassDiagram.jpg) 

![picture](https://github.com/boguss1225/Researcher_management_c_charp/blob/main/docs/3_ViewClassDiagram1.jpg) 

![picture](https://github.com/boguss1225/Researcher_management_c_charp/blob/main/docs/4_ControllerClassDiagram1.jpg) 

![picture](https://github.com/boguss1225/Researcher_management_c_charp/blob/main/docs/5_DatabaseClassDiagram1.jpg) 

## User Sequence Overview
![picture](https://github.com/boguss1225/Researcher_management_c_charp/blob/main/docs/6_SequenceDiagram5.jpg) 

![picture](https://github.com/boguss1225/Researcher_management_c_charp/blob/main/docs/6_SequenceDiagram6.jpg) 


## Need to configure database connecter
in [ERDAdapter.cs](https://github.com/boguss1225/Researcher_management_c_charp/blob/main/database/ERDAdapter.cs) file [Line 20~23](https://github.com/boguss1225/Researcher_management_c_charp/blob/main/database/ERDAdapter.cs#L20-23)
```
		//Connect to the database
        private const string db = "dbname";
        private const string user = "username";
        private const string pass = "passwd";
        private const string server = "server.ip.utas.edu.au";

```
