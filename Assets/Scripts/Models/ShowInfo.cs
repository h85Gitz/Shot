namespace Models
{
    public class ShowInfo
    {

        public readonly string Account;
        public readonly string Password;
        //private bool _isBoy;

        public ShowInfo(string account, string password/*, bool isBoy*/ )
        {
            Account = account;
            Password = password;
            //_isBoy = isBoy;
        }
    }
}

