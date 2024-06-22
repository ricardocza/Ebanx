# REQUIREMENTS
Go to this [Prerequisites Page](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio-code)
or search for "Tutorial: Create a web API with ASP.NET Core" and go to the "Prerequisites" section.\
There will be displayed the requirements to run this project using Visual Studio for Windows, Mac or using VS Code

### How to execute test
Download or clone this repository using the preferred method.
Select the solution file to open the project (Visual Studio) or access the directory using a terminal.

### Visual Studio
In the main branch, Build and start the project, using http, https or IIS Express using the IDE play button

### VS Code
Access the folder that you downloaded or cloned the project, make sure you are in the main branch and run the following commands

```
dotnet restore
dotnet run
```

The API should start, and the terminal should display a message like "Now listening on: http://localhost:5264"

### Ngrok
Access [Ngrok](https://ngrok.com/), Sign up if you don't have an account\
In the main dashboard will have a guide to download the Ngrok agent, select the OS that you are using and download the agent.\
Make sure to follow the guide until ```ngrok config add-authtoken [AuthenticationKey]```.\
After downloading it, open the agent using a terminal and run the following command:
```
ngrok http https://localhost:[port]/ --host-header="localhost:[port]"
```
Change the port according to the port that the API started, and note that if you started the api using http, the command changes the https to http
```
ngrok http http://localhost:[port]/ --host-header="localhost:[port]"
```
The terminal should now be tunneling the api and have a address like this
```
Forwarding                    https://d506-177-220-174-86.ngrok-free.app -> https://localhost:5264/
```
Select the link ```https://d506-177-220-174-86.ngrok-free.app``` and place it into the URL field available in the [Ipkiss page](https://ipkiss.pragmazero.com/)
