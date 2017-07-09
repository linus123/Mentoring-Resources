using System;
using System.Data;
using System.Data.SqlClient;

namespace CodeExamples.PatternsOfEnterpriseApplicationArchitecture.DomainLogicPatterns
{
    public class OrderReportTransactionScript
    {
        public void PrintOrdersWithTotals()
        {
            var ordersDataSet = GetAllOrders();

            PrintReportToConsole(ordersDataSet);
        }

        private DataSet GetAllOrders()
        {
            var connectionString = "[My Connection String]";

            var allOrdersSql = @"
SELECT
        *
    FROM
        Order

SELECT
        *
    FROM
        LineItem";

            var ordersDataSet = new DataSet();

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(allOrdersSql, connection);

                var sqlDataAdapter = new SqlDataAdapter(command);

                connection.Open();
                sqlDataAdapter.Fill(ordersDataSet);
                connection.Close();
            }

            return ordersDataSet;
        }

        private void PrintReportToConsole(DataSet ordersDataSet)
        {
            Console.WriteLine("Date\tName\tTotal");

            foreach (DataRow order in ordersDataSet.Tables[0].Rows)
            {
                var orderTotal = CalculateOrderTotal(order, ordersDataSet.Tables[1]);
                var orderDate = Convert.ToDateTime(order["OrderDate"]).ToShortDateString();
                var customerName = order["CustomerName"].ToString();

                Console.WriteLine(
                    "'{0}'\t'{1}'\t{2}",
                    orderDate,
                    customerName,
                    orderTotal);
            }
        }

        private decimal CalculateOrderTotal(DataRow order, DataTable lineItems)
        {
            var orderTotal = 0m;

            var lineItemsForOrder = lineItems.Select("OrderId = " + order["OrderId"]);

            foreach (DataRow lineItem in lineItemsForOrder)
            {
                var unitPrice = Convert.ToDecimal(lineItem["UnitPrice"]);
                var quanity = Convert.ToInt32(lineItem["Quantity"]);

                orderTotal += unitPrice * quanity;
            }

            return orderTotal;
        }
    }
}