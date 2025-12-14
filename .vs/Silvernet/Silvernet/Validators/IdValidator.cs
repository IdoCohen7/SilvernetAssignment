using System.Linq;

namespace Silvernet.Validators
{

    public static class IdValidator
    {
        public static bool IsValidIsraeliId(this long idNumberLong)
        {
            if (idNumberLong < 0 || idNumberLong > 999999999) return false;

            string idString = idNumberLong.ToString().PadLeft(9, '0');
            int sum = 0;

            for (int i = 0; i < 9; i++)
            {
                int digit = idString[i] - '0';
                int multipliedDigit = (i % 2 == 0) ? digit : digit * 2;
                sum += (multipliedDigit > 9) ? multipliedDigit - 9 : multipliedDigit;
            }

            return (sum % 10 == 0);
        }
    }
}
