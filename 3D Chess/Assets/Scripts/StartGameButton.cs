using Realms;
using Realms.Sync;
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] private GameObject loadingIndicator = default;
    [SerializeField] private InputField gameIdInputField = default;

    public async void OnStartButtonClicked()
    {
        loadingIndicator.SetActive(true);

        var gameId = gameIdInputField.text;
        PlayerPrefs.SetString(Constants.PlayerPrefsKeys.GameId, gameId);

        await CreateRealmAsync(gameId);

        SceneManager.LoadScene(Constants.SceneNames.Main);
    }

    private async Task CreateRealmAsync(string gameId)
    {
        var app = App.Create(Constants.Realm.AppId);
        var user = app.CurrentUser;

        if (user == null)
        {
            // This example focuses on an introduction to Sync.
            // We will keep the registration simple for now by just creating a random email and password.
            // We'll also not create a seperate registration dialog here and instead just try to register
            // and if already registered, log in.
            // In a different example we will focus on authentication methods, login / registration dialogs, etc.
            var email = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            await app.EmailPasswordAuth.RegisterUserAsync(email, password);
            user = await app.LogInAsync(Credentials.EmailPassword(email, password));
        }

        RealmConfiguration.DefaultConfiguration = new SyncConfiguration(gameId, user);

        if (!File.Exists(RealmConfiguration.DefaultConfiguration.DatabasePath))
        {
            // If this is the first time we start the game, we need to create a new Realm and sync it.
            // This is done by `GetInstanceAsync`. There is nothing further we need to do here.
            // The Realm is then used by `GameState` in it's `Awake` method.
            using var realm = await Realm.GetInstanceAsync();
        }
    }
}
