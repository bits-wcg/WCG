using System.Collections.Generic;

[System.Serializable]
public class TransactionStatement
{
    public Quarterly_1 Quarterly1=new Quarterly_1();
    public Quarterly_2 Quarterly2=new Quarterly_2();
    public Quarterly_3 Quarterly3=new Quarterly_3();
    public Quarterly_4 Quarterly4=new Quarterly_4();
    [System.Serializable]
    public class Quarterly_1
    {
        public List<Transaction> January=new List<Transaction>();
        public List<Transaction> Feb=new List<Transaction>();
        public List<Transaction> March=new List<Transaction>();
    }
    [System.Serializable]
    public class Quarterly_2
    {
        public List<Transaction> April=new List<Transaction>();
        public List<Transaction> May=new List<Transaction>();
        public List<Transaction> June=new List<Transaction>();
    }
    [System.Serializable]
    public class Quarterly_3
    {
        public List<Transaction> July=new List<Transaction>();
        public List<Transaction> August=new List<Transaction>();
        public List<Transaction> Sep=new List<Transaction>();
    }
    [System.Serializable]
    public class Quarterly_4
    {
        public List<Transaction> October=new List<Transaction>();
        public List<Transaction> November=new List<Transaction>();
        public List<Transaction> December=new List<Transaction>();
    }
}