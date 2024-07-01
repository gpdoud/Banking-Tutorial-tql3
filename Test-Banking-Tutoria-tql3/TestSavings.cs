using Banking_Tutorial_tql3;

namespace Test_Banking_Tutoria_tql3;

public class TestSavings {

    Savings savings = default!;

    public TestSavings() {
        savings = new Savings("TestSavingsAccount");
    }

    [Fact]
    public void TestInterestRateAt12Pct() {
        Assert.Equal(0.12m, savings.IntRate, 2);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    [InlineData(500)]
    [InlineData(10000)]
    public void TestSimpleDeposit(int amount) {
        savings.Deposit(amount);
        Assert.True(savings.Balance == amount);
    }
    [Theory]
    [InlineData(1)]
    [InlineData(100)]
    [InlineData(500)]
    [InlineData(10000)]
    public void TestExistingDeposit(int amount) {
        savings.Deposit(500);
        savings.Deposit(amount);
        Assert.True(savings.Balance == amount + 500);
    }
    [Theory]
    [InlineData(1000, 1)]
    [InlineData(1000, 100)]
    [InlineData(1000, 500)]
    [InlineData(1000, 1000)]
    //[InlineData(1000, 1001)]
    public void TestWithdraw(int deposit, int amount) {
        savings.Deposit(deposit);
        savings.Withdraw(amount);
        Assert.True(savings.Balance == deposit - amount);
    }
    [Fact]
    public void TestDepositNegativeAmount() {
        Action ex = () => savings.Deposit(-100);
        Assert.Throws<NonPositiveAmountException>(ex);
    }
    [Fact]
    public void TestWithdrawNegativeAmount() {
        Action ex = () => savings.Withdraw(-100);
        Assert.Throws<NonPositiveAmountException>(ex);
    }
    [Fact]
    public void TestWithdrawInsifficentFunds() {
        savings.Deposit(1000);
        Action ex = () => savings.Withdraw(1001);
        Assert.Throws<InsufficientFundsException>(ex);
    }
    [Theory]
    [InlineData(1000, 200, 300, 700, 500)]
    public void TestTransfer(int deposit1, int deposit2, int amount, int savBalance, int sav2Balance) {
        var savings2 = new Savings( "Second Savings Account");
        savings.Deposit(deposit1);
        savings2.Deposit(deposit2);
        savings.Transfer(amount, savings2);
        Assert.True(savings.Balance == savBalance &&  savings2.Balance == sav2Balance);
    }
}