using System;
using Android.App;
using Android.Content;
using Android.Gms.Common.Apis;
using Android.OS;
using Android.Util;
using BaseGameUtils;
using Microsoft.Xna.Framework;

namespace CocosSharpBaseGameUtils
{
    /// <summary>
    /// Use this instead of AndroidGameActivity for Games that use Google Play Services and CocosSharp 1.6.2
    /// </summary>
    [Activity(Label = "BaseGameActivity")]
    public abstract class CCBaseGameActivity : AndroidGameActivity
    {
        // The game helper object. This class is mainly a wrapper around this object.
        private static GameHelper _gameHelper;

        // We expose these constants here because we don't want users of this class
        // to have to know about GameHelper at all.
        public static int CLIENT_GAMES = GameHelper.CLIENT_GAMES;
        public static int CLIENT_APPSTATE = GameHelper.CLIENT_APPSTATE;
        public static int CLIENT_PLUS = GameHelper.CLIENT_PLUS;
        public static int CLIENT_ALL = GameHelper.CLIENT_ALL;

        // Requested clients. By default, that's just the games client.

        private static String TAG = "BaseGameActivity";
        protected bool _isDebugLog = false;
        protected int _requestedClients = CLIENT_GAMES;

        /** Constructs a BaseGameActivity with default client (GamesClient). */

        protected CCBaseGameActivity()
        {
        }

        /**
     * Constructs a BaseGameActivity with the requested clients.
     * @param requestedClients The requested clients (a combination of CLIENT_GAMES,
     *         CLIENT_PLUS and CLIENT_APPSTATE).
     */

        protected CCBaseGameActivity(int requestedClients)
        {
            SetRequestedClients(requestedClients);
        }

        /**
     * Sets the requested clients. The preferred way to set the requested clients is
     * via the constructor, but this method is available if for some reason your code
     * cannot do this in the constructor. This must be called before onCreate or getGameHelper()
     * in order to have any effect. If called after onCreate()/getGameHelper(), this method
     * is a no-op.
     *
     * @param requestedClients A combination of the flags CLIENT_GAMES, CLIENT_PLUS
     *         and CLIENT_APPSTATE, or CLIENT_ALL to request all available clients.
     */

        protected void SetRequestedClients(int requestedClients)
        {
            _requestedClients = requestedClients;
        }

        public GameHelper GetGameHelper()
        {
            try
            {

                if (_gameHelper == null)
                {
                    _gameHelper = new GameHelper(this, _requestedClients);
                    _gameHelper.enableDebugLog(_isDebugLog);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return _gameHelper;
        }

        protected override void OnCreate(Bundle b)
        {
            try
            {
                base.OnCreate(b);
                if (_gameHelper == null)
                {
                    GetGameHelper();
                }
                _gameHelper.setup(this as IGameHelperListener);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected override void OnStart()
        {
            try
            {
                base.OnStart();
                _gameHelper.onStart(this);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected override void OnStop()
        {
            base.OnStop();
            _gameHelper.onStop();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            _gameHelper.onActivityResult(requestCode, (int)resultCode, data);
        }

        protected GoogleApiClient ApiClient
        {
            get { return _gameHelper.getApiClient(); }
        }

        protected bool IsSignedIn
        {
            get { return _gameHelper.isSignedIn(); }
        }

        protected void BeginUserInitiatedSignIn()
        {
            _gameHelper.beginUserInitiatedSignIn();
        }

        protected void SignOut()
        {
            _gameHelper.signOut();
        }

        protected void ShowAlert(String message)
        {
            _gameHelper.makeSimpleDialog(message).Show();
        }

        protected void ShowAlert(String title, String message)
        {
            _gameHelper.makeSimpleDialog(title, message).Show();
        }

        protected void EnableDebugLog(bool enabled)
        {
            _isDebugLog = true;
            if (_gameHelper != null)
            {
                _gameHelper.enableDebugLog(enabled);
            }
        }

        [Obsolete]
        protected void EnableDebugLog(bool enabled, String tag)
        {
            Log.Warn(TAG, "CCBaseGameActivity.enabledDebugLog(bool,String) is " +
                          "deprecated. Use enableDebugLog(bool)");
            EnableDebugLog(enabled);
        }

        protected String InvitationId
        {
            get { return _gameHelper.getInvitationId(); }
        }

        protected void ReconnectClient()
        {
            _gameHelper.reconnectClient();
        }

        protected bool HasSignInError
        {
            get { return _gameHelper.hasSignInError(); }
        }

        protected SignInFailureReason SignInError
        {
            get { return _gameHelper.getSignInError(); }
        }
    }
}

