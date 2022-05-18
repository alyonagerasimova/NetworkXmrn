
using System;
using Android.Content;
using Android.OS;
using Android.Text;
using Android.Widget;
using Java.IO;
using Java.Lang;
using Java.Net;
using Java.Util;
using Network.Droid.activities;
using Org.Json;
using Button = Xamarin.Forms.Button;
using Exception = Java.Lang.Exception;
using String = Java.Lang.String;

namespace Network.Droid.activities
{
    public class LoginRegistrationActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    { 
        Button btnLogin;
    Button btnRegister;
    FirebaseAuth auth;
    FirebaseDatabase db;
    DatabaseReference users;
    EditText email;
    EditText password;
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.activity__login_register);

        btnLogin = FindViewById(Resource.Id.btn_login);
        btnRegister = FindViewById(Resource.Id.btn_register);

        email = findViewById(R.id.email);
        password = findViewById(R.id.password);

        auth = FirebaseAuth.getInstance();
        db = FirebaseDatabase.getInstance();
        users = db.getReference("Users");

        btnLogin.setOnClickListener(view -> {
            try {
                login(email, password);
            } catch (FirebaseAuthException e) {
                e.printStackTrace();
            }
        });

        btnRegister.setOnClickListener(view -> registration(email, password));
    }

    private void login(EditText email, EditText password) throws FirebaseAuthException {

        if (TextUtils.isEmpty(email.getText().toString())) {
            Toast.makeText(LoginRegistrationActivity.this, R.string.enter_email,
                    Toast.LENGTH_LONG).show();
            return;
        }
        if (TextUtils.isEmpty(password.getText().toString())) {
            Toast.makeText(LoginRegistrationActivity.this, R.string.enter_password,
                    Toast.LENGTH_LONG).show();
            return;
        }

        auth.signInWithEmailAndPassword(email.getText().toString(), password.getText().toString())
                .addOnSuccessListener(authResult -> {
                    FirebaseUser mUser = FirebaseAuth.getInstance().getCurrentUser();
                    if (mUser != null) {
                        mUser.getIdToken(false)
                                .addOnCompleteListener(task -> {
                                    if (task.isSuccessful()) {
                                        String idToken = task.getResult().getToken();
                                        new Thread(() -> {
                                            String str = getData(idToken);
                                            try {
                                                JSONObject object = new JSONObject(str);
                                                String welcomeText = object.getString("welcome");
                                                runOnUiThread(() -> {
                                                    Toast.makeText(
                                                            getApplicationContext(),
                                                            welcomeText,
                                                            Toast.LENGTH_LONG).show();
//                                                    Intent intent = new Intent(LoginRegistrationActivity.this, NetworkActivity.class);
//                                                    startActivity(intent);
                                                });
                                            } catch (JSONException e) {
                                                e.printStackTrace();
                                            }
                                        }).start();
                                        Intent intent = new Intent(LoginRegistrationActivity.this, NetworkActivity.class);
                                        startActivity(intent);
                                    } else {
                                        Objects.requireNonNull(task.getException()).printStackTrace();
                                    }
                                });
                    }
                }).addOnFailureListener(e ->
                Toast.makeText(LoginRegistrationActivity.this, getString(R.string.error_signin) + e.getMessage(), Toast.LENGTH_LONG).show());
    }

    private void registration(EditText email, EditText password) {

        if (TextUtils.isEmpty(email.getText().toString())) {
            Toast.makeText(LoginRegistrationActivity.this, R.string.enter_email,
                    Toast.LENGTH_LONG).show();
            return;
        }
        if (TextUtils.isEmpty(password.getText().toString())) {
            Toast.makeText(LoginRegistrationActivity.this, R.string.enter_password,
                    Toast.LENGTH_LONG).show();
            return;
        }

        auth.createUserWithEmailAndPassword(email.getText().toString(),
                password.getText().toString())
                .addOnSuccessListener(authResult -> {
                    User user = new User(email.getText().toString(),
                            password.getText().toString());
                    users.child(Objects.requireNonNull(FirebaseAuth.getInstance().getCurrentUser()).getUid())
                            .setValue(user)
                            .addOnSuccessListener(unused -> Toast.makeText(
                                    LoginRegistrationActivity.this,
                                    R.string.add_user,
                                    Toast.LENGTH_LONG).show());
                })
                .addOnFailureListener(e -> Toast.makeText(LoginRegistrationActivity.this,
                        getString(R.string.error_register) + e.getMessage(), Toast.LENGTH_LONG).show());
        ;
    }

    private String getData(String token) {
        String targetURL = "http://192.168.1.89:6070/api/user/welcome";

        try {
            URL url = new URL(targetURL);
            HttpURLConnection connection = (HttpURLConnection) url.openConnection();
            connection.setRequestMethod("GET");
            connection.setRequestProperty("User-Agent", "Mozilla/5.0");
            connection.setRequestProperty("X-UserId", FirebaseAuth.getInstance().getUid());
            connection.setRequestProperty("X-IdToken", token);

            int responseCode = connection.getResponseCode();
            if (responseCode == 404) {
                throw new IllegalArgumentException();
            }

            BufferedReader reader = new BufferedReader(new InputStreamReader(connection.getInputStream()));
            StringBuilder builder = new StringBuilder();
            String line = "";

            while ((line = reader.readLine()) != null) {
                builder.append(line).append("\n");
            }
            reader.close();
            return builder.toString();

        } catch (Exception e) {
            e.printStackTrace();
        }

        return "{}";
    }
    }
}