using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voxel.Transactions.Constraints.Service.Contract;

namespace testsConstraints
{
    class Program
    {
        static void Main(string[] args)
        {

            var validateTransactionsClient = new Voxel.Transactions.Constraints.ValidateTransactionConstraintsClient(
                // IMPORTANT Only set the uri for local tests. Comment the line if other enviroment
                new Uri("http://proddmz-bavel-typhoon-typh-transactionsconstraints.lbservice")
            );

            var filename = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "UT.xml");

            var utContent = System.IO.File.ReadAllBytes(filename);

            var checkTransactionRequest = new CheckTransactionRequest()
            {
                XmlBytes = utContent,
                AllowArchive = false,
                UiCulture = ""
            };
                var transactionResponse = validateTransactionsClient.CheckTransaction(checkTransactionRequest);
                var transactionsFailed = transactionResponse.ConstraintsPassed.Where(x => x.Passed != true).ToList();
                var transactionsSuccess = transactionResponse.ConstraintsPassed.Where(x => x.Passed == true).ToList();

        }
    }
}
