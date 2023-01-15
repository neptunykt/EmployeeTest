using System;
using System.Globalization;
using System.Linq;
using Core.Entities;
using Core.Model;
using Core.Services;
using Microsoft.EntityFrameworkCore;

namespace Core.DAL
{
    public static class EmployeeExtensions
    {
        /// <summary>
        /// Queryable Order by Employee
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="propertyName"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static IOrderedQueryable<Employee> OrderBy(this IQueryable<Employee> queryable, string propertyName,
            OrderDirection? direction)
        {
            switch (propertyName)
            {
                case "payRollNumber":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.PayRollNumber)
                        : queryable.OrderBy(e => e.PayRollNumber);
                case "surname":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Surname)
                        : queryable.OrderBy(e => e.Surname);
                case "forenames":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Forenames)
                        : queryable.OrderBy(e => e.Forenames);
                case "dateOfBirth":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.DateOfBirth)
                        : queryable.OrderBy(e => e.DateOfBirth);
                case "telephone":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Telephone)
                        : queryable.OrderBy(e => e.Telephone);
                case "mobile":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Mobile)
                        : queryable.OrderBy(e => e.Mobile);
                case "address":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Address)
                        : queryable.OrderBy(e => e.Address);
                case "address2":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Address2)
                        : queryable.OrderBy(e => e.Address2);
                case "postCode":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.PostCode)
                        : queryable.OrderBy(e => e.PostCode);
                case "emailHome":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.EmailHome)
                        : queryable.OrderBy(e => e.EmailHome);
                case "startDate":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.StartDate)
                        : queryable.OrderBy(e => e.StartDate);
                case "":
                case null:
                    return queryable.OrderBy(e => e.Id);
                default:
                    throw new ArgumentException("Property not found", nameof(propertyName));
            }
        }
        
        
        /// <summary>
        /// Queryable Order search by filter
        /// </summary>
        /// <param name="queryable"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        /// <exception cref="ServiceException"></exception>

        public static IQueryable<Employee> SearchText(this IQueryable<Employee> queryable, string searchText)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                return queryable.Where(e =>
                    e.Address.ToLower().Contains(searchText.ToLower()) ||
                    e.Address2.ToLower().Contains(searchText.ToLower()) ||
                    e.Forenames.ToLower().Contains(searchText.ToLower()) ||
                    e.Mobile.ToLower().Contains(searchText.ToLower()) ||
                    e.Surname.ToLower().Contains(searchText.ToLower()) ||
                    e.Telephone.ToLower().Contains(searchText.ToLower()) ||
                    e.EmailHome.ToLower().Contains(searchText.ToLower()) ||
                    e.PostCode.ToLower().Contains(searchText.ToLower()) ||
                    e.PayRollNumber.ToLower().Contains(searchText.ToLower()));
            }
            // nothing to do
            return queryable;
        }

        public static IQueryable<Employee> SearchDate(this IQueryable<Employee> queryable, string searchDate)
        {
            if (!string.IsNullOrEmpty(searchDate))
            {
                var cultureInfo = new CultureInfo("en-US");
                DateTime.TryParseExact(searchDate, "dd/MM/yyyy", cultureInfo, DateTimeStyles.None, out var dateValue);
                return queryable.Where(e =>
                    e.DateOfBirth.Date.Date == dateValue.Date ||
                    e.StartDate.Date.Date == dateValue.Date);
            }
            // nothing to do
            return queryable;
        }

        }
    }