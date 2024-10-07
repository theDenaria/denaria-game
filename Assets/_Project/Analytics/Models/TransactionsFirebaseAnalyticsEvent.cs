//using _Project.Applovin.Scripts.Models;
//using _Project.CBSUtility.Models;
using _Project.LoggingAndDebugging;
using _Project.SceneManagementUtilities.Models;
using _Project.Utilities;

namespace _Project.Analytics.Models
{
    public class TransactionsFirebaseAnalyticsEvent : FirebaseAnalyticsEvent
    {
        //[Inject] public ICurrencyModel CurrencyModel { get; set; }
        [Inject] public ICurrentSceneModel CurrentSceneModel { get; set; }

        public TransactionsFirebaseAnalyticsEvent()
        {
        }
        
        public TransactionsFirebaseAnalyticsEvent SetParametersAndReturn(string transaction_type, string transaction_link, long amount, string result, float transaction_duration_time)
        {
            EventName = "transactions";
            
            //long amount_before_transaction = (long)CurrencyModel.CurrencyAmount;//TODO: UNCOMMENT
            long amount_before_transaction = 0;

            if (amount < 0)
            {
                amount_before_transaction -= amount; //To deal with branch prediction on currency spendings (in other places, we pre-reduce the gem in model to prevent extra purchases)
            }
            
            string transaction_source = CurrentSceneModel.CurrentSceneId;

            //int transaction_duration_time = (int)(DateUtility.GetCurrentEpochSeconds() - TransactionTimeModel.ClickEpochTime);
            
            EventParameters.Add(nameof(transaction_type), new FirebaseAnalyticsEventParameter(transaction_type));
            EventParameters.Add(nameof(transaction_source), new FirebaseAnalyticsEventParameter(transaction_source));
            EventParameters.Add(nameof(transaction_link), new FirebaseAnalyticsEventParameter(transaction_link));
            EventParameters.Add(nameof(amount_before_transaction), new FirebaseAnalyticsEventParameter(amount_before_transaction));
            EventParameters.Add(nameof(amount), new FirebaseAnalyticsEventParameter(amount));
            EventParameters.Add(nameof(result), new FirebaseAnalyticsEventParameter(result));
            EventParameters.Add(nameof(transaction_duration_time), new FirebaseAnalyticsEventParameter(transaction_duration_time));

            LogTransactionDurationTime(transaction_type, transaction_link, transaction_duration_time);

            return this;
        }
        
        public TransactionsFirebaseAnalyticsEvent SetParametersAndReturnAdRelated(string transaction_type, string transaction_link, string result, float transaction_duration_time)
        {
            EventName = "transactions";
            
            //long amount_before_transaction = (long)CurrencyModel.CurrencyAmount;//TODO: UNCOMMENT
            long amount_before_transaction = 0;
            string transaction_source = CurrentSceneModel.CurrentSceneId;

            EventParameters.Add(nameof(transaction_type), new FirebaseAnalyticsEventParameter(transaction_type));
            EventParameters.Add(nameof(transaction_source), new FirebaseAnalyticsEventParameter(transaction_source));
            EventParameters.Add(nameof(transaction_link), new FirebaseAnalyticsEventParameter(transaction_link));
            EventParameters.Add(nameof(amount_before_transaction), new FirebaseAnalyticsEventParameter(amount_before_transaction));
            EventParameters.Add(nameof(result), new FirebaseAnalyticsEventParameter(result));
             EventParameters.Add(nameof(transaction_duration_time), new FirebaseAnalyticsEventParameter(transaction_duration_time));

            LogTransactionDurationTime(transaction_type, transaction_link, transaction_duration_time);
            
            return this;
        }

        private void LogTransactionDurationTime(string transactionType, string transactionLink, float transactionDurationTime)
        {
            UnityEngine.Debug.LogFormat("TransactionAnalyticsEvent\nType: {0}\nLink: {1}\nDuration: {2}",
                transactionType,
                transactionLink,
                transactionDurationTime.ToString("F3")
            );
        }

    }
}