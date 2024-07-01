using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Banking_Tutorial_tql3;

public class Account {

    private static int nextId { get; set; } = 1;

    public int AccountId { get; set; } = 0;
    public string Description { get; set; } = string.Empty;
    public decimal Balance { get; set; } = 0;

    public bool Deposit(decimal Amount) {
        if(Amount <= 0) {
            throw new NonPositiveAmountException();
        }
        Balance += Amount;
        return true;
    }

    public bool Withdraw(decimal Amount) {
        Exception? ex = null;
        Exception? ex1 = null;
        if(Amount <= 0) {
            ex = new NonPositiveAmountException();
        }
        if(Amount > Balance) {
            if(ex is null) {
                ex = new InsufficientFundsException { Amount = Amount, Balance = Balance };
            } else {
                ex1 = new InsufficientFundsException("Insufficient funds", ex) { 
                    Amount = Amount, Balance = Balance 
                };
            }
        }
        if(ex1 is not null) throw ex1;
        if(ex is not null) throw ex;

        Balance -= Amount;
        return true;
    }

    public bool Transfer(decimal Amount, Account ax) {
        var success = this.Withdraw(Amount);
        if(success == true) {
            ax.Deposit(Amount);
        }
        return true;
    }

    public Account(string description) {
        AccountId = nextId++;
        Description = description;
        Balance = 0;
    }
}
