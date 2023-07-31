[System.Serializable]
public class Transaction
    {
        public string Date;
        public string MadeBy;
        
        public enum TransactionType
        {
            Credit,
            Debit
        }

        public TransactionType Type;
        public int TransactionAmount;
        public int ClosingBalance;
        public int quantity;
    }
