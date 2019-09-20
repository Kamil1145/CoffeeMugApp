namespace CoffeeMugApp.Helper
{
    public class Utils
    {
        public static bool ValidatePrize(decimal prize)
        {
            if (prize >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ValidateName(string name)
        {
            if (name.Length<100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
