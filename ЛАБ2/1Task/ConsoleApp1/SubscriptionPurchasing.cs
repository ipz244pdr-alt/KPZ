using ConsoleApp1;

namespace ConsoleApp1
{

    public abstract class SubscriptionPurchasing
    {

        public abstract Subscription CreateSubscription();

        public void FinalizePurchase()
        {
            Subscription sub = CreateSubscription();
            Console.WriteLine($"--- Обробка запиту через {this.GetType().Name} ---");
            sub.PrintDetails();
        }
    }

    public class WebSite : SubscriptionPurchasing
    {
        public override Subscription CreateSubscription() => new DomesticSubscription();
    }

    public class MobileApp : SubscriptionPurchasing
    {
        public override Subscription CreateSubscription() => new EducationalSubscription();
    }

    public class ManagerCall : SubscriptionPurchasing
    {
        public override Subscription CreateSubscription() => new PremiumSubscription();
    }
}