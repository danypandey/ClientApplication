using System;
using System.Threading.Tasks;
using System.Net.Http;
using UserCommon;
using MongoDB.Bson;

namespace ClientApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            SampleClient result = new SampleClient();


            /*
             *GET Request
             */
            /*Task<Data> detail = result.GetUserData("10");*/


            /*
             *GetAll Request
             */
            Task<Data[]> details = result.GetAllUsers();


            /*
             * POST Request
             */
            /*Data details = new Data("", "Din ka dayal", "24");
            Task<Result> results = result.CreateUser(details);
            Console.WriteLine(results);*/




            /*
             * PUT(Update) Request
             */
            /*Data putdetails = new Data("12", "Mishra", "33");
            Task<Result> putdetail = result.UpdateUser(putdetails);*/




            /*
             * Delete Request
             */
            /*Task<Result> delete = result.DeleteUser("13");*/

            Console.ReadKey();
        }
    }
}
