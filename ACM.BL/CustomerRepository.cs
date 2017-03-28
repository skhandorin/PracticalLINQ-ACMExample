using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ACM.BL
{
    public class CustomerRepository
    {
        public Customer Find(List<Customer> customerList, int customerId)
        {
            Customer foundCustomer = null;

            foundCustomer = customerList.FirstOrDefault(c => c.CustomerId == customerId);

            return foundCustomer;
        }

        public IEnumerable<string> GetNames(List<Customer> customerList)
        {
            var query = customerList.Select(c => c.LastName + ", " + c.FirstName);
            return query;
        }

        public dynamic GetNamesAndEmail(List<Customer> customerList)
        {
            var query = customerList.Select(c => new
                            {
                                Name = c.LastName + ", " + c.FirstName,
                                c.EmailAddress
                            });
            foreach (var item in query)
            {
                Console.WriteLine(item.Name + ": " + item.EmailAddress);
            }
            return query;
        }

        public dynamic GetNamesAndType(List<Customer> customerList, List<CustomerType> customerTypeList)
        {
            var query = customerList.Join(customerTypeList,
                                        c => c.CustomerTypeId,
                                        ct => ct.CustomerTypeId,
                                        (c, ct) => new
                                        {
                                            Name = c.LastName + ", " + c.FirstName,
                                            CustomerTypeName = ct.TypeName
                                        });

            foreach (var item in query)
            {
                Console.WriteLine(item.Name + ": " + item.CustomerTypeName);
            }

            return query;
        }

        public IEnumerable<Customer> GetOverdueCustomers(List<Customer> customerList)
        {
            var query = customerList
                        .SelectMany(c => c.InvoiceList
                                    .Where(i => (i.IsPaid ?? false) == false),
                                    (c,i) => c)
                        .Distinct();
            return query;
        }

        public List<Customer> Retrieve()
        {
            var invoiceRepository = new InvoiceRepository();

            List<Customer> custList = new List<Customer>
                    {new Customer() 
                          { CustomerId = 1, 
                            FirstName="Frodo",
                            LastName = "Baggins",
                            EmailAddress = "fb@hob.me",
                            CustomerTypeId=1,
                            InvoiceList = invoiceRepository.Retrieve(1)
                    },
                    new Customer() 
                          { CustomerId = 2, 
                            FirstName="Bilbo",
                            LastName = "Baggins",
                            EmailAddress = "bb@hob.me",
                            CustomerTypeId=null,
                            InvoiceList = invoiceRepository.Retrieve(2)},
                    new Customer() 
                          { CustomerId = 3, 
                            FirstName="Samwise",
                            LastName = "Gamgee",
                            EmailAddress = "sg@hob.me",
                            CustomerTypeId=4,
                            InvoiceList = invoiceRepository.Retrieve(3)},
                    new Customer() 
                          { CustomerId = 4, 
                            FirstName="Rosie",
                            LastName = "Cotton",
                            EmailAddress = "rc@hob.me",
                            CustomerTypeId=2,
                            InvoiceList = invoiceRepository.Retrieve(4)}};
            return custList;
        }

        public IEnumerable<Customer> RetrieveEmptyList()
        {
            return Enumerable.Repeat(new Customer(), 5);
        }

        public IEnumerable<Customer> SortByName(IEnumerable<Customer> customerList)
        {
            return customerList.OrderBy(c => c.LastName)
                                .ThenBy(c => c.FirstName);
        }

        public IEnumerable<KeyValuePair<string, decimal>> GetInvoiceTotalByCustomerType(List<Customer> customerList, List<CustomerType> customerTypeList)
        {
            var customerTypeQuery = customerList.GroupJoin(customerTypeList, 
                                                      c => c.CustomerTypeId,
                                                      ct => ct.CustomerTypeId,
                                                      (c, ctRows) => new
                                                      {
                                                          Customer = c,
                                                          CustomerTypeRows = ctRows.DefaultIfEmpty()
                                                      })
                                                .SelectMany( z => z.CustomerTypeRows.Select(ct => new
                                                    {
                                                        Customer = z.Customer,
                                                        CustomerType = ct
                                                    })
                
                                                );

            var query = customerTypeQuery.GroupBy(c => c.CustomerType,
                                             c => c.Customer.InvoiceList.Sum(inv => inv.TotalAmount),
                                             (groupKey, invTotal) => new KeyValuePair<string, decimal>
                                             (
                                                 groupKey == null ? "_unspecified_" : groupKey.TypeName,
                                                 invTotal.Sum()
                                             ));

            foreach (var item in query)
            {
                Console.WriteLine(item.Key + ": " + item.Value);
            }
            return query;
        }
    }
}
