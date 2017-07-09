using System;
using System.Data;
using System.Data.SqlClient;

namespace CodeExamples.PatternsOfEnterpriseApplicationArchitecture.DomainLogicPatterns
{
    public class OrderReportTableModule
    {
        public class OrderTableModule
        {
            private readonly DataSet _dataSet;

            public OrderTableModule(DataSet dataSet)
            {
                _dataSet = dataSet;
            }

            public DataRowCollection GetAll()
            {
                return _dataSet.Tables["Order"].Rows;
            }

            private DataTable LineItems
            {
                get { return _dataSet.Tables["LineItem"]; }
            }

            public decimal CalculateOrderTotal(
                Guid orderId)
            {
                var orderTotal = 0m;

                var lineItemsForOrder = LineItems.Select("OrderId = " + orderId);

                foreach (DataRow lineItem in lineItemsForOrder)
                {
                    var unitPrice = Convert.ToDecimal(lineItem["UnitPrice"]);
                    var quanity = Convert.ToInt32(lineItem["Quantity"]);

                    orderTotal += unitPrice * quanity;
                }

                return orderTotal;
            }

        }

        public void PrintOrdersWithTotals()
        {
            var ordersDataSet = GetOrderTableModule();

            PrintReportToConsole(ordersDataSet);
        }

        private OrderTableModule GetOrderTableModule()
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

            return new OrderTableModule(ordersDataSet);
        }

        private void PrintReportToConsole(OrderTableModule orderTableModule)
        {
            Console.WriteLine("Date\tName\tTotal");

            var orders = orderTableModule.GetAll();

            foreach (DataRow orderDataRow in orders)
            {
                var orderTotal = orderTableModule.CalculateOrderTotal((Guid)orderDataRow["OrderId"]);
                var orderDate = Convert.ToDateTime(orderDataRow["OrderDate"]).ToShortDateString();
                var customerName = orderDataRow["CustomerName"].ToString();

                Console.WriteLine(
                    "'{0}'\t'{1}'\t{2}",
                    orderDate,
                    customerName,
                    orderTotal);
            }
        }
    }
}