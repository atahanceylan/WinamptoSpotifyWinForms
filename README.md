# winamptospotifyforms
Winamp Archieve to Spotify
The aim of this side project is collecting mp3 filenames from harddisk and create Spotify Playlist based on selected folder. To use this you need a Spotify Developer Account. To use the Web API, start by creating a Spotify user account (Premium or Free). To do that, simply sign up at https://developer.spotify.com/dashboard After creating a spotify developer account you should register an application through Dashboard.
![Creating_Spotify_App](/Resources/creating_app_spotify.gif) 

After these 3 step your application should be created successfully.
![Dashboard](/Resources/after_creating_app_on_dashboard.png)

After creating you will have ClientID and Client Secret values. After creating app from Edit Settings tab you should set Redirection URLs.
![Redirect URls](/Resources/setting_redirect_urls.png)

By using https://developer.spotify.com/console/get-current-user/ link you can get your UserID of Spotify User ID.
![GettingUserId](/Resources/getting_user_id.png)

ClientID, SecretID and UserID should be placed in App.config
![ConfigSettings](/Resources/app.config.png)

After setting these config values application is ready to run. You can reach codes from https://github.com/atahanceylan/winamptospotifyforms
You can open solution file(.sln) with Visual Studio or Rider and run. 
![Run Project](/Resources/launch_project.png)

Here is GUI of WinampToSpotify windows form app:
![GUI](/Resources/gui.png)

First step is getting Access Token. After getting access token select folder to process.To simulate Oauth process with callback I used WebView2 control can be used to handle OAuth 2.0 authorization, using Authorization code flow with PKCE. I learned this information from https://melmanm.github.io/misc/2023/02/13/article6-oauth20-authorization-in-desktop-applicaions.html

![Access Token](/Resources/accestoken.png)

Example folder I selected Black Eyed Peas
![Default folder view](/Resources/folder_view.png)

![Example folder](/Resources/selecting_example_folder.png)

![Output1](/Resources/output_of_forms_app.png)

![Output2](/Resources/spotify_output.png)
