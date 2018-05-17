using NetCoreTester.Win.Bll;
using NetCoreTester.Win.Framework;
using System;
using Unity;
 


namespace NetCoreTester.Win
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Unity.UnityContainer();
            container.RegisterSingleton<IStringHelper, StringHelper>();
                             
            var newUsers = container.Resolve<Users>();
            Console.WriteLine(newUsers.FormatUserName("cnf"));

            // Unity.Injection.

            Console.ReadKey();
        }
    }
}
