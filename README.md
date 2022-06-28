# Left Field Labs: Unity Engineer Take Home

## Pumpkin Shooter

Currently using Unity 2020.3.35f1

Create a gameManager where it contains one Custom Update event which is called on every frame. This class is also a singelton class and it does not get destroy when the scene
is scene is changed. Hence the music is on the objects and wouldn't get interupted. It handles the swaping between scenes, and updating the UI, and loading all the data needed user and game.
An abstract class was created for the UI which is UIBase where every time a new scene is loaded this class is fetched and InitUI() is called to initialized the UI regardless 
of what the scene is game or main menu.
GameData handles all the data of the game based on the JSON file in the streaming assets folder.
UserData handles the players data(currently using player prefs)
GameSession the action where converted to UnityEvents and 2 more were added one for score one for time.
By using this technique we removed the updated function in the UI and is now called when the time changes and when the score changes.
Cannon ball was added a destroy timer.
Enemy was added an event that send the score.
Spawner was added to coroutine that spawns a pumpkin and is hooked on the death event of the pumpkin and the adding the score.