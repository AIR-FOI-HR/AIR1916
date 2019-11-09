using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;

namespace FOIKnjiznica.Firebase
{
    public class FirebaseHelper
    {
        FirebaseClient database = new FirebaseClient("https://foiknjiznica.firebaseio.com/");
    }
}
