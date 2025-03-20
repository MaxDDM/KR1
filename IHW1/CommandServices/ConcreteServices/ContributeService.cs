﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHW1.CommandServices.ConcreteServices
{
    internal class ContributeService : Service
    {
        public override void Execute(ref BankAccount account, int sum = -1, string description = "", string category = "", 
            DateTime begin = new DateTime(), DateTime end = new DateTime())
        {
            account.Contribute(sum, description, category);
        }
    }
}
