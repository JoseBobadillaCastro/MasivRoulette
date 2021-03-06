using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasivRoulette.Models;
namespace MasivRoulette.Util
{
    public class BetValidation
    {
        public bool validateBet(Bet bet, double maximum)
        {
            bool result = false;
            if (isValidBetType(bet.type.name, bet.selection) && isValidBetAmount(bet.amount, maximum))
            {
                result = true;
            }
            return result;
        }
        public bool isValidBetAmount(double amount, double maximum)
        {
            bool isValid = true;
            if (amount > maximum)
            {
                isValid = false;
                throw new Exception("Maximum bet amount exceeded");
            }
            return isValid;
        }
        public bool isValidBetType(string type, string selection)
        {
            bool isValid = true;
            if (type == "number")
            {
                isValid = isValidBetNumberSelection(selection);
            }
            else if (type == "color")
            {
                isValid = isValidBetColorSelection(selection);
            }
            else
            {
                isValid = false;
                throw new Exception("Invalid bet type");
            }
            return isValid;
        }
        public bool isValidBetNumberSelection(string selection)
        {
            bool isValid = true;
            if (double.TryParse(selection, out _) == false)
            {
                isValid = false;
                throw new Exception("Invalid bet selection for number bet type");
            }
            return isValid;
        }
        public bool isValidBetColorSelection(string selection)
        {
            bool isValid = false;
            if (selection == "black" || selection == "red")
            {
                isValid = true;
            }
            else 
            {
                isValid = false;
                throw new Exception("Invalid bet selection for color bet type");
            }
            return isValid;
        }
    }
}
