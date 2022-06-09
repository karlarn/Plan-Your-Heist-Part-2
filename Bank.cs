using System;

namespace HeistPartTwo
{
   public class Bank
    {
        public int CashOnHand {get; set;}

        public int AlarmScore {get; set;}

        public int VaultScore {get; set;}

        public int SecurityGuardScore {get; set; }

        public bool IsSecure {
            get
            {
               if(SecurityGuardScore<=0 && VaultScore<=0 && AlarmScore<=0)
               {
                   return false;
               }
               else
               {
                   return true;
               }
            }
        }
        
    }
}