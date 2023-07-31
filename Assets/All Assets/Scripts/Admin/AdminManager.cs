
using System.Collections;

using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdminManager : MonoBehaviour
{
    public GameObject supplier_A_T;
    public GameObject supplier_A_Parent;

    public GameParameters AdminParameters;

    public static AdminManager Instance;
    public GameObject Exit;

    private void Awake()
    {
        Instance = this;
    }

    private IEnumerator Start()
    {
        StartCoroutine(ResultManager.Instance.GetResult());
//Upload();
 //yield return new WaitForSeconds(2);

        ServerManager.instance.FetchData((() => { Debug.Log("Download Success"); }),
            (() => { Debug.Log("Download Failed"); }));
        while (!ServerManager.instance.Received)
        {
            Debug.Log("Waiting for Server Data");
            yield return null;
        }


//#if UNITY_EDITOR || UNITY_WEBGL
       // var n = JsonUtility.FromJson<GameParameters>(GameManager.Instance.dummyData);
//#else
           var n = JsonUtility.FromJson<GameParameters>(ServerManager.instance.ReceivedData);
//#endif
        Debug.Log(ServerManager.instance.ReceivedData);
        AdminParameters = n;
        AdminCustomerRating.Instance.Populate();
        foreach (var supplier in AdminParameters.AvailableSupplierList)
        {
            var s = Instantiate(supplier_A_T, supplier_A_Parent.transform);
            s.GetComponent<AdminSupplier>().Add(supplier);
        }

        foreach (var customer in AdminParameters.AvailableCustomerList)
        {
            var s = Instantiate(customer_A_T, customer_A_Parent.transform);
            s.GetComponent<AdminCustomer>().Add(customer);
        }
    }

    public void AddNewSupplier()
    {
        var n = Instantiate(supplier_A_T, supplier_A_Parent.transform);
        n.GetComponent<AdminSupplier>().Default();
    }

    public void RemoveSupplier()
    {
    }

    public GameObject customer_A_T;
    public GameObject customer_A_Parent;

    public void AddNewCustomer()
    {
        var n = Instantiate(customer_A_T, customer_A_Parent.transform);
        n.GetComponent<AdminCustomer>().Default();
    }

    private bool uploaded;

    public void Upload()
    {
        var s = JsonUtility.ToJson(AdminParameters);
        Debug.Log("UPLOAD DATA :" + s);
        ServerManager.instance.SaveData(s, (() =>
        {
            Debug.Log("Upload Success");
           
        }), (() => { Debug.Log("Upload Failed"); }));
    }



    public void mainScene()
    {
        var s = JsonUtility.ToJson(AdminParameters);
        Debug.Log("UPLOAD DATA :" + s);
        ServerManager.instance.SaveData(s, (() =>
        {
            Debug.Log("Upload Success");
            SceneManager.LoadScene(0);
         
        }), (() => { Debug.Log("Upload Failed"); }));
        
    }
    public void mainSceneWithoutSave()
    {
        SceneManager.LoadScene(0);
    }
}