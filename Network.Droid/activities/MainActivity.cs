using Android.App;
using Android.Content.PM;
using Android.Database;
using Android.Database.Sqlite;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Java.Lang;
using Network.Droid.data.db;

namespace Network.Droid.activities
{
    [Activity(Label = "Network", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private ListView userList;
        private TextView header;
        private SaveNetwork databaseHelper;
        private SQLiteDatabase db;
        private ICursor userCursor;
        private SimpleCursorAdapter userAdapter;
        private Button upload;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            upload = (Button)FindViewById(Resource.Id.upload);
            header = (TextView)FindViewById(Resource.Id.header);
            userList = (ListView)FindViewById(Resource.Id.list);
            databaseHelper = new SaveNetwork(getApplicationContext());
            SetContentView(Resource.Layout.activuty_upload);
        }
        
        protected override void OnResume()
        {
            base.OnResume();
            // открываем подключение
            db = databaseHelper.getReadableDatabase();

            //получаем данные из бд в виде курсора
            userCursor = db.RawQuery("select * from " + SaveNetwork.TABLE, null);
            // определяем, какие столбцы из курсора будут выводиться в ListView
            string[] headers = new string[]{SaveNetwork.COLUMN_NAME, SaveNetwork.COLUMN_NUMBER_HIDDEN,
                SaveNetwork.COLUMN_NUMBER_LEARNING, SaveNetwork.COLUMN_NUMBER_CYCLE, SaveNetwork.COLUMN_NUMBER_ERROR};
            // создаем адаптер, передаем в него курсор
            userAdapter = new SimpleCursorAdapter(this, Android.Resource.Layout.TwoLineListItem,
                userCursor, headers, new int[]{Android.Resource.Id.Text1, Android.Resource.Id.Text2}, 0);
            header.SetText(Resource.String.items_found + userCursor.getCount());
            userList.SetAdapter(userAdapter);
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            db.Close();
            userCursor.Close();
        }

    }
}