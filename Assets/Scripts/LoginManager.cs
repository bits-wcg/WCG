using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public TMP_Text Status;
    public float stepsDelay;
    public UnityEvent studentLogin;
    public UnityEvent staffLogin;

    public TMP_InputField emailField;
    public TMP_InputField passwordField;

    public Button loginButton;
    public string userRole;

    public string[] adminRoles;
    public string[] studentRoles;

    public Dictionary<string, string> response = new Dictionary<string, string>();

    private enum Roles
    {
        student,
        staff,
        unknown
    }

    private Roles role;


    private void Start()
    {
        string userName = PlayerPrefs.GetString("username");
        string password = PlayerPrefs.GetString("password");

        emailField.text = userName;
        passwordField.text = password;
        loginButton.onClick.AddListener(Login);
    }


    private void Login()
    {
        PlayerPrefs.SetString("username", emailField.text);
        PlayerPrefs.SetString("password", passwordField.text);
        PlayerDataManager.Instance._PlayerData.PlayerName = emailField.text;
        emailField.interactable = false;
        passwordField.interactable = false;
        loginButton.interactable = false;
        Debug.Log("Starting Inout Verification");
        Status.color = new Color(255, 242, 186, 255);
        StartCoroutine(PostCrt());
    }


    private IEnumerator PostCrt()
    {
        Status.text = "Connecting to server...";
        yield return new WaitForSeconds(stepsDelay);
        var form = new WWWForm();
        form.AddField("username", emailField.text);
        form.AddField("password", passwordField.text);

        using var www = UnityWebRequest.Post("https://elearn.bits-pilani.ac.in/user/vcredentials/", form);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Status.text = "Failed to connect to servers :" + www.error;
            }
            else
            {
                Status.text = "Invalid Email or Password";
            }

            Status.color = Color.red;
            yield return new WaitForSeconds(stepsDelay);
            Debug.Log(www.error);
            Debug.Log(www.result);
            loginButton.interactable = true;
            emailField.interactable = true;
            passwordField.interactable = true;
        }
        else
        {
            Status.text = "Connected to server";
            yield return new WaitForSeconds(stepsDelay);
            Status.text = "Verifying user role";
            yield return new WaitForSeconds(stepsDelay);

            response = www.GetResponseHeaders();

            Debug.Log(response);
            userRole = response["X-Role"];

            if (adminRoles.Any(roleX => userRole == roleX))
            {
                Debug.Log("This is a staff role assigning as Admin");
                role = Roles.staff;
            }
            else
            {
                Debug.Log("This is not Admin Account");
                if (studentRoles.Any(roleX => userRole == roleX))
                {
                    Debug.Log("This is a student role assigning as Student");
                    role = Roles.student;
                }
                else
                {
                    Debug.Log("This is also not a student role Setting as UnKnown");
                    role = Roles.unknown;
                }
            }

            switch (role)
            {
                case Roles.student:
                    Status.text = "Login Success, This is a student account";
                    Status.color = Color.green;
                    yield return new WaitForSeconds(2f);
                    Status.text = "Taking to WCG please wait...";
                    yield return new WaitForSeconds(2f);
                    studentLogin?.Invoke();
                    break;
                case Roles.staff:
                    Status.text = "This is a admin account";

                    yield return new WaitForSeconds(stepsDelay);
                    Status.text = "Welcome Professor...";

                    yield return new WaitForSeconds(stepsDelay);
                    staffLogin?.Invoke();
                    break;
                case Roles.unknown: //Temp fix
                    Status.text = "This is an unkown account";
                    Status.color = Color.blue;
                    yield return new WaitForSeconds(stepsDelay);
                    Status.text = "Taking to WCG please wait...";

                    yield return new WaitForSeconds(stepsDelay);
                    studentLogin?.Invoke();
                    break;
                // Status.text = "Unknown User Role please contact Admin";
                // yield return new WaitForSeconds(stepsDelay);
                // break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            loginButton.interactable = true;
            emailField.interactable = true;
            passwordField.interactable = true;
        }
    }
}