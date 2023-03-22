# ASP.NET (C#) GeneralCulture

## A small simple CRUD app with authentication where users can add, delete, modify or update general knowledge questions and their answers.


After setting up the controller and generating the views. Run with or without debugging, should see the page. Click on the Home and Privacy Links we'll see the URLs. Now if we remove the last /Home/Privacy from the URL and enter Questions, we should see the questions stored in the Db. There will be a problem because we don't have a Db yet. We will create it.

This is where the Model side comes into play. We'll use Migrations to create our tables.

Open the "Migrations" folder and select the first file from the top. We'll see a few tables but we don't see anything related to our class Question in our file.


Open the "Package Manager Console" (if we don't see it type it in the search bar above). We are going to use it for some Db management.

Initial setup of our database:

```
add-migration "initialsetup"
``` 

If you look at the "Migrations" folder you'll see a new file named initialsetup which contains a createTable with name "Question" and the columns Id, Quest and Answer
(located in /Models/Question.cs).

We are starting an ORM (Object Relation Mapper). Mapping between a class and a table. The Class has attributes and the ORM will create a table with those attributes (attributes are the columns of the table).

2 options for db creation, ORM like we explained or a DAO, more like MS Access. DAO (more old school, easier to find issues,). ORM (computer generation of tables, saves time, db updated using migrations, better of simple apps). Let's go with the ORM option that'll create our tables. Still in the "Package Manager"

```
update-database 
```

Once you get the done message. Go to the SQL Server Obj.Explorer at the left of our screen (If not there go on the "View" tab or search bar). Follow the path:
SQL Server/localdb/Databases/aspnet-[SolutionName]/Tables/dboQuestion

If you got a type wrong (Int instead of string in the models file) you can "drop-database" and then remove-migration, fix the type, add the migration, verify if you want to allow non-nulls values and update the database.

Push the green arrow to run, and go to /Jokes in the URL and this time you'll see 2 columns with the questions and answers (empty to start). You can add, edit, see the lists, update and delete the question (created with the views we put up earlier).

We could say our app is done but we could also some more features such as:

- Show who wrote the question
- Complete the register and log in and link them
- Hide the answer
- Hide the "Create question" feature to non-logged users


First things first we want to add a way to reach our questions from the NavBar.

Go to /Shared/Layout.cshtml :
Copy on of the list elements <li> tags (either Home or Privacy) and paste it.
Change the text to Questions ans the asp-controller prop to Questions since we created a controller for Questions. Change the action prop to Index which is also defined in the Controler file. You can now access our questions from the navbar


### SEARCH OPTION:

This feature allows us to find a specific question if the keywords typed in are in the questions. Copy another list element like mentionned just above and name it Search. The action however is different, name it "ShowSearchForm".

Just like the Index() method, we know need a ShowSearchForm() method in the controller. This time we'll get an error because we do not have a "ShowSearchForm" view. Now you could manually could create a "ShowSearchForm" folder in the views or be lazy and click on the ShowSearchForm in the controller and click "add View".

You can choose between the pre-programmed view or the blank one. Hoping that you are lazy like me, click on the pre-programmed one. Change the template to "Create"
since we are adding a data entry. 

Select the right model in my case ( Question (GeneralCultureWeb.Models) ) and click  on the Create as a partial view since we want it to be a part of the main template. Run the app and see if the Search option in the navBar sends you to a functional page with a form.

Since this is not how we want our form to look go back to the "ShowSearchForm" view. Leave only one form group.

1- Change the button value to "Search"
2- Remove the first line since we are not adding anything to our project but only searching and anything validation related + the script below
3- Rename the page between the h4 tags
4- Remove the asp-for prop of the input and the label in the form group and the label and add a name prop for the input( gave it the "SearchPhrase") and a for prop for
the label ("SearchPhrase").
5- Change form's action to a function we'll add (we don't want it to show itself) named ("ShowSearchResults")
6- Add the method to the controller. It is a POST method so we need a param which is SearchPhrase.
7- The View() method will have this line of code

```
View("Index",await _context.Question.Where( j => j.Quest.Contains(SearchPhrase)).ToListAsync()) :
```
We are looking for the Index view (first param) and the data that goes with it.
We also filter all the results and only keep the ones that contain SearchPhrase entered.

And voilà !

### HIDE THE ANSWERS OPTION:

Go to Views/Questions/Index.cshtml and remove the Answer parts in the table and 
the @foreach parts

### LOGIN REQUIREMENT TO ADD A QUESTION:

Go to the Create function in the Questions controller and type the following code above the Create() and the [HttpPost] or whatever function you feel needs authorization.
```
[Authorize]
```
If you see an error add the following at the top

```
using Microsoft.AspNetCore.Authorization;
```

AND VOILÀ. We have completed what we were aiming for. Now of course the page is not pleasant to the eye but we can style it in the wwwroot/css folder.

