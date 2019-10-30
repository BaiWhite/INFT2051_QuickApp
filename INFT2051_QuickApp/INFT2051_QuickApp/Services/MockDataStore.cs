using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INFT2051_QuickApp.Models;

namespace INFT2051_QuickApp.Services
{
    public class MockDataStore : IDataStore<BusinessPage>
    {
        List<BusinessPage> items;

        public MockDataStore()
        {
            items = new List<BusinessPage>();
            var mockItems = new List<BusinessPage>
            {
                new BusinessPage { Id = Guid.NewGuid().ToString(), CompanyName = "Example Company", Description="This is an item description." },
                new BusinessPage { Id = Guid.NewGuid().ToString(), CompanyName = "Second item", Description="This is an item description." },
                new BusinessPage { Id = Guid.NewGuid().ToString(), CompanyName = "Third item", Description="This is an item description." },
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(BusinessPage item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(BusinessPage item)
        {
            var oldItem = items.Where((BusinessPage arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((BusinessPage arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<BusinessPage> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<BusinessPage>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}