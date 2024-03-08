using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;
using Firebase.Extensions;
using System;

public class FirestoreManager : MonoBehaviour
{
    [SerializeField] InputField Review;
    //[SerializeField] Button submit;

    FirebaseFirestore db;
    Dictionary<string, object> user;

    // Start is called before the first frame update
    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        //submit.onClick.AddListener(OnHandleClick);

    }

    public void saveData()
    {
        user = new Dictionary<string, object>
        {
            {"Review", Review.text },
        };
        db.Collection("Users").Document(Review.text).SetAsync(user).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("SUCCESS");
            }
            else
            {
                Debug.Log("No good");
            }
        });
    }
    //void OnHandleClick()
    //{
    //    int oldtext = int.Parse(Review.text);

    //}
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using Firebase.Firestore;
//using Firebase.Extensions;
//using System;

//public class FirestoreManager : MonoBehaviour
//{
//    [SerializeField] InputField Review;
//    //[SerializeField] Button submit;

//    FirebaseFirestore db;
//    Dictionary<string, object> user;

//    // Start is called before the first frame update
//    void Start()
//    {
//        db = FirebaseFirestore.DefaultInstance;
//        //submit.onClick.AddListener(OnHandleClick);
//    }

//    public void saveData()
//    {
//        user = new Dictionary<string, object>
//        {
//            {"Review", Review.text },
//        };

//        db.Collection("Users").Document(Review.text).SetAsync(user).ContinueWith(task =>
//        {
//            if (task.IsCompleted)
//            {
//                // Dispatch the success log on the main thread
//                UnityMainThreadDispatcher.Instance().Enqueue(() =>
//                {
//                    Debug.Log("SUCCESS");
//                });
//            }
//            else
//            {
//                // Dispatch the error log on the main thread
//                UnityMainThreadDispatcher.Instance().Enqueue(() =>
//                {
//                    Debug.Log("No good");
//                });
//            }
//        });
//    }
//}
