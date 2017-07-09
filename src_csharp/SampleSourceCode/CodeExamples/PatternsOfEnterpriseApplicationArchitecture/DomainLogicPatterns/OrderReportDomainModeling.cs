using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CodeExamples.PatternsOfEnterpriseApplicationArchitecture.DomainLogicPatterns
{
    public class OrderReportDomainModeling
    {
        public void PrintOrdersWithTotals()
        {
            var orderRepository = new OrderRepository();

            var orders = orderRepository.GetAll();

            PrintReportToConsole(orders);
        }

        private void PrintReportToConsole(Order[] orders)
        {
            Console.WriteLine("Date\tName\tTotal");

            foreach (var order in orders)
            {
                var orderDate = order.OrderDate;
                var customerName = order.CustomerName;
                var orderTotal = order.OrderTotal;

                Console.WriteLine(
                    "'{0}'\t'{1}'\t{2}",
                    orderDate,
                    customerName,
                    orderTotal);
            }
        }

        public class Order
        {
            private Guid _orderId;
            private DateTime _orderDate;
            private readonly string _customerName;
            private readonly LineItemValueObject[] _lineItemValueObjects;

            public Order(
                Guid orderId,
                DateTime orderDate,
                string customerName,
                LineItemValueObject[] lineItems)
            {
                _lineItemValueObjects = lineItems;
                _customerName = customerName;
                _orderDate = orderDate;
                _orderId = orderId;
            }

            public string OrderDate
            {
                get { return _orderDate.ToShortDateString(); }
            }

            public string CustomerName
            {
                get { return _customerName; }
            }

            public decimal OrderTotal
            {
                get { return _lineItemValueObjects.Sum(l => l.Cost); }
            }
        }

        public class LineItemValueObject
        {
            private int _productId;
            private string _name;
            private int _quantity;
            private decimal _unitPrice;

            public LineItemValueObject(
                int productId,
                string name,
                int quantity,
                decimal unitPrice)
            {
                _unitPrice = unitPrice;
                _quantity = quantity;
                _name = name;
                _productId = productId;
            }

            public decimal Cost
            {
                get { return _quantity * _unitPrice; }
            }
        }

        public class OrderRepository
        {
            public Order[] GetAll()
            {
                var ordersDataSet = GetOrdersDataSet();

                var orders = new List<Order>();

                foreach (DataRow orderDataRow in ordersDataSet.Tables[0].Rows)
                {
                    var lineItemsForOrder = ordersDataSet.Tables[1].Select("OrderId = " + orderDataRow["OrderId"]);

                    var lineItemValueObjects = new List<LineItemValueObject>();

                    foreach (var lineItemDataRow in lineItemsForOrder)
                    {
                        var lineItemValueObject = new LineItemValueObject(
                            Convert.ToInt32(lineItemDataRow["ProductId"]),
                            lineItemDataRow["Name"].ToString(),
                            Convert.ToInt32(lineItemDataRow["Quantity"]),
                            Convert.ToDecimal(lineItemDataRow["UnitPrice"]));

                        lineItemValueObjects.Add(lineItemValueObject);
                    }

                    var order = new Order(
                        (Guid)orderDataRow["OrderId"],
                        Convert.ToDateTime(orderDataRow["OrderDate"]),
                        orderDataRow["CustomerName"].ToString(),
                        lineItemValueObjects.ToArray());

                    orders.Add(order);
                }

                return orders.ToArray();
            }

            private DataSet GetOrdersDataSet()
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

        }
    }
}